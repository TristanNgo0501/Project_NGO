using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models;
using Project_NGO.Models.Authenication;
using Project_NGO.Models.CustomResponse;
using Project_NGO.Models.UserModel;
using Project_NGO.Repositories.UserRepo;
using Project_NGO.Utils;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly UserManager<User> userManager;

        public UserController(IUserRepository _userRepo, UserManager<User> _userManager)
        {
            userRepo = _userRepo;
            userManager = _userManager;
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            try
            {
                var result = await userRepo.GetUserAsync(email);
                if (result != null)
                {
                    return CustomMethodResponse.GetResponse200Ok(result, "Get user success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get user fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);

            }
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<User>> UpdateUser([FromForm] UserModel userModel, IFormFile? photo)
        {
            try
            {
                var result = await userRepo.UpdateUserAsync(userModel, photo);
                if(result != null)
                {
                    return CustomMethodResponse.GetResponse200Ok(result, "Update user success");
                } else
                {
                    return CustomMethodResponse.GetResponse400BadResquest("Update user fail");
                }

            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<User>> DeleteEmployee(string email)
        {
            try
            {
                var result = await userRepo.DeleteUserAsync(email);
                if(result)
                {
                    return CustomMethodResponse.GetResponse200Ok(result, "Delete user success");
                } else
                {
                    return CustomMethodResponse.GetResponse400BadResquest("Delete user fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListUser()
        {
            try
            {
                var result = await userRepo.GetListUserAsync();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetListUserReceipt()
        {
            try
            {
                var result = await userRepo.GetListUserReceipt();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

    }
}
