using System;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public abstract class RestitutionApplicationDtoBase
    {
        [MaxLength(100)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        public string OtherFirstName { get; set; }

        [MaxLength(100)]
        public string OtherLastName { get; set; }

        public int? Gender { get; set; }

        public int? IndigenousStatus { get; set; }

        public int? PreferredMethodOfContact { get; set; }

        public int? SmsPreferred { get; set; }

        [MaxLength(30)]
        public string PrimaryPhoneNumber { get; set; }

        [MaxLength(30)]
        public string AlternatePhoneNumber { get; set; }

        [MaxLength(254)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string PrimaryAddressLine1 { get; set; }

        [MaxLength(200)]
        public string PrimaryAddressLine2 { get; set; }

        [MaxLength(200)]
        public string PrimaryAddressLine3 { get; set; }

        [Required]
        [MaxLength(100)]
        public string PrimaryCity { get; set; }

        [Required]
        [MaxLength(100)]
        public string PrimaryProvince { get; set; }

        [Required]
        [MaxLength(20)]
        public string PrimaryPostalCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string PrimaryCountry { get; set; }

        [MaxLength(100)]
        public string OffenderFirstName { get; set; }

        [MaxLength(100)]
        public string OffenderMiddleName { get; set; }

        [MaxLength(100)]
        public string OffenderLastName { get; set; }

        public int? VoicemailOption { get; set; }

        [Required]
        [MaxLength(5_000_000)]
        public string Signature { get; set; }

        [MaxLength(200)]
        public string DeclarationFullName { get; set; }

        [MaxLength(100)]
        public string SigningOfficerTitle { get; set; }

        public DateTime? DeclarationDate { get; set; }

        [MaxLength(100)]
        public string ContactTitle { get; set; }

        [MaxLength(200)]
        public string OffenderCustodyLocation { get; set; }

        [MaxLength(200)]
        public string GenderIdentityText { get; set; }

        public int? PrimaryRaceEthnicity { get; set; }

        [MaxLength(200)]
        public string PrimaryRaceEthnicityText { get; set; }

        public int? Pronouns { get; set; }

        [MaxLength(100)]
        public string PronounsText { get; set; }
    }
}
