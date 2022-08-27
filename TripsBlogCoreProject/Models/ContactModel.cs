using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Models
{
    public class ContactModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
