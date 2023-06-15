using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_NGO.CustomStatusCode;
using Project_NGO.Data;
using Project_NGO.Models.About;
using Project_NGO.Repositories.Abouts;
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
        public async Task<ActionResult<CustomResult<IEnumerable<About>>>> GetAbouts()
        {
            try
            {
                var resources = await _aboutRepository.GetAboutsAsync();
                if (resources != null && resources.Any())
                {
                    var response = new CustomResult<IEnumerable<About>>(200,
                        "Resources found", resources, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<IEnumerable<About>>(404,
                        "No resources found", null, null);
                    return (response);
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
        public async Task<ActionResult<CustomResult<About>>> GetAbout(int id)
        {
            try
            {
                var resource = await _aboutRepository.GetAboutByIdAsync(id);
                if (resource == null)
                {
                    var response = new CustomResult<About>(404,
                        "Resource not found", null, null);
                    return NotFound(response);
                }
                else
                {
                    var response = new CustomResult<About>(200,
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
        public async Task<IActionResult> AddAbout([FromForm] About about, IFormFile ? photo, List<IFormFile>? files)
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
        public async Task<ActionResult<CustomResult<About>>> UpdateAbout([FromForm] About about, IFormFile? photo)
        {
            try
            {
                var resource = await _aboutRepository.GetAboutByIdAsync(about.Id);
                if (resource != null)
                {
                    var resourceUpdate = await _aboutRepository.UpdateAboutAsync(
                        about, photo);
                    var response = new CustomResult<About>(200,
                        "update about successfully", resourceUpdate, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomResult<About>(404,
                        "No about to update", null, null);
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
