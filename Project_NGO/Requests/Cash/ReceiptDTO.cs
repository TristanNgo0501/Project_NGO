namespace Project_NGO.Requests.Cash
{
    public class ReceiptDTO
    {
        public decimal Money { get; set; }
        public int UserId { get; set; }
        public int? ProgramId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}