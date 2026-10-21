using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebSite.Models
{
    public class UploadFile
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
