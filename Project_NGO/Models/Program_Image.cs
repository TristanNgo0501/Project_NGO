namespace Project_NGO.Models
{
    public class Program_Image
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Programs? Programs { get; set; }
    }
}