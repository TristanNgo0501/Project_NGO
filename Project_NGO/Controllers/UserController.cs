using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models;
using Project_NGO.Models.Authenication;
using Project_NGO.Models.UserModel;
using Project_NGO.Repositories.UserRepo;

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
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "Not found list admin" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });

            }
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<User>> UpdateUser([FromForm] UserModel userModel, IFormFile? photo)
        {
            try
            {
                var resources = await userRepo.UpdateUserAsync(userModel, photo);
                if(resources != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "200", Message = "Update Success" });
                } else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Update Fail" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }

        }
    }
}
