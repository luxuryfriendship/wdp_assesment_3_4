using System.ComponentModel.DataAnnotations;

namespace WDPAssessment3.Models
{
    public class UploadFile
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
