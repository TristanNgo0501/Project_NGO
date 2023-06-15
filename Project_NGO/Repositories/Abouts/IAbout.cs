using Project_NGO.Models.About;

namespace Project_NGO.Repositories.Abouts
{
    public interface IAbout
    {
        Task<IEnumerable<About>> GetAboutsAsync();
        Task<About> GetAboutByIdAsync(int id);
        Task<About> AddAboutAsync(About about, IFormFile photo, List<IFormFile> files);  
        Task<About> UpdateAboutAsync(About about, IFormFile photo);
        Task<bool> DeleteAboutAsync(int id);
        Task<byte[]> GetQRCode(int id);
    }
}
