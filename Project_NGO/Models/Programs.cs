namespace Project_NGO.Models
{
    public class Programs
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Budget { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public int Category_Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Category? Category { get; set; }
        public List<Program_Image> Program_Images { get; set; }
        public List<Receipt> Receipt { get; set; }
    }
}