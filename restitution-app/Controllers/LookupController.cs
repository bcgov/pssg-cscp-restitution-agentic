using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataverseModel;
using Gov.Cscp.VictimServices.Public.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Serilog;

namespace Gov.Cscp.VictimServices.Public.Controllers
{
    [Route("api/lookups")]
    public class LookupsController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public LookupsController(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("countries")]
        [ProducesResponseType(typeof(LookupResponseDto<CountryLookupDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<CountryLookupDto>>> GetCountries()
        {
            try
            {
                var query = new QueryExpression("vsd_country")
                {
                    ColumnSet = new ColumnSet("vsd_countryid", "vsd_name"),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<CountryLookupDto>
                {
                    Value = result
                        .Entities.Select(entity => new CountryLookupDto
                        {
                            vsd_countryid = entity.Id.ToString(),
                            vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up countries in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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
                var query = new QueryExpression("vsd_province")
                {
                    ColumnSet = new ColumnSet(
                        "vsd_provinceid",
                        "vsd_code",
                        "vsd_countryid",
                        "vsd_name"
                    ),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<ProvinceLookupDto>
                {
                    Value = result
                        .Entities.Select(entity =>
                        {
                            var countryReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_countryid"
                                );

                            return new ProvinceLookupDto
                            {
                                vsd_provinceid = entity.Id.ToString(),
                                vsd_code = entity.GetAttributeValue<string>("vsd_code"),
                                _vsd_countryid_value = countryReference?.Id.ToString(),
                                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                            };
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up provinces in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("cities")]
        [ProducesResponseType(typeof(LookupResponseDto<CityLookupDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<CityLookupDto>>> GetCities()
        {
            try
            {
                var query = new QueryExpression("vsd_city")
                {
                    ColumnSet = new ColumnSet(
                        "vsd_cityid",
                        "vsd_countryid",
                        "vsd_stateid",
                        "vsd_name"
                    ),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<CityLookupDto>
                {
                    Value = result
                        .Entities.Select(entity =>
                        {
                            var countryReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_countryid"
                                );
                            var provinceReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_stateid"
                                );

                            return new CityLookupDto
                            {
                                vsd_cityid = entity.Id.ToString(),
                                _vsd_countryid_value = countryReference?.Id.ToString(),
                                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                                _vsd_stateid_value = provinceReference?.Id.ToString(),
                            };
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up cities in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("cities/search")]
        [ProducesResponseType(typeof(CitySearchResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CitySearchResponseDto>> SearchCities(
            string country,
            string province,
            string searchVal,
            int limit
        )
        {
            try
            {
                var maxResults = limit > 0 ? limit : 15;
                var normalizedSearchVal = searchVal?.Trim();
                var cityEntities = new List<Microsoft.Xrm.Sdk.Entity>();
                Guid countryId = Guid.Empty;
                Guid provinceId = Guid.Empty;

                var hasCountryFilter =
                    !string.IsNullOrWhiteSpace(country) && Guid.TryParse(country, out countryId);
                var hasProvinceFilter =
                    !string.IsNullOrWhiteSpace(province) && Guid.TryParse(province, out provinceId);

                QueryExpression BuildCityQuery(int topCount)
                {
                    var cityQuery = new QueryExpression("vsd_city")
                    {
                        ColumnSet = new ColumnSet(
                            "vsd_cityid",
                            "vsd_countryid",
                            "vsd_stateid",
                            "vsd_name"
                        ),
                        TopCount = topCount,
                    };

                    cityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);

                    if (hasCountryFilter)
                    {
                        cityQuery.Criteria.AddCondition(
                            "vsd_countryid",
                            ConditionOperator.Equal,
                            countryId
                        );
                    }

                    if (hasProvinceFilter)
                    {
                        cityQuery.Criteria.AddCondition(
                            "vsd_stateid",
                            ConditionOperator.Equal,
                            provinceId
                        );
                    }

                    cityQuery.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                    return cityQuery;
                }

                if (!string.IsNullOrWhiteSpace(normalizedSearchVal))
                {
                    var startsWithQuery = BuildCityQuery(maxResults);
                    startsWithQuery.Criteria.AddCondition(
                        "vsd_name",
                        ConditionOperator.BeginsWith,
                        normalizedSearchVal
                    );

                    var startsWithResult = await _organizationService.RetrieveMultipleAsync(
                        startsWithQuery
                    );
                    cityEntities.AddRange(startsWithResult.Entities);

                    if (cityEntities.Count < maxResults)
                    {
                        var containsQuery = BuildCityQuery(maxResults - cityEntities.Count);
                        containsQuery.Criteria.AddCondition(
                            "vsd_name",
                            ConditionOperator.Like,
                            $"%{normalizedSearchVal}%"
                        );
                        containsQuery.Criteria.AddCondition(
                            "vsd_name",
                            ConditionOperator.NotLike,
                            $"{normalizedSearchVal}%"
                        );

                        var containsResult = await _organizationService.RetrieveMultipleAsync(
                            containsQuery
                        );
                        var existingIds = cityEntities.Select(entity => entity.Id).ToHashSet();

                        cityEntities.AddRange(
                            containsResult.Entities.Where(entity => existingIds.Add(entity.Id))
                        );
                    }
                }
                else
                {
                    var query = BuildCityQuery(maxResults);
                    var result = await _organizationService.RetrieveMultipleAsync(query);
                    cityEntities.AddRange(result.Entities);
                }

                var response = new CitySearchResponseDto
                {
                    Result = "success",
                    CityCollection = cityEntities
                        .Select(entity =>
                        {
                            var countryReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_countryid"
                                );
                            var provinceReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_stateid"
                                );

                            return new CityLookupDto
                            {
                                vsd_cityid = entity.Id.ToString(),
                                _vsd_countryid_value = countryReference?.Id.ToString(),
                                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                                _vsd_stateid_value = provinceReference?.Id.ToString(),
                            };
                        })
                        .ToList(),
                    CountryCollection = Array.Empty<CountryLookupDto>(),
                    ProvinceCollection = Array.Empty<ProvinceLookupDto>(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while searching cities in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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

                var query = new QueryExpression("vsd_city")
                {
                    ColumnSet = new ColumnSet(
                        "vsd_cityid",
                        "vsd_countryid",
                        "vsd_stateid",
                        "vsd_name"
                    ),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Criteria.AddCondition("vsd_countryid", ConditionOperator.Equal, countryId);
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<CityLookupDto>
                {
                    Value = result
                        .Entities.Select(entity =>
                        {
                            var countryReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_countryid"
                                );
                            var provinceReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_stateid"
                                );

                            return new CityLookupDto
                            {
                                vsd_cityid = entity.Id.ToString(),
                                _vsd_countryid_value = countryReference?.Id.ToString(),
                                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                                _vsd_stateid_value = provinceReference?.Id.ToString(),
                            };
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up cities by country in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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

                var query = new QueryExpression("vsd_city")
                {
                    ColumnSet = new ColumnSet(
                        "vsd_cityid",
                        "vsd_countryid",
                        "vsd_stateid",
                        "vsd_name"
                    ),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Criteria.AddCondition(
                    "vsd_countryid",
                    ConditionOperator.Equal,
                    parsedCountryId
                );
                query.Criteria.AddCondition(
                    "vsd_stateid",
                    ConditionOperator.Equal,
                    parsedProvinceId
                );
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<CityLookupDto>
                {
                    Value = result
                        .Entities.Select(entity =>
                        {
                            var countryReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_countryid"
                                );
                            var provinceReference =
                                entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>(
                                    "vsd_stateid"
                                );

                            return new CityLookupDto
                            {
                                vsd_cityid = entity.Id.ToString(),
                                _vsd_countryid_value = countryReference?.Id.ToString(),
                                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                                _vsd_stateid_value = provinceReference?.Id.ToString(),
                            };
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up cities by province in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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
                var query = new QueryExpression("vsd_relationship")
                {
                    ColumnSet = new ColumnSet("vsd_relationshipid", "vsd_name"),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<RelationshipLookupDto>
                {
                    Value = result
                        .Entities.Select(entity => new RelationshipLookupDto
                        {
                            vsd_relationshipid = entity.Id.ToString(),
                            vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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
                var query = new QueryExpression("vsd_relationship")
                {
                    ColumnSet = new ColumnSet("vsd_relationshipid", "vsd_name"),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Criteria.AddCondition(
                    "vsd_optionalauthorizedrelationship",
                    ConditionOperator.Equal,
                    true
                );
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<RelationshipLookupDto>
                {
                    Value = result
                        .Entities.Select(entity => new RelationshipLookupDto
                        {
                            vsd_relationshipid = entity.Id.ToString(),
                            vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up optional auth relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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
                var query = new QueryExpression("vsd_relationship")
                {
                    ColumnSet = new ColumnSet("vsd_relationshipid", "vsd_name"),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Criteria.AddCondition(
                    "vsd_cvap_representativerelationship",
                    ConditionOperator.Equal,
                    true
                );
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<RelationshipLookupDto>
                {
                    Value = result
                        .Entities.Select(entity => new RelationshipLookupDto
                        {
                            vsd_relationshipid = entity.Id.ToString(),
                            vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up representative relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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
                var query = new QueryExpression("vsd_relationship")
                {
                    ColumnSet = new ColumnSet("vsd_relationshipid", "vsd_name"),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Criteria.AddCondition(
                    "vsd_rest_offenderrelationship",
                    ConditionOperator.Equal,
                    true
                );
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<RelationshipLookupDto>
                {
                    Value = result
                        .Entities.Select(entity => new RelationshipLookupDto
                        {
                            vsd_relationshipid = entity.Id.ToString(),
                            vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up restitution relationships in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
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
                var query = new QueryExpression("vsd_policedetachment")
                {
                    ColumnSet = new ColumnSet("vsd_policedetachmentid", "vsd_name"),
                };

                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<PoliceDetachmentLookupDto>
                {
                    Value = result
                        .Entities.Select(entity => new PoliceDetachmentLookupDto
                        {
                            vsd_policedetachmentid = entity.Id.ToString(),
                            vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up police detachments in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("courts")]
        [ProducesResponseType(typeof(LookupResponseDto<LookupItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupResponseDto<LookupItemDto>>> GetCourts()
        {
            try
            {
                var query = new QueryExpression(VSd_Court.EntityLogicalName)
                {
                    ColumnSet = new ColumnSet(
                        VSd_Court.Fields.VSd_CourtId,
                        VSd_Court.Fields.VSd_Name
                    ),
                };

                query.Criteria.AddCondition(
                    VSd_Court.Fields.StateCode,
                    ConditionOperator.Equal,
                    (int)VSd_Court_StateCode.Active
                );
                query.Orders.Add(
                    new OrderExpression(VSd_Court.Fields.VSd_Name, OrderType.Ascending)
                );

                var result = await _organizationService.RetrieveMultipleAsync(query);

                var response = new LookupResponseDto<LookupItemDto>
                {
                    Value = result
                        .Entities.Select(entity => new LookupItemDto
                        {
                            Id = entity.Id.ToString(),
                            Name = entity.GetAttributeValue<string>(VSd_Court.Fields.VSd_Name),
                        })
                        .ToList(),
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Error(
                    e,
                    "Unexpected error while looking up courts in Dataverse. Source = Restitution"
                );
                return BadRequest();
            }
            finally { }
        }
    }
}
