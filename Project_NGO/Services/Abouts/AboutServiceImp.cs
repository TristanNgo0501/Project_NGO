using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models.About;
using Project_NGO.Repositories.Abouts;

// using System.Web.Http.ModelBinding;
// using System.Web.Http.Results;
using Project_NGO.Repositories.UploadFileRepo;
using Project_NGO.Requests.Abouts;
using Project_NGO.Responses;
using System.Drawing.Imaging;

namespace Project_NGO.Services.Abouts
{
    public class AboutServiceImp : IAbout
    {
        private readonly DatabaseContext _dbContext;
        private readonly string _uploadFolder;
        private readonly IFileRepository _fileService;
        private readonly IMapper _mapper;

        public AboutServiceImp(DatabaseContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _dbContext = dbContext;
            _uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads/Abouts/");
            _mapper = mapper;
        }

        public async Task<IEnumerable<AboutResponse>> GetAboutsAsync()
        {
            var abouts = await _dbContext.Abouts.ToListAsync();
            var aboutResponse = _mapper.Map<IEnumerable<AboutResponse>>(abouts);
            return aboutResponse;
        }

        public async Task<AboutResponse> GetAboutByIdAsync(int id)
        {
            var about = await _dbContext.Abouts.FindAsync(id);
            var responseAbout = _mapper.Map<AboutResponse>(about);
            return responseAbout;
        }

        public async Task<byte[]> GetQRCode(int id)
        {
            // Lấy dữ liệu dựa trên Id (ví dụ: từ cơ sở dữ liệu hoặc bất kỳ nguồn nào khác)
            About about = await _dbContext.Abouts.FindAsync(id);

            // Tạo đối tượng mã QR
            var qrGenerator = new QRCoder.QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(about.QR_Code, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrCodeData);

            // Chuyển đổi mã QR thành hình ảnh
            var qrCodeImage = qrCode.GetGraphic(20);

            // Chuyển đổi hình ảnh thành byte array
            using (var stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                var imageBytes = stream.ToArray();

                return imageBytes;
            }
        }

        public async Task<AboutResponse> AddAboutAsync(RequestAddAbout aboutDto, IFormFile photo, List<IFormFile> files)
        {
            var about = _mapper.Map<About>(aboutDto);
            about.QR_Code = $"{about.Id}," +
                        $"{about.Name},{about.Description},{about.Account_Number},{about.Account_Bank},{about.Account_Name}";

            if (photo != null && photo.Length > 0)
            {
                // Generate unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                string filePath = Path.Combine(_uploadFolder, fileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                about.Image = "http://localhost:5065/Abouts/" + fileName;
            }
            about.CreatedAt = DateTime.Now;
            await _dbContext.Abouts.AddAsync(about);
            await _dbContext.SaveChangesAsync();

            if (files != null && files.Count > 0)
            {
                var uploadedFiles = new List<string>();

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(_uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        about.Image = "http://localhost:5065/Abouts/" + fileName;
                        //uploadedFiles.Add(fileName);
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
            var responseAbout = _mapper.Map<AboutResponse>(about);
            return responseAbout;
        }

        public async Task<AboutResponse> UpdateAboutAsync(RequestUpdateAbout aboutDto, int id, IFormFile photo)
        {
            var about = _mapper.Map<About>(aboutDto);
            about.Id = id;
            var aboutDb = await _dbContext.Abouts.FindAsync(id);
            if (photo != null && photo.Length > 0)
            {
                // Delete existing photo file if it exists
                if (!string.IsNullOrEmpty(aboutDb.Image))
                {
                    string filePathDemo = Path.Combine(_uploadFolder, aboutDb.Image);

                    if (File.Exists(filePathDemo))
                    {
                        File.Delete(filePathDemo);
                    }
                }

                // Generate unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                string filePath = Path.Combine(_uploadFolder, fileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                about.Image = "http://localhost:5065/Abouts/" + fileName;
            }
            else
            {
                about.Image = aboutDb.Image;
            }
            about.UpdatedAt = DateTime.Now;
            _dbContext.Entry(about).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            var responseAbout = _mapper.Map<AboutResponse>(about);
            return responseAbout;
        }

        public async Task<bool> DeleteAboutAsync(int id)
        {
            About about = await _dbContext.Abouts.FindAsync(id);

            if (about != null)
            {
                // Delete photo file if it exists
                if (!string.IsNullOrEmpty(about.Image))
                {
                    string filePath = Path.Combine(_uploadFolder, about.Image);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                _dbContext.Abouts.Remove(about);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}