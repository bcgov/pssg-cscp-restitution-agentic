using Gov.Cscp.VictimServices.Public.ViewModels;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class CreateRestitutionCaseRequestDto
    {
        public ApplicationDto Application { get; set; }
        public CourtInfoDto[] CourtInfoCollection { get; set; }
        public ParticipantDto[] ProviderCollection { get; set; }
        public DocumentDto[] DocumentCollection { get; set; }
    }
}
