namespace Gov.Cscp.VictimServices.Public.Models
{
    public class CreateVictimRestitutionCaseRequestDto : RestitutionCaseRequestDtoBase
    {
        public VictimApplicationDto Application { get; set; }
    }
}
