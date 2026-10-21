using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AspNetCoreWebSite.Models
{
    public class AIImage
    {
        public int Id { get; set; }

        [Display(Name = "File Name")]
        public string? FileName { get; set; }

        [Display(Name = "File Extension")]
        public string? FileExtension { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime? UploadDate { get; set; }

        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Prompt")]
        public string Prompt { get; set; }

        [Required] 
        [Display(Name = "MessageContent")]
        public string MessageContent { get; set; }

        [Display(Name = "Like")]
        public int Like { get; set; }

        [Display(Name = "File size")]
        public long? FileSize { get; set; }

        [JsonIgnore]
        public string StoragePath => Path.Combine("images", $"{Id}.{FileExtension}");
    }
}
