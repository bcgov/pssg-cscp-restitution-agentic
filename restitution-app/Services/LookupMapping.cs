using System.Linq;
using Gov.Cscp.VictimServices.Public.Models;
using Microsoft.Xrm.Sdk;

namespace Gov.Cscp.VictimServices.Public.Services
{
    public static class LookupMapping
    {
        public static CountryLookupDto ToCountryLookupDto(Entity entity)
        {
            return new CountryLookupDto
            {
                Id = entity.Id.ToString(),
                Name = entity.GetAttributeValue<string>("vsd_name"),
            };
        }

        public static ProvinceLookupDto ToProvinceLookupDto(Entity entity)
        {
            var countryReference = entity.GetAttributeValue<EntityReference>("vsd_countryid");

            return new ProvinceLookupDto
            {
                Id = entity.Id.ToString(),
                Code = entity.GetAttributeValue<string>("vsd_code"),
                CountryId = countryReference?.Id.ToString(),
                Name = entity.GetAttributeValue<string>("vsd_name"),
            };
        }

        public static CityLookupDto ToCityLookupDto(Entity entity)
        {
            var countryReference = entity.GetAttributeValue<EntityReference>("vsd_countryid");
            var provinceReference = entity.GetAttributeValue<EntityReference>("vsd_stateid");

            return new CityLookupDto
            {
                Id = entity.Id.ToString(),
                CountryId = countryReference?.Id.ToString(),
                ProvinceId = provinceReference?.Id.ToString(),
                Name = entity.GetAttributeValue<string>("vsd_name"),
            };
        }

        public static RelationshipLookupDto ToRelationshipLookupDto(Entity entity)
        {
            return new RelationshipLookupDto
            {
                Id = entity.Id.ToString(),
                Name = entity.GetAttributeValue<string>("vsd_name"),
            };
        }

        public static PoliceDetachmentLookupDto ToPoliceDetachmentLookupDto(Entity entity)
        {
            return new PoliceDetachmentLookupDto
            {
                Id = entity.Id.ToString(),
                Name = entity.GetAttributeValue<string>("vsd_name"),
            };
        }

        public static LookupItemDto ToCourtLookupItemDto(Entity entity)
        {
            return new LookupItemDto
            {
                Id = entity.Id.ToString(),
                Name = entity.GetAttributeValue<string>(DataverseModel.VSd_Court.Fields.VSd_Name),
            };
        }

        public static LookupResponseDto<T> ToLookupResponse<T>(IQueryable<T> items)
        {
            return new LookupResponseDto<T> { Value = items.ToList() };
        }
    }
}
