using System.ComponentModel.DataAnnotations;

namespace TripsBlogCoreProject.Models
{
    public class WriterSignUpModel
    {
        [Required]
        [StringLength(50,ErrorMessage = "Adınız 50 Karakterden fazla Olamaz...!")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Soyadınız 50 Karakterden fazla Olamaz...!")]
        public string LastName { get; set; }
        [StringLength(30, ErrorMessage = "Kullanıcı adınız 30 Karakterden fazla Olamaz...!")]
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        [Required]
        [Phone]
        [StringLength(15, ErrorMessage = "Telefon Numaranız 15 Karakterden fazla Olamaz...!")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifreler Uyumlu Değildir.!")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
