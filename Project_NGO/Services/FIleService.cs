using Project_NGO.Repositories;

namespace Project_NGO.Services;

public class FIleService:IFileRepository
{
    private readonly string _uploadFolder;

    public FIleService(IWebHostEnvironment webHostEnvironment)
    {
        _uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Upload");
        if (!Directory.Exists(_uploadFolder))
        {
            Directory.CreateDirectory(_uploadFolder);
        }
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        string fileName = Path.GetFileName(file.FileName);
        string filePath = Path.Combine(_uploadFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return fileName;
    }

    public Task<bool> DeleteFile(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            string filePathUpdate = Path.Combine(_uploadFolder, filePath);
            if (File.Exists(filePathUpdate))
            {
                File.Delete(filePathUpdate);
            }
        }

        return Task.FromResult(true);
    }
}