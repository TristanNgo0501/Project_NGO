using Project_NGO.Models;

namespace Project_NGO.Responses
{
    public class ReceiptResponse
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public int UserId { get; set; }
        public int? ProgramId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReceiptType Type { get; set; }
    }
}