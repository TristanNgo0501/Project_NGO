namespace Project_NGO.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public int? User_Id { get; set; }
        public int? Program_Id { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Accounting Accounting { get; set; }
        public User? User { get; set; }
        public Programs? Programs { get; set; }
    }
}