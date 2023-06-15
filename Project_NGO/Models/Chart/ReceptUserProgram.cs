namespace Project_NGO.Models.Chart
{
    public class ReceptUserProgram
    {
        public int Id { get; set; }
        public decimal? Money { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ReceiptType ? Type { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public string? ProgramName { get; set; }
        public User? User { get; set; }
        public Programs? Program { get; set; }
        
    }
}
