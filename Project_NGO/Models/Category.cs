using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Programs>? Programs { get; set; }
    }
}