namespace Project_NGO.Models
{
    public enum ProgramStatus
    {
        UP_COMING = 1,

        COMING = 2,

        CLOSE = 3,
    }

    public class Programs
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public string? Image { get; set; }

        public string? Status { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Category? Category { get; set; }

        public virtual List<Program_Image>? ProgramImages { get; set; }

        public virtual List<Receipt>? Receipt { get; set; }
    }
}