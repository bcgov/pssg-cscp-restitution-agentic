using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class VictimEntityApplicationDto : RestitutionApplicationDtoBase
    {
        [Required]
        public string EntityName { get; set; }
    }
}
