using System;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public abstract class RestitutionApplicationDtoBase
    {
        public string MiddleName { get; set; }

        public string OtherFirstName { get; set; }

        public string OtherLastName { get; set; }

        public int? Gender { get; set; }

        public int? IndigenousStatus { get; set; }

        public int? PreferredMethodOfContact { get; set; }

        public int? SmsPreferred { get; set; }

        public string PrimaryPhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string PrimaryAddressLine1 { get; set; }

        public string PrimaryAddressLine2 { get; set; }

        public string PrimaryAddressLine3 { get; set; }

        [Required]
        public string PrimaryCity { get; set; }

        [Required]
        public string PrimaryProvince { get; set; }

        [Required]
        public string PrimaryPostalCode { get; set; }

        [Required]
        public string PrimaryCountry { get; set; }

        public string OffenderFirstName { get; set; }

        public string OffenderMiddleName { get; set; }

        public string OffenderLastName { get; set; }

        public int? VoicemailOption { get; set; }

        [Required]
        public string Signature { get; set; }

        public string DeclarationFullName { get; set; }

        public string SigningOfficerTitle { get; set; }

        public DateTime? DeclarationDate { get; set; }

        public string ContactTitle { get; set; }

        public string OffenderCustodyLocation { get; set; }

        public string GenderIdentityText { get; set; }

        public int? PrimaryRaceEthnicity { get; set; }

        public string PrimaryRaceEthnicityText { get; set; }

        public int? Pronouns { get; set; }

        public string PronounsText { get; set; }
    }
}
