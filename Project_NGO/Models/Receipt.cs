namespace Project_NGO.Models
{
    public enum ReceiptType
    {
        Price_In = 1, Price_Out = 2,
    }

    public class Receipt
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public int UserId { get; set; }
        public int? ProgramId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReceiptType Type { get; set; }

        public User User
        { get; set; }

        public Programs? Programs { get; set; }
    }
}