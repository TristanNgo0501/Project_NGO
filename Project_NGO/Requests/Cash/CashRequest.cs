namespace Project_NGO.Requests.Cash
{
    public class CashRequest
    {
        public decimal money { get; set; }
        public int? programId { get; set; }
        public int userId { get; set; }
    }
}