using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models;
using Project_NGO.Models.Authenication;
using Project_NGO.Repositories;
using Project_NGO.Requests;
using Project_NGO.Utils;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly ProgramRepository _repository;

        public ProgramController(ProgramRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Program
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramDTO>>> GetAll()
        {
            try
            {
                var list = await _repository.GetProgramList();
                if (list != null && list.Any())
                {
                    var response = new CustomStatusResult<IEnumerable<ProgramDTO>>
                        (StatusCodes.Status200OK, "Get List Program Successfully", list, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<IEnumerable<Programs>>
                        (StatusCodes.Status404NotFound, "Can not get list Program", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }
        }

        // GET: api/Program/5
        [HttpGet("{id}", Name = "GetProgramById")]
        public async Task<ActionResult> GetProgramById(int id)
        {
            try
            {
                var resource = await _repository.GetProgramById(id);
                if (resource != null)
                {
                    var response = new CustomStatusResult<ProgramDTO>
                        (StatusCodes.Status200OK, "Get Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "404", Message = "Can not get program" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }
        }

        // POST: api/Program
        [HttpPost]
        public async Task<ActionResult> AddPrograms([FromForm] ProgramDTO programDto, IFormFile? file)
        {
            try
            {
                var resource = await _repository.AddProgram(programDto, file);
                if (resource != null)
                {
                    var response = new CustomStatusResult<ProgramDTO>
                        (StatusCodes.Status201Created, "Added Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Invalid Request" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }
        }

        // PUT: api/Program/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProgram([FromForm] ProgramDTO programDto, int id, IFormFile? file)
        {
            try
            {
                var resource = await _repository.UpdateProgram(programDto, id, file);
                if (resource != null)
                {
                    var response = new CustomStatusResult<ProgramDTO>
                        (StatusCodes.Status200OK, "Updated Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Invalid Request" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }
        }

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProgram(int id)
        {
            try
            {
                var resource = await _repository.DeleteProgram(id);
                if (resource != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "200", Message = "Deleted Successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "Invalid Request" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }
        }
    }
}