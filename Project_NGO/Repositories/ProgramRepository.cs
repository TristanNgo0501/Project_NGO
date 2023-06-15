using Project_NGO.Requests;
using Project_NGO.Requests.Program;
using Project_NGO.Responses;

namespace Project_NGO.Repositories
{
    public interface ProgramRepository
    {
        Task<List<ProgramResponse>> GetProgramList();

        Task<ProgramResponse> GetProgramById(int id);

        Task<ProgramResponse> AddProgram(ProgramDTO programsDto, IFormFile? formFile);

        Task<ProgramResponse> UpdateProgram(ProgramUpdateDTO programsDto, int id, IFormFile? formFile);

        Task<bool> DeleteProgram(int id);

        Task<List<ProgramResponse>> GetProgramsWithStatus(string status);
    }
}