using Gov.Cscp.VictimServices.Public.Utilities.Converters;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class ParticipantDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PreferredName { get; set; }

        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public int? PreferredMethodOfContact { get; set; }

        public int? RestContactPreferenceForUpdates { get; set; }

        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public int? VoicemailOptions { get; set; }

        public string Email { get; set; }

        public string CustodyLocation { get; set; }

        public string ProgramName { get; set; }

        public string Relationship1 { get; set; }

        public string Relationship2 { get; set; }

        public string Relationship2Other { get; set; }

        public string Title { get; set; }

        public string ContactTitle { get; set; }

        public int? SmsPreferred { get; set; }

        public int? IsPrimaryEntityContact { get; set; }
    }
}
