using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class LookupDataDto
    {
        public IList<CountryLookupDto> countries { get; set; }
        public IList<ProvinceLookupDto> provinces { get; set; }
        public IList<CityLookupDto> cities { get; set; }
        public IList<RelationshipLookupDto> relationships { get; set; }
        public IList<RelationshipLookupDto> representativeRelationships { get; set; }
        public IList<CourtLookupDto> courts { get; set; }
        public IList<PoliceDetachmentLookupDto> police_detachments { get; set; }
    }

    public class CountryLookupDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ProvinceLookupDto
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("countryId")]
        public string CountryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CityLookupDto
    {
        [JsonProperty("countryId")]
        public string CountryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("provinceId")]
        public string ProvinceId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class RelationshipLookupDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CourtLookupDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class PoliceDetachmentLookupDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class LookupItemDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class LookupResponseDto<T>
    {
        [JsonProperty("value")]
        public IList<T> Value { get; set; }
    }

    public class CitySearchResponseDto
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("cityCollection")]
        public IList<CityLookupDto> CityCollection { get; set; }

        [JsonProperty("countryCollection")]
        public IList<CountryLookupDto> CountryCollection { get; set; }

        [JsonProperty("provinceCollection")]
        public IList<ProvinceLookupDto> ProvinceCollection { get; set; }
    }
}
