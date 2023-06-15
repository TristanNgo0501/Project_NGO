using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Models.Authenication.Register;
using Project_NGO.Models.Authenication;
using Project_NGO.Models.UserModel;
using Project_NGO.Repositories.UploadFileRepo;
using Project_NGO.Repositories.UserRepo;
using System.Security.Policy;
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
        public async Task<bool> DeleteUserAsync(string email)
        {
            var userExist = await dbcontext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            if(userExist != null)
            {
                if (userExist.Image != null && userExist.Image != "")
                {
                    await fileRepo.DeleteFile(userExist.Image);
                }
                dbcontext.Users.Remove(userExist);
                await dbcontext.SaveChangesAsync();
                return true;
            } else { return false; }
        }

        public async Task<IEnumerable<UserModel>> GetListUserAsync()
        {
            var list = await dbcontext.Users.Where(u => u.Role != RoleModel.Admin && u.Role != RoleModel.Manager)
                                            .Select(u => new UserModel
                                            {
                                                Id = u.Id,
                                                Name = u.Name,
                                                Email = u.Email,
                                                Rank = u.Rank,
                                                Address = u.Address,
                                                Phone = u.Phone,
                                                Image = u.Image,
                                                Region = u.Region,
                                                Role = u.Role,
                                                Status = u.Status
                                            })
                                            .ToListAsync();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return null;
        }

        public async Task<IEnumerable<InforUserReceipt>> GetListUserReceipt()
        {
            var list = await dbcontext.Users.Select(u => new InforUserReceipt
                                            {
                                                Id=u.Id,
                                                Name=u.Name,
                                                Email=u.Email,
                                                Role = u.Role,
                                            })
                                            .ToListAsync();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return null;
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
                    userExist.Address = userModel.Address;
                    userExist.Phone = userModel.Phone;
                    userExist.Image = "http://localhost:5065/Users/" + fileName;
                    userExist.Region = userModel.Region;
                    userExist.Status = userModel.Status;
                    CheckUserModelNull(userExist);
                     dbcontext.Users.Update(userExist);
                    await dbcontext.SaveChangesAsync();
                    return userExist;
                }
                else
                {
                    userExist.Name = userModel.Name;
                    userExist.Address = userModel.Address;
                    userExist.Phone = userModel.Phone;
                    userExist.Region = userModel.Region;
                    userExist.Status = userModel.Status;
                    CheckUserModelNull(userExist);
                    dbcontext.Users.Update(userExist);
                    await dbcontext.SaveChangesAsync();
                    return userExist;

                }
            } else { return null; }
        }

        private User CheckUserModelNull(User user)
        {
            var entry = dbcontext.Entry(user);
            if (user.Address == null || user.Address == "null")
                entry.Property(u => u.Address).IsModified = false;
            if (user.Phone == null || user.Phone == "null")
                entry.Property(u => u.Phone).IsModified = false;
            if (user.Region == null || user.Region == "null")
                entry.Property(u => u.Region).IsModified = false;
            return user;
        }
    }
}
