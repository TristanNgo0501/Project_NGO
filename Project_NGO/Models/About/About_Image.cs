namespace Project_NGO.Models.About
{
    public class About_Image
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image_Url { get; set; }
        public int? About_Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public About? About { get; set; }
    }
}