using System.Linq;
using System.Threading.Tasks;
using DataverseModel;
using Gov.Cscp.VictimServices.Public.Models;
using Gov.Cscp.VictimServices.Public.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Gov.Cscp.VictimServices.Public.Controllers
{
    [Route("api/[controller]")]
    public class RestitutionsController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger<RestitutionsController> _logger;

        public RestitutionsController(
            IOrganizationServiceAsync organizationService,
            ILogger<RestitutionsController> logger
        )
        {
            _organizationService = organizationService;
            _logger = logger;
        }

        [HttpPost("victim")]
        public async Task<IActionResult> SubmitVictimRestitution([FromBody] CreateVictimRestitutionCaseRequestDto model)
        {
            return await SubmitRestitutionInternal(
                model,
                x => x.ConvertToDynamicsRequest(),
                RestitutionSubmitAudit.VictimFormType
            );
        }

        [HttpPost("victim-entity")]
        public async Task<IActionResult> SubmitVictimEntityRestitution(
            [FromBody] CreateVictimEntityRestitutionCaseRequestDto model
        )
        {
            return await SubmitRestitutionInternal(
                model,
                x => x.ConvertToDynamicsRequest(),
                RestitutionSubmitAudit.VictimEntityFormType
            );
        }

        [HttpPost("offender")]
        public async Task<IActionResult> SubmitOffenderRestitution(
            [FromBody] CreateOffenderRestitutionCaseRequestDto model
        )
        {
            return await SubmitRestitutionInternal(
                model,
                x => x.ConvertToDynamicsRequest(),
                RestitutionSubmitAudit.OffenderFormType
            );
        }

        private async Task<IActionResult> SubmitRestitutionInternal<TModel>(
            TModel model,
            System.Func<TModel, VSd_CreateRestitutionCaseRequest> dynamicsRequestFactory,
            string formType
        )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(entry => entry.Value?.Errors.Count > 0)
                        .Select(entry =>
                            $"{entry.Key}: {string.Join(", ", entry.Value.Errors.Select(err => err.ErrorMessage))}"
                        );
                    _logger.LogError(
                        "API call to 'SubmitRestitution' made with invalid model state. Errors are:\n{Errors}",
                        string.Join("\n", errors)
                    );
                    return BadRequest(ModelState);
                }

                var request = dynamicsRequestFactory(model);
                var response = await _organizationService.ExecuteAsync(request);

                if (response.Results["IsSuccess"] is not true)
                {
                    _logger.LogError(
                        "Error while saving victim restitution. Response from Dynamics was:\n{@Response}",
                        response
                    );

                    return StatusCode(500, "An error occurred while saving the restitution case.");
                }

                RestitutionSubmitAudit.WriteSuccess(_logger, formType, HttpContext?.TraceIdentifier);

                return Ok(response);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, "Unexpected error while saving victim restitution.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
