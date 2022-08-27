namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class TeamAddViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public string Resume { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
    }
}
