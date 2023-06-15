using Project_NGO.Repositories.Abouts;

namespace Project_NGO.Services.Abouts
{
    public class FileServiceImp : IFile
    {
        private readonly string _uploadFolder;

        public FileServiceImp(IWebHostEnvironment webHostEnvironment)
        {
            _uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads"); //  dùng để tạo thư mục Uploads
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName); // // Tạo tên file duy nhất
            string filePath = Path.Combine(_uploadFolder, fileName); // tạo 1 đường dẫn đến file

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Copy nội dung của file vào stream
            }

            return fileName; // // Trả về tên file đã tải lên
        }

        public async Task<bool> DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string getFileName = filePath.Substring(9);
                string filePathExist = Path.Combine(_uploadFolder, getFileName);
                if (File.Exists(filePathExist))
                {
                    File.Delete(filePathExist);
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
    }
}
