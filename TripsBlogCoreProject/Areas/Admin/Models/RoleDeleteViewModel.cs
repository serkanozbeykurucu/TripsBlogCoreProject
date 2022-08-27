using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class RoleDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
    }
}
