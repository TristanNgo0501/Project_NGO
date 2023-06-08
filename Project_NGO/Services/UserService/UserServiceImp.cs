using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Models.UserModel;
using Project_NGO.Repositories.UploadFileRepo;
using Project_NGO.Repositories.UserRepo;
using System.Xml.Linq;

namespace Project_NGO.Services.UserService
{
    public class UserServiceImp : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly DatabaseContext dbcontext;
        private readonly IFileRepository fileRepo;
        public UserServiceImp(UserManager<User> _userManager, DatabaseContext _dbcontext, IFileRepository _fileRepo)
        {
            userManager = _userManager;
            dbcontext = _dbcontext;
            fileRepo = _fileRepo;
        }
        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetListUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var userModel = new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Rank = user.Rank,
                    Address = user.Address,
                    Phone = user.Phone,
                    Image = user.Image,
                    Region = user.Region,
                    Role = user.Role,
                    Status = user.Status,
                };
                return userModel;
            } else { return null; }
        }

        public async Task<User> UpdateUserAsync(UserModel userModel, IFormFile? photo)
        {
            var userExist = await userManager.FindByEmailAsync(userModel.Email);
            if(userExist != null )
            {
                if (photo != null && photo.Length > 0)
                {
                    if(userExist.Image != null)
                    {
                        await fileRepo.DeleteFile(userExist.Image);
                    }
                    var fileName = await fileRepo.UploadFile(photo, "Users");
                    userExist.Name = userModel.Name;
                    userExist.Rank = userModel.Rank;
                    userExist.Address = userModel.Address;
                    userExist.Phone = userModel.Phone;
                    userExist.Image = "http://localhost:5065/Users/" + fileName;
                    userExist.Region = userModel.Region;
                    userExist.Status = userModel.Status;
                    await userManager.UpdateAsync(userExist);

                    return userExist;
                }
                else
                {
                    userExist.Name = userModel.Name;
                    userExist.Rank = userModel.Rank;
                    userExist.Address = userModel.Address;
                    userExist.Phone = userModel.Phone;
                    userExist.Region = userModel.Region;
                    userExist.Status = userModel.Status;
                    await userManager.UpdateAsync(userExist);
                    return userExist;

                }
            } else { return null; }
        }
    }
}
