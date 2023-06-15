namespace Project_NGO.Repositories.UploadFileRepo
{
    public interface IFileRepository
    {
        Task<string> UploadFile(IFormFile file, string? folder);
        Task<bool> DeleteFile(string filePath);
    }
}
