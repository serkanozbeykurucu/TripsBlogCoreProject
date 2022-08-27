namespace TripsBlogCoreProject.Areas.Writer.Models
{
    public class BlogAddViewModel
    {
        public string BlogName { get; set; } //1
        public string BlogDescription { get; set; } //1
        public string BlogShortDescription { get; set; } //1
        public DateTime BlogDate { get; set; } //1
        public int AppUserId { get; set; }
        public int CategoryId { get; set; }

        // resim işlemleri
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }

        public string ThumnailImageUrl { get; set; }
        public IFormFile ThumnailImage { get; set; }

    }
}
