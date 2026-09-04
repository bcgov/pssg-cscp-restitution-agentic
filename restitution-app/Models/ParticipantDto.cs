using System.ComponentModel.DataAnnotations;
using Gov.Cscp.VictimServices.Public.Utilities.Converters;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class ParticipantDto
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string PreferredName { get; set; }

        [MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string AddressLine1 { get; set; }

        [MaxLength(200)]
        public string AddressLine2 { get; set; }

        [MaxLength(200)]
        public string AddressLine3 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Province { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        public int? PreferredMethodOfContact { get; set; }

        public int? RestContactPreferenceForUpdates { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string AlternatePhoneNumber { get; set; }

        public int? VoicemailOptions { get; set; }

        [MaxLength(254)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string CustodyLocation { get; set; }

        [MaxLength(200)]
        public string ProgramName { get; set; }

        [MaxLength(100)]
        public string Relationship1 { get; set; }

        [MaxLength(100)]
        public string Relationship2 { get; set; }

        [MaxLength(200)]
        public string Relationship2Other { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string ContactTitle { get; set; }

        public int? SmsPreferred { get; set; }

        public int? IsPrimaryEntityContact { get; set; }
    }
}
