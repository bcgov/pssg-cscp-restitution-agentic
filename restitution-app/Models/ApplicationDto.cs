using System;
using Gov.Cscp.VictimServices.Public.Utilities.Converters;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class ApplicationDto
    {
        public int Applicanttype { get; set; }

        public string Applicantsfirstname { get; set; }

        public string Applicantsmiddlename { get; set; }

        public string Applicantslastname { get; set; }

        public string Otherfirstname { get; set; }

        public string Otherlastname { get; set; }

        public int? Applicantsgendercode { get; set; }

        public DateTime? Applicantsbirthdate { get; set; }

        public int? Indigenous { get; set; }

        public int? Applicantspreferredmethodofcontact { get; set; }

        public int? Smspreferred { get; set; }

        public string Applicantsprimaryphonenumber { get; set; }

        public string Applicantsalternatephonenumber { get; set; }

        public string Applicantsemail { get; set; }

        public string Applicantsprimaryaddressline1 { get; set; }

        public string Applicantsprimaryaddressline2 { get; set; }

        public string Applicantsprimaryaddressline3 { get; set; }

        public string Applicantsprimarycity { get; set; }

        public string Applicantsprimaryprovince { get; set; }

        public string Applicantsprimarypostalcode { get; set; }

        public string Applicantsprimarycountry { get; set; }

        public string Offenderfirstname { get; set; }

        public string Offendermiddlename { get; set; }

        public string Offenderlastname { get; set; }

        public int? Voicemailoption { get; set; }

        public string Applicantssignature { get; set; }

        public string Declarationfullname { get; set; }

        public string Signingofficertitle { get; set; }

        public DateTime? Declarationdate { get; set; }

        public string Contacttitle { get; set; }

        public string Offendercustodylocation { get; set; }

        public string Genderidentitytext { get; set; }

        public int? Primaryraceethnicity { get; set; }

        public string Primaryraceethnicitytext { get; set; }

        public int? Pronouns { get; set; }

        public string Pronountext { get; set; }
    }
}
