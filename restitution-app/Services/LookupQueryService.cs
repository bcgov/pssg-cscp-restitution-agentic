using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataverseModel;
using Gov.Cscp.VictimServices.Public.Models;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Gov.Cscp.VictimServices.Public.Services
{
    public class LookupQueryService
    {
        private readonly IOrganizationServiceAsync _organizationService;

        public LookupQueryService(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<LookupResponseDto<CountryLookupDto>> GetCountriesAsync()
        {
            var query = new QueryExpression("vsd_country")
            {
                ColumnSet = new ColumnSet("vsd_countryid", "vsd_name"),
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<CountryLookupDto>
            {
                Value = result
                    .Entities.Select(entity => new CountryLookupDto
                    {
                        vsd_countryid = entity.Id.ToString(),
                        vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                    })
                    .ToList(),
            };
        }

        public async Task<LookupResponseDto<ProvinceLookupDto>> GetProvincesAsync()
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

            return new LookupResponseDto<ProvinceLookupDto>
            {
                Value = result
                    .Entities.Select(entity =>
                    {
                        var countryReference = entity.GetAttributeValue<EntityReference>(
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
        }

        public async Task<LookupResponseDto<CityLookupDto>> GetCitiesAsync()
        {
            var query = new QueryExpression("vsd_city")
            {
                ColumnSet = new ColumnSet("vsd_cityid", "vsd_countryid", "vsd_stateid", "vsd_name"),
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<CityLookupDto>
            {
                Value = result.Entities.Select(MapCity).ToList(),
            };
        }

        public async Task<CitySearchResponseDto> SearchCitiesAsync(
            string country,
            string province,
            string searchVal,
            int limit
        )
        {
            var maxResults = limit > 0 ? limit : 15;
            var normalizedSearchVal = searchVal?.Trim();
            var cityEntities = new List<Entity>();
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

            return new CitySearchResponseDto
            {
                Result = "success",
                CityCollection = cityEntities.Select(MapCity).ToList(),
                CountryCollection = Array.Empty<CountryLookupDto>(),
                ProvinceCollection = Array.Empty<ProvinceLookupDto>(),
            };
        }

        public async Task<LookupResponseDto<CityLookupDto>> GetCitiesByCountryAsync(Guid countryId)
        {
            var query = new QueryExpression("vsd_city")
            {
                ColumnSet = new ColumnSet("vsd_cityid", "vsd_countryid", "vsd_stateid", "vsd_name"),
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("vsd_countryid", ConditionOperator.Equal, countryId);
            query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<CityLookupDto>
            {
                Value = result.Entities.Select(MapCity).ToList(),
            };
        }

        public async Task<LookupResponseDto<CityLookupDto>> GetCitiesByProvinceAsync(
            Guid countryId,
            Guid provinceId
        )
        {
            var query = new QueryExpression("vsd_city")
            {
                ColumnSet = new ColumnSet("vsd_cityid", "vsd_countryid", "vsd_stateid", "vsd_name"),
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("vsd_countryid", ConditionOperator.Equal, countryId);
            query.Criteria.AddCondition("vsd_stateid", ConditionOperator.Equal, provinceId);
            query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<CityLookupDto>
            {
                Value = result.Entities.Select(MapCity).ToList(),
            };
        }

        public async Task<LookupResponseDto<RelationshipLookupDto>> GetRelationshipsAsync()
        {
            return await GetRelationshipsInternalAsync();
        }

        public async Task<
            LookupResponseDto<RelationshipLookupDto>
        > GetOptionalAuthorizationRelationshipsAsync()
        {
            return await GetRelationshipsInternalAsync("vsd_optionalauthorizedrelationship");
        }

        public async Task<
            LookupResponseDto<RelationshipLookupDto>
        > GetRepresentativeRelationshipsAsync()
        {
            return await GetRelationshipsInternalAsync("vsd_cvap_representativerelationship");
        }

        public async Task<
            LookupResponseDto<RelationshipLookupDto>
        > GetRestitutionRelationshipsAsync()
        {
            return await GetRelationshipsInternalAsync("vsd_rest_offenderrelationship");
        }

        public async Task<LookupResponseDto<PoliceDetachmentLookupDto>> GetPoliceDetachmentsAsync()
        {
            var query = new QueryExpression("vsd_policedetachment")
            {
                ColumnSet = new ColumnSet("vsd_policedetachmentid", "vsd_name"),
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<PoliceDetachmentLookupDto>
            {
                Value = result
                    .Entities.Select(entity => new PoliceDetachmentLookupDto
                    {
                        vsd_policedetachmentid = entity.Id.ToString(),
                        vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                    })
                    .ToList(),
            };
        }

        public async Task<LookupResponseDto<LookupItemDto>> GetCourtsAsync()
        {
            var query = new QueryExpression(VSd_Court.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(VSd_Court.Fields.VSd_CourtId, VSd_Court.Fields.VSd_Name),
            };

            query.Criteria.AddCondition(
                VSd_Court.Fields.StateCode,
                ConditionOperator.Equal,
                (int)VSd_Court_StateCode.Active
            );
            query.Orders.Add(new OrderExpression(VSd_Court.Fields.VSd_Name, OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<LookupItemDto>
            {
                Value = result
                    .Entities.Select(entity => new LookupItemDto
                    {
                        Id = entity.Id.ToString(),
                        Name = entity.GetAttributeValue<string>(VSd_Court.Fields.VSd_Name),
                    })
                    .ToList(),
            };
        }

        private async Task<LookupResponseDto<RelationshipLookupDto>> GetRelationshipsInternalAsync(
            string filterField = null
        )
        {
            var query = new QueryExpression("vsd_relationship")
            {
                ColumnSet = new ColumnSet("vsd_relationshipid", "vsd_name"),
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);

            if (!string.IsNullOrWhiteSpace(filterField))
            {
                query.Criteria.AddCondition(filterField, ConditionOperator.Equal, true);
            }

            query.Orders.Add(new OrderExpression("vsd_name", OrderType.Ascending));

            var result = await _organizationService.RetrieveMultipleAsync(query);

            return new LookupResponseDto<RelationshipLookupDto>
            {
                Value = result
                    .Entities.Select(entity => new RelationshipLookupDto
                    {
                        vsd_relationshipid = entity.Id.ToString(),
                        vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                    })
                    .ToList(),
            };
        }

        private static CityLookupDto MapCity(Entity entity)
        {
            var countryReference = entity.GetAttributeValue<EntityReference>("vsd_countryid");
            var provinceReference = entity.GetAttributeValue<EntityReference>("vsd_stateid");

            return new CityLookupDto
            {
                vsd_cityid = entity.Id.ToString(),
                _vsd_countryid_value = countryReference?.Id.ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                _vsd_stateid_value = provinceReference?.Id.ToString(),
            };
        }
    }
}
