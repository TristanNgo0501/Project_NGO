using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_NGO.CustomStatusCode;
using Project_NGO.Data;
using Project_NGO.Models.About;
using Project_NGO.Repositories.Abouts;
using Project_NGO.Requests.Abouts;
using Project_NGO.Responses;
using Project_NGO.Services.Abouts;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAbout _aboutRepository;
        private readonly DatabaseContext _dbContext;
        private readonly AboutServiceImp _aboutServiceImp;

        public AboutController(IAbout aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AboutResponse>>> GetAbouts()
        {
            try
            {
                var resources = await _aboutRepository.GetAboutsAsync();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<AboutResponse>>(StatusCodes.Status200OK,
                        "Resources found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<AboutResponse>>(StatusCodes.Status404NotFound,
                        "No resources found", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<About>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomResult<AboutResponse>>> GetAbout(int id)
        {
            try
            {
                var resource = await _aboutRepository.GetAboutByIdAsync(id);
                if (resource == null)
                {
                    var response = new CustomResult<AboutResponse>(StatusCodes.Status404NotFound,
                        "Resource not found", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomResult<AboutResponse>(200,
                        "Get about successfully", resource, null);

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<About>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAbout([FromForm] RequestAddAbout about, IFormFile? photo, List<IFormFile>? files)
        {
            try
            {
                var resources = await _aboutRepository.AddAboutAsync(about, photo, files);
                return CustomHandleResultJson.PostActionJson(resources);
            }
            catch (DbUpdateException ex)
            {
                return CustomHandleResultJson.ErrorActionResult<About>(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AboutResponse>> UpdateAbout([FromForm] RequestUpdateAbout about, int id, IFormFile? photo)
        {
            try
            {
                var resource = await _aboutRepository.UpdateAboutAsync(
                        about, id, photo);
                if (resource != null)
                {
                    var response = new CustomResult<AboutResponse>(StatusCodes.Status200OK,
                        "update about successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<AboutResponse>(StatusCodes.Status400BadRequest,
                        "Invalid Request", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<AboutResponse>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResult<string>>> DeleteAbout(int id)
        {
            bool resourceDeleted = false;
            var resource = await _aboutRepository.GetAboutByIdAsync(id);

            if (resource != null)
            {
                resourceDeleted = await _aboutRepository.DeleteAboutAsync(id);
            }
            if (resourceDeleted)
            {
                var response = new CustomResult<string>(200,
                    "Resource deleted successfully", null, null);
                return Ok(response);
            }
            else
            {
                var response = new CustomResult<string>(200,
                    "Resource not found or unable to delete", null, null);
                return NotFound(response);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<CustomResult<byte[]>>> GetQRCode(int id)
        {
            try
            {
                var resource = await _aboutRepository.GetQRCode(id);
                if (resource == null)
                {
                    var response = new CustomResult<byte[]>(404,
                        "Resource not found", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomResult<byte[]>(200,
                        "Get about successfully", resource, null);

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResult<About>()
                {
                    Message = "An error occurred while retrieving the model.",
                    Error = ex.Message
                });
            }
        }
    }
}