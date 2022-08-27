namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class JourneyUpdateViewModel
    {
        public int ID { get; set; }
        public string JourneyName { get; set; }
        public string OldThumbNailUrl { get; set; }
        public string OldImageUrl { get; set; }
        public string JourneyShortDescription { get; set; }
        public string JourneyDescription { get; set; }
        public bool Status { get; set; }

        //yeni resimler
        public string? ThumbNailUrl { get; set; }
        public IFormFile? ThumbNail { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
