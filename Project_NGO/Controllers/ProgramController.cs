using Microsoft.AspNetCore.Mvc;
using Project_NGO.Repositories;
using Project_NGO.Requests;
using Project_NGO.Requests.Program;
using Project_NGO.Responses;
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
        public async Task<ActionResult<IEnumerable<ProgramResponse>>> GetAll()
        {
            try
            {
                var list = await _repository.GetProgramList();
                if (list != null && list.Any())
                {
                    var response = new CustomStatusResult<IEnumerable<ProgramResponse>>
                        (StatusCodes.Status200OK, "Get List Program Successfully", list, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<IEnumerable<ProgramResponse>>
                        (StatusCodes.Status404NotFound, "Can not get list Program", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
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
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status200OK, "Get Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status404NotFound, "Can not get list Program", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
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
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status201Created, "Added Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status400BadRequest, "Add Program Failed", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        // PUT: api/Program/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProgram([FromForm] ProgramUpdateDTO programDto, int id, IFormFile? file)
        {
            try
            {
                var resource = await _repository.UpdateProgram(programDto, id, file);
                if (resource != null)
                {
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status200OK, "Updated Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<ProgramResponse>
                         (StatusCodes.Status400BadRequest, "Update Program Failed", null, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProgram(int id)
        {
            try
            {
                bool resource = await _repository.DeleteProgram(id);
                if (resource)
                {
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status200OK, "Delete Program Successfully", null, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<ProgramResponse>
                        (StatusCodes.Status404NotFound, "Delete Program Failed", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetProgramWithStatus(string status)
        {
            try
            {
                var resource = await _repository.GetProgramsWithStatus(status);
                if (resource != null)
                {
                    var response = new CustomStatusResult<List<ProgramResponse>>
                        (StatusCodes.Status200OK, "Get Program Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<List<ProgramResponse>>
                        (StatusCodes.Status404NotFound, "Can not get list Program", null, null);
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    ErrorMessage = "An error occurred while retrieving the user",
                    ErrorDetails = ex.ToString()
                });
            }
        }
    }
}