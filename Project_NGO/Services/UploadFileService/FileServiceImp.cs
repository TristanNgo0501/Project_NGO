using Project_NGO.Repositories.UploadFileRepo;

namespace Project_NGO.Services.UploadFileService
{
    public class FileServiceImp : IFileRepository
    {
        private readonly string upLoadFolder;
        private readonly string typeFolder;

        public FileServiceImp(IWebHostEnvironment webHostEnvironment)
        {
            upLoadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads");
            if (!Directory.Exists(upLoadFolder))
            {
                Directory.CreateDirectory(upLoadFolder);
            }
        }

        public async Task<bool> DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string baseUrl = "http://localhost:5065/";

                string filePathExist = Path.Combine(upLoadFolder, filePath.Substring(baseUrl.Length));
                if (File.Exists(filePathExist))
                {
                    File.Delete(filePathExist);
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }

        public async Task<string> UploadFile(IFormFile file, string? folder)
        {
            var typeFolder = Path.Combine(upLoadFolder, folder);
            if (!Directory.Exists(typeFolder))
            {
                Directory.CreateDirectory(typeFolder);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
            string pathFile = Path.Combine(typeFolder, fileName);
            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}