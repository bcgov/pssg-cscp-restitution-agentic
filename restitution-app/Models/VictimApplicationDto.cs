using System;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class VictimApplicationDto : RestitutionApplicationDtoBase
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }
    }
}
