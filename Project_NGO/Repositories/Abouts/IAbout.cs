using Project_NGO.Requests.Abouts;
using Project_NGO.Responses;

namespace Project_NGO.Repositories.Abouts
{
    public interface IAbout
    {
        Task<IEnumerable<AboutResponse>> GetAboutsAsync();

        Task<AboutResponse> GetAboutByIdAsync(int id);

        Task<AboutResponse> AddAboutAsync(RequestAddAbout about, IFormFile photo, List<IFormFile> files);

        Task<AboutResponse> UpdateAboutAsync(RequestUpdateAbout about, int id, IFormFile photo);

        Task<bool> DeleteAboutAsync(int id);

        Task<byte[]> GetQRCode(int id);
    }
}