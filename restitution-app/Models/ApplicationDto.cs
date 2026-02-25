using System;
using Gov.Cscp.VictimServices.Public.Utilities.Converters;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class ApplicationDto
    {
        public int ApplicantType { get; set; }
        public string ApplicantsFirstName { get; set; }
        public string ApplicantsMiddleName { get; set; }
        public string ApplicantsLastName { get; set; }
        public string OtherFirstName { get; set; }
        public string OtherLastName { get; set; }
        public int? ApplicantsGenderCode { get; set; }
        public DateTime? ApplicantsBirthDate { get; set; }
        public int? Indigenous { get; set; }
        public int? ApplicantsPreferredMethodOfContact { get; set; }
        public int? SmsPreferred { get; set; }
        public string ApplicantsPrimaryPhoneNumber { get; set; }
        public string ApplicantsAlternatePhoneNumber { get; set; }
        public string ApplicantsEmail { get; set; }
        public string ApplicantsPrimaryAddressLine1 { get; set; }
        public string ApplicantsPrimaryAddressLine2 { get; set; }
        public string ApplicantsPrimaryAddressLine3 { get; set; }
        public string ApplicantsPrimaryCity { get; set; }
        public string ApplicantsPrimaryProvince { get; set; }
        public string ApplicantsPrimaryPostalCode { get; set; }
        public string ApplicantsPrimaryCountry { get; set; }
        public string OffenderFirstName { get; set; }
        public string OffenderMiddleName { get; set; }
        public string OffenderLastName { get; set; }
        public int? VoicemailOption { get; set; }
        public string ApplicantsSignature { get; set; }
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
