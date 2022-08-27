using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class BlogAddViewModel
    {
        [Required]
        public string BlogName { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Minimum 1 Karakter Girininiz...!")]
        public string BlogDescription { get; set; }
        [Required]
        [MinLength(1,ErrorMessage = "Minimum 1 Karakter Girininiz...!")]
        [MaxLength(200,ErrorMessage = "Maximum 200 Karakter Girininiz...!")]
        public string BlogShortDescription { get; set; }
        [Required]
        public DateTime BlogDate { get; set; }
        [Required]
        public int AppUserId { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string? ThumbNailImageURl { get; set; }
        [Required]
        public IFormFile ThumbNailImage { get; set; }
    }
}
