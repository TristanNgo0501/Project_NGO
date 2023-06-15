
using Microsoft.AspNetCore.Identity;
using Project_NGO.Models;
using Project_NGO.Models.Authenication.Email;
using Project_NGO.Models.Authenication.Login;
using Project_NGO.Models.Authenication.Register;


namespace Project_NGO.Repositories.Authenication
{
    public interface IAuthRepository
    {
        Task<IEnumerable<ListAdmin>> ListAdmin();
        Task<RegisterModel> Register(RegisterModel model);
        Task<string> Login(LoginModel model);
        Task SendMail(MailConfig mailConfig);
        Task<User> ChangePassword(ChangePassword changePassword);
        Task<User> ResetPassword(string email);
    }
}
