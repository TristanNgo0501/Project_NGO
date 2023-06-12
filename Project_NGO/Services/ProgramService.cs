using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories;
using Project_NGO.Repositories.UploadFileRepo;
using Project_NGO.Requests;

namespace Project_NGO.Services;

public class ProgramService : ProgramRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IFileRepository _fileRepository;
    private readonly IMapper _mapper;

    public ProgramService(DatabaseContext databaseContext, IFileRepository repository, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _fileRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProgramDTO>> GetProgramList()
    {
        var listPro = await _databaseContext.Programs.ToListAsync();
        if (listPro == null)
        {
            return null;
        }
        var listDto = _mapper.Map<List<ProgramDTO>>(listPro);
        return listDto;
    }

    public async Task<ProgramDTO> GetProgramById(int id)
    {
        var pro = await _databaseContext.Programs.SingleOrDefaultAsync(p => p.Id == id);
        if (pro == null)
        {
            return null;
        }
        var proDto = _mapper.Map<ProgramDTO>(pro);
        return proDto;
    }

    public async Task<ProgramDTO> AddProgram(ProgramDTO programDto, IFormFile? file)
    {
        var programs = _mapper.Map<Programs>(programDto);
        if (file != null && file.Length > 0)
        {
            var fileName = await _fileRepository.UploadFile(file, "Programs");
            programs.Image = "http://localhost:5065/Programs/" + fileName;
        }

        await _databaseContext.Programs.AddAsync(programs);
        await _databaseContext.SaveChangesAsync();
        var addedProgramDto = _mapper.Map<ProgramDTO>(programs);
        return addedProgramDto;
    }

    public async Task<ProgramDTO> UpdateProgram(ProgramDTO programDto, int id, IFormFile? file)
    {
        var programs = _mapper.Map<Programs>(programDto);
        programs.Id = id;
        var pro = await _databaseContext.Programs.SingleOrDefaultAsync(p => p.Id == programs.Id);
        if (pro == null)
        {
            return null;
        }
        ;

        if (file != null && file.Length > 0)
        {
            await _fileRepository.DeleteFile(pro.Image);
            var fileName = await _fileRepository.UploadFile(file, "Programs");
            programs.Image = "http://localhost:5065/Programs/" + fileName;
        }
        else
        {
            programs.Image = pro.Image;
        }
        _databaseContext.Entry(programs).State = EntityState.Modified;
        await _databaseContext.SaveChangesAsync();

        var updatedProgramDto = _mapper.Map<ProgramDTO>(programs);
        return updatedProgramDto;
    }

    public async Task<bool> DeleteProgram(int id)
    {
        var pro = await _databaseContext.Programs.SingleOrDefaultAsync(p => p.Id == id);
        if (pro != null)
        {
            await _fileRepository.DeleteFile(pro.Image);
            _databaseContext.Programs.Remove(pro);
            await _databaseContext.SaveChangesAsync();
        }

        return true;
    }
}