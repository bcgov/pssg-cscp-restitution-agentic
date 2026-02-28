namespace Gov.Cscp.VictimServices.Public.Models
{
    public class CreateOffenderRestitutionCaseRequestDto : RestitutionCaseRequestDtoBase
    {
        public OffenderApplicationDto Application { get; set; }
    }
}
