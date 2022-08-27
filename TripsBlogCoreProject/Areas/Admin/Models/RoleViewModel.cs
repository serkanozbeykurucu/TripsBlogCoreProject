using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen Rol Adı Giriniz")]
        public string name { get; set; }
    }
}
