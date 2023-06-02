namespace Project_NGO.Repositories;

public interface IFileRepository
{
    Task<string> UploadFile(IFormFile file);
    Task<bool> DeleteFile(string filePath);
}