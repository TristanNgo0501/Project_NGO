using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }
        [Url(ErrorMessage = "Invalid image URL.")]
        public string? Image { get; set; }
        public DateTime? CreatedAt {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime? UpdatedAt {
            get
            {
                return DateTime.Now;
            }
        }
        public List<Programs>? Programs { get; set; }
    }
}