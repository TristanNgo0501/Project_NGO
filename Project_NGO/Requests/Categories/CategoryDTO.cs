namespace Project_NGO.Requests.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime? UpdatedAt
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
