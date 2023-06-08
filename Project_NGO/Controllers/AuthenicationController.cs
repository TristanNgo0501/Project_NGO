using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models.Authenication.Login;
using Project_NGO.Models.Authenication.Register;
using Project_NGO.Models.Authenication;
using Project_NGO.Models;
using Project_NGO.Repositories.Authenication;
using Project_NGO.Models.Authenication.Email;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenicationController : ControllerBase
    {
        private readonly IAuthRepository authRepo;
        private readonly UserManager<User> userManager;

        public AuthenicationController(IAuthRepository _authRepo, UserManager<User> _userManager)
        {
            authRepo = _authRepo;
            userManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var result = await authRepo.Register(registerModel);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created, new Response { Status = "201", Message = "Create is success" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Create is fail" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var token = await authRepo.Login(loginModel);
            if (token != null)
            {
                var user = await userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    var inforUser = new InfoUserLogin
                    {
                        Id = user.Id,
                        Image = user.Image,
                        Name = user.Name,
                        Email = user.Email,
                        Role = user.Role
                    };
                    if(loginModel.Role != null)
                    {
                        if(loginModel.Role.Equals(RoleModel.Admin))
                        {
                            if(user.Role.Equals(RoleModel.Person) || user.Role.Equals(RoleModel.Organization))
                            {
                                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "This account has not been registered, please contact the manager to get an account" });
                            }
                        }   
                    }
                    return Ok(new { token, inforUser });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Login fail" });
        }

        [HttpGet]
        public async Task<IActionResult> ListAdmin()
        {
            var result = await authRepo.ListAdmin();
            if(result != null)
            {
                return Ok(result);
            } else 
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "Not found list admin" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(MailConfig mailConfig)
        {
            try
            {
                var result = authRepo.SendMail(mailConfig);
                
                if (result.IsCompleted)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "200", Message = "Send email success" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Send mail fail" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }

        }

        [HttpPut("{email}")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var result = await authRepo.ChangePassword(changePassword);

                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "200", Message = "Change Password Success" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Change Password Fail" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }

        }

    }
}
