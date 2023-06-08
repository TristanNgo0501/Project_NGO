using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models.Authenication.Login
{
    public class InfoUserLogin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
