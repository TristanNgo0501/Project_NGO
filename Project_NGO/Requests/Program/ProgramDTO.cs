using System.ComponentModel.DataAnnotations;

namespace Project_NGO.Requests;

public class ProgramDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Budget is required")]
    public decimal? Budget { get; set; }
    [Required(ErrorMessage = "Image is required")]
    public string? Image { get; set; }
    [Required(ErrorMessage = "Status is required")]    
    public string? Status { get; set; }
    public int Category_Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}