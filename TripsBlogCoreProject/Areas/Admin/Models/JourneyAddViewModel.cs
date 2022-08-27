namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class JourneyAddViewModel
    {
        public string JourneyName { get; set; }
        public string? ThumbNailUrl { get; set; }
        public IFormFile ThumbNail { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string JourneyShortDescription { get; set; }
        public string JourneyDescription { get; set; }
    }
}
