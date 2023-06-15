namespace Project_NGO.Repositories.Abouts
{
    public interface IFile 
    {
        Task<string> UploadFile(IFormFile file);
        Task<bool> DeleteFile(string filePath);
    }
}
