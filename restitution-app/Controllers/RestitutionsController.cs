using System.Threading.Tasks;
using DataverseModel;
using Gov.Cscp.VictimServices.Public.Models;
using Gov.Cscp.VictimServices.Public.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Serilog;

namespace Gov.Cscp.VictimServices.Public.Controllers
{
    [Route("api/[controller]")]
    public class RestitutionsController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public RestitutionsController(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpPost("victim")]
        public async Task<IActionResult> SubmitVictimRestitution(
            [FromBody] CreateVictimRestitutionCaseRequestDto model
        )
        {
            return await SubmitRestitutionInternal(model, x => x.ConvertToDynamicsRequest());
        }

        [HttpPost("victim-entity")]
        public async Task<IActionResult> SubmitVictimEntityRestitution(
            [FromBody] CreateVictimEntityRestitutionCaseRequestDto model
        )
        {
            return await SubmitRestitutionInternal(model, x => x.ConvertToDynamicsRequest());
        }

        [HttpPost("offender")]
        public async Task<IActionResult> SubmitOffenderRestitution(
            [FromBody] CreateOffenderRestitutionCaseRequestDto model
        )
        {
            return await SubmitRestitutionInternal(model, x => x.ConvertToDynamicsRequest());
        }

        private async Task<IActionResult> SubmitRestitutionInternal<TModel>(
            TModel model,
            System.Func<TModel, VSd_CreateRestitutionCaseRequest> dynamicsRequestFactory
        )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error(
                        $"API call to 'SubmitRestitution' made with invalid model state. Error is:\n{ModelState}."
                    );
                    return BadRequest(ModelState);
                }

                var request = dynamicsRequestFactory(model);
                var response = await _organizationService.ExecuteAsync(request);

                if (response.Results["IsSuccess"] is not true)
                {
                    _logger.Error(
                        "Error while saving victim restitution. Response from Dynamics was:\n{@Response}",
                        response
                    );

                    return StatusCode(500, "An error occurred while saving the restitution case.");
                }

                return Ok(response);
            }
            catch (System.Exception e)
            {
                _logger.Error(e, "Unexpected error while saving victim restitution.", model);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
