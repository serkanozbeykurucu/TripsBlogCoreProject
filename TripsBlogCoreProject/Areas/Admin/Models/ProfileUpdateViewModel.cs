using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class ProfileUpdateViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public bool IsChecked { get; set; }
    }
}
