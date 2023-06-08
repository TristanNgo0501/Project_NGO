using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories;
using Project_NGO.Repositories.UploadFileRepo;

namespace Project_NGO.Services;

public class ProgramService:ProgramRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly IFileRepository _fileRepository;
    private readonly string _uploadFolder;

    public ProgramService(DatabaseContext databaseContext, IFileRepository repository,IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        _fileRepository = repository;
        _uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Upload");
    }

    public async Task<List<Programs>> GetProgramList()
    {
        return await  _databaseContext.Programs.ToListAsync();
    }

    public async Task<Programs> GetProgramById(int id)
    {
        var pro = await _databaseContext.Programs.SingleOrDefaultAsync(p => p.Id == id);
        if (pro != null)
        {
            return pro;
        }

        return null;
    }

    public async  Task<Programs> AddProgram(Programs programs, IFormFile? file)
    {
        if (file != null && file.Length > 0)
        {
            var fileName = _fileRepository.UploadFile(file, "Programs");
            programs.Image = "http://localhost:5065/Programs/" + fileName;
        }

        await _databaseContext.Programs.AddAsync(programs);
        await _databaseContext.SaveChangesAsync();
        return programs;
    }

    public async Task<Programs> UpdateProgram(Programs programs, IFormFile? file)
    {
        var pro = await  _databaseContext.Programs.SingleOrDefaultAsync(p => p.Id == programs.Id);
        if (file != null && file.Length >0)
        {
            _fileRepository.DeleteFile(pro.Image);
            var fileName = _fileRepository.UploadFile(file, "Programs");
            programs.Image = "http://localhost:5065/Programs/" + fileName;
            _databaseContext.Entry(programs).State = EntityState.Modified;
            await _databaseContext.SaveChangesAsync();
            return programs;
        }
        else
        {
            programs.Image = pro.Image;
            _databaseContext.Entry(programs).State = EntityState.Modified;
            await _databaseContext.SaveChangesAsync();
            return programs;
        }
    }

    public async Task<bool> DeleteProgram(int id)
    {
        var pro = await  _databaseContext.Programs.SingleOrDefaultAsync(p => p.Id == id);
        if (pro != null)
        {
            _fileRepository.DeleteFile(pro.Image);
            _databaseContext.Programs.Remove(pro);
            await _databaseContext.SaveChangesAsync();
        }

        return true;
    }
}