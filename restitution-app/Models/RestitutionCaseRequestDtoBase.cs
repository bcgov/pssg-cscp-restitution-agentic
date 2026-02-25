namespace Gov.Cscp.VictimServices.Public.Models
{
    public abstract class RestitutionCaseRequestDtoBase
    {
        public CourtInfoDto[] CourtInfoCollection { get; set; }
        public ParticipantDto[] ProviderCollection { get; set; }
        public DocumentDto[] DocumentCollection { get; set; }
    }
}
