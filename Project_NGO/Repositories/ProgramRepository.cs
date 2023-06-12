using Project_NGO.Requests;

namespace Project_NGO.Repositories
{
    public interface ProgramRepository
    {
        Task<List<ProgramDTO>> GetProgramList();

        Task<ProgramDTO> GetProgramById(int id);

        Task<ProgramDTO> AddProgram(ProgramDTO programsDto, IFormFile? formFile);

        Task<ProgramDTO> UpdateProgram(ProgramDTO programsDto, int id, IFormFile? formFile);

        Task<bool> DeleteProgram(int id);
    }
}