using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models;
using Project_NGO.Repositories;
using Project_NGO.Utils;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]")]
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
        public async  Task<ActionResult<IEnumerable<Programs>>> GetAll()
        {
            try
            {
                var list = await _repository.GetProgramList();

                var results = new List<ValidationResult>();
                var context = new ValidationContext(list, serviceProvider: null, items: null);
                var isValid = Validator.TryValidateObject(list, context, results, true);

                if (isValid)
                {
                    if (list != null && list.Any())
                    {
                        var response = new CustomStatusResult<IEnumerable<Programs>>
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
                else
                {
                    var errors = results.Select(result => result.ErrorMessage).ToList();
                    var response = new CustomStatusResult<IEnumerable<Programs>>(
                        StatusCodes.Status400BadRequest,   "Invalid input", null, errors);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new CustomStatusResult<Programs>()
                    {
                        Message = "An error occured while retrived model",
                        Error = new List<string> { ex.Message }
                    });
            }
        }

        // GET: api/Program/5
        [HttpGet("{id}", Name = "GetProgramById")]
        public async Task<Programs> GetProgramById(int id)
        {
            return await _repository.GetProgramById(id);
        }

        // POST: api/Program
        [HttpPost]
        
        public async Task<Programs> AddPrograms([FromForm] Programs programs, IFormFile? file)
        {
            return await _repository.AddProgram(programs, file);
        }

        // PUT: api/Program/5
        [HttpPut("{id}")]
        public async Task<Programs> UpdateProgram([FromForm] Programs programs, IFormFile? file)
        {
            return await _repository.UpdateProgram(programs, file);
        }

        // DELETE: api/Program/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProgram(int id)
        {
            return await _repository.DeleteProgram(id);
        }
    }
}
