using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Models
{
    public class WriterSignInModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
