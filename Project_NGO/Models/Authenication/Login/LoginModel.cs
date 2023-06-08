using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models.Authenication.Login
{
    public class LoginModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Role { get; set; }

    }
}
