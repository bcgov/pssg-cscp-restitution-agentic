using System;
using System.Threading.Tasks;
using Gov.Cscp.VictimServices.Public.Models;
using Gov.Cscp.VictimServices.Public.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Gov.Cscp.VictimServices.Public.Controllers
{
    [Route("api/[controller]")]
    public class LookupsController : Controller
    {
        private readonly ILookupQueryService _lookupQueryService;
        private readonly ILogger _logger;

        public LookupsController(ILookupQueryService lookupQueryService)
        {
            _lookupQueryService = lookupQueryService;
            _logger = Log.Logger;
        }

        [HttpGet("countries")]
        [ProducesResponseType(typeof(LookupResponseDto<CountryLookupDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<CountryLookupDto>>> GetCountries()
        {
            try
            {
                return Ok(await _lookupQueryService.GetCountriesAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up countries in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("provinces")]
        [ProducesResponseType(
            typeof(LookupResponseDto<ProvinceLookupDto>),
            StatusCodes.Status200OK
        )]
        public async Task<ActionResult<LookupResponseDto<ProvinceLookupDto>>> GetProvinces()
        {
            try
            {
                return Ok(await _lookupQueryService.GetProvincesAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up provinces in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("cities")]
        [ProducesResponseType(typeof(LookupResponseDto<CityLookupDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<CityLookupDto>>> GetCities()
        {
            try
            {
                return Ok(await _lookupQueryService.GetCitiesAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up cities in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("cities/search")]
        [ProducesResponseType(typeof(CitySearchResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CitySearchResponseDto>> SearchCities(
            [FromQuery] string country,
            [FromQuery] string province,
            [FromQuery] string searchVal,
            [FromQuery] int limit
        )
        {
            try
            {
                return Ok(
                    await _lookupQueryService.SearchCitiesAsync(country, province, searchVal, limit)
                );
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while searching cities in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("country/{country}/cities")]
        [ProducesResponseType(typeof(LookupResponseDto<CityLookupDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<CityLookupDto>>> GetCitiesByCountry(
            string country
        )
        {
            try
            {
                if (!Guid.TryParse(country, out Guid countryId))
                {
                    return BadRequest();
                }

                return Ok(await _lookupQueryService.GetCitiesByCountryAsync(countryId));
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up cities by country in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("country/{countryId}/province/{provinceId}/cities")]
        [ProducesResponseType(typeof(LookupResponseDto<CityLookupDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<CityLookupDto>>> GetCitiesByProvince(
            string countryId,
            string provinceId
        )
        {
            try
            {
                if (!Guid.TryParse(countryId, out Guid parsedCountryId))
                {
                    return BadRequest();
                }

                if (!Guid.TryParse(provinceId, out Guid parsedProvinceId))
                {
                    return BadRequest();
                }

                return Ok(
                    await _lookupQueryService.GetCitiesByProvinceAsync(
                        parsedCountryId,
                        parsedProvinceId
                    )
                );
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up cities by province in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("relationships")]
        [ProducesResponseType(
            typeof(LookupResponseDto<RelationshipLookupDto>),
            StatusCodes.Status200OK
        )]
        public async Task<ActionResult<LookupResponseDto<RelationshipLookupDto>>> GetRelationships()
        {
            try
            {
                return Ok(await _lookupQueryService.GetRelationshipsAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("auth_relationships")]
        [ProducesResponseType(
            typeof(LookupResponseDto<RelationshipLookupDto>),
            StatusCodes.Status200OK
        )]
        public async Task<
            ActionResult<LookupResponseDto<RelationshipLookupDto>>
        > GetOptionalAuthorizationRelationships()
        {
            try
            {
                return Ok(await _lookupQueryService.GetOptionalAuthorizationRelationshipsAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up optional auth relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("representative_relationships")]
        [ProducesResponseType(
            typeof(LookupResponseDto<RelationshipLookupDto>),
            StatusCodes.Status200OK
        )]
        public async Task<
            ActionResult<LookupResponseDto<RelationshipLookupDto>>
        > GetRepresentativeRelationships()
        {
            try
            {
                return Ok(await _lookupQueryService.GetRepresentativeRelationshipsAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up representative relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("restitution_relationships")]
        [ProducesResponseType(
            typeof(LookupResponseDto<RelationshipLookupDto>),
            StatusCodes.Status200OK
        )]
        public async Task<
            ActionResult<LookupResponseDto<RelationshipLookupDto>>
        > GetRestitutionRelationships()
        {
            try
            {
                return Ok(await _lookupQueryService.GetRestitutionRelationshipsAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up restitution relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("police_detachments")]
        [ProducesResponseType(
            typeof(LookupResponseDto<PoliceDetachmentLookupDto>),
            StatusCodes.Status200OK
        )]
        public async Task<
            ActionResult<LookupResponseDto<PoliceDetachmentLookupDto>>
        > GetPoliceDetachments()
        {
            try
            {
                return Ok(await _lookupQueryService.GetPoliceDetachmentsAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up police detachments in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }

        [HttpGet("courts")]
        [ProducesResponseType(typeof(LookupResponseDto<LookupItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<LookupItemDto>>> GetCourts()
        {
            try
            {
                return Ok(await _lookupQueryService.GetCourtsAsync());
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up courts in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
        }
    }
}
