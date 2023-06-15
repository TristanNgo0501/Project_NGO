namespace Project_NGO.Requests.Program
{
    public class ProgramUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
    }
}