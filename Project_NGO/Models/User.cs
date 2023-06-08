using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Project_NGO.Models
{
    public class User:IdentityUser<int>
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public string? CustomEmail { get; set; }
        public string? Password { get; set; }
        public string? Rank { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? Region { get; set; }
        public string? Role { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Receipt> Receipt { get; set; }
    }
}