namespace Project_NGO.Models
{
    public class Accounting
    {
        public int Id { get; set; }
        public decimal? Total_Price_In { get; set; }
        public decimal? Total_Price_Out { get; set; }
        public decimal? Remain_Money { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Receipt Receipt { get; set; }
    }
}