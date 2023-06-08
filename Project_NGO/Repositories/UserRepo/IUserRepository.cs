using Project_NGO.Models;
using Project_NGO.Models.UserModel;

namespace Project_NGO.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListUserAsync();
        Task<UserModel> GetUserAsync(string email);
        Task<User> UpdateUserAsync(UserModel userModel, IFormFile? photo);
        Task<bool> DeleteUserAsync(int id);
    }
}
