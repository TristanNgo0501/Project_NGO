using AutoMapper;
using Project_NGO.Models;

namespace Project_NGO.Requests.Program;

public class ProgramProfile:Profile
{
    public ProgramProfile()
    {
        CreateMap<Programs, ProgramDTO>();
        CreateMap<ProgramDTO, Programs>();
    }
}