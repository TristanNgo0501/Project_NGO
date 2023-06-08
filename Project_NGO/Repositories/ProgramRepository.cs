using Project_NGO.Models;

namespace Project_NGO.Repositories
{
    public interface ProgramRepository
    {
        Task<List<Programs>> GetProgramList();
        Task<Programs> GetProgramById(int id);
        Task<Programs> AddProgram(Programs programs, IFormFile? formFile);
        Task<Programs> UpdateProgram(Programs programs, IFormFile? formFile);
        Task<bool> DeleteProgram(int id);
    }
}
