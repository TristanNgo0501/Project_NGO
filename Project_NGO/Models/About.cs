namespace Project_NGO.Models
{
    public class About
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Account_Name { get; set; }
        public string? Account_Number { get; set; }
        public string? Account_Bank { get; set; }
        public string? QR_Code { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<About_Image> About_Images { get; set; }
    }
}