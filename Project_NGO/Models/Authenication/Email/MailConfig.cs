using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models.Authenication.Email
{
    public class MailConfig
    {
        [Required]
        public string ToMail { get; set; }
        [Required]
        public string Subject { get; set; }
        public string? Body { get; set; }
    }
}
