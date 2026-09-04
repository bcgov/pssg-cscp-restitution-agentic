using System.ComponentModel.DataAnnotations;
using Gov.Cscp.VictimServices.Public.Utilities.Converters;

namespace Gov.Cscp.VictimServices.Public.Models
{
    public class DocumentDto
    {
        [MaxLength(255)]
        public string Filename { get; set; }

        // Base64 document body — large but finite to bound request size
        [MaxLength(20_000_000)]
        public string Body { get; set; }

        [MaxLength(500)]
        public string Subject { get; set; }
    }
}
