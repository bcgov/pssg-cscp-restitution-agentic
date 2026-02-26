using System.Collections.Generic;

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
        public string vsd_name { get; set; }
        public string vsd_countryid { get; set; }
    }

    public class ProvinceLookupDto
    {
        public string vsd_code { get; set; }
        public string _vsd_countryid_value { get; set; }
        public string vsd_name { get; set; }
        public string vsd_provinceid { get; set; }
    }

    public class CityLookupDto
    {
        public string _vsd_countryid_value { get; set; }
        public string vsd_name { get; set; }
        public string _vsd_stateid_value { get; set; }
        public string vsd_cityid { get; set; }
    }

    public class RelationshipLookupDto
    {
        public string vsd_name { get; set; }
        public string vsd_relationshipid { get; set; }
    }

    public class CourtLookupDto
    {
        public string vsd_name { get; set; }
        public string vsd_courtid { get; set; }
    }

    public class PoliceDetachmentLookupDto
    {
        public string vsd_name { get; set; }
        public string vsd_policedetachmentid { get; set; }
    }

    public class LookupItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class LookupResponseDto<T>
    {
        public IList<T> Value { get; set; }
    }

    public class CitySearchResponseDto
    {
        public string Result { get; set; }
        public IList<CityLookupDto> CityCollection { get; set; }
        public IList<CountryLookupDto> CountryCollection { get; set; }
        public IList<ProvinceLookupDto> ProvinceCollection { get; set; }
    }
}
