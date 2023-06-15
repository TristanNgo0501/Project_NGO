namespace Project_NGO.Responses
{
    public class AboutResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Account_Name { get; set; }
        public string? Account_Number { get; set; }
        public string? Account_Bank { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}