using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;


namespace Project_NGO.Models
{
    public class Programs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Budget is required")]
        public decimal? Budget { get; set; }
        public string? Image { get; set; }
        
        public string? Status { get; set; }
        public int Category_Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Program_Image>? Program_Images { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Receipt>? Receipt { get; set; }
    }
}