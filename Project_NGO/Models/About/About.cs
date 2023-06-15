using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Models.About
{
    public class About
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string? Account_Name { get; set; }

        [Required]
        public string? Account_Number { get; set; }

        [Required]
        public string? Account_Bank { get; set; }

        [Required]
        [Display(Name = "Enter QR Code Text")]
        public string? QR_Code { get; set; }

        public string? Image { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt
        {
            get
            {
                return DateTime.Now;
            }
        }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        public virtual List<About_Image>? About_Images { get; set; }
    }
}