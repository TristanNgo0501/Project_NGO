namespace Project_NGO.Responses
{
    public class CashResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public decimal Money { get; set; }
    }
}