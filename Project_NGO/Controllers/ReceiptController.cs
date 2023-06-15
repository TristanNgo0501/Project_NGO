using Microsoft.AspNetCore.Mvc;
using Project_NGO.Repositories.ReceiptRepo;
using Project_NGO.Responses;
using Project_NGO.Utils;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptRepository _repository;

        public ReceiptController(IReceiptRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllReceipt()
        {
            try
            {
                var resource = await _repository.GetReceipts();

                if (resource != null)
                {
                    var response = new CustomStatusResult<List<ReceiptResponse>>
                           (StatusCodes.Status200OK, "Get List Receipt Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<List<ReceiptResponse>>
                           (StatusCodes.Status400BadRequest, "Invalid Request", null, null);
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

        [HttpGet]
        public async Task<ActionResult> GetReceiptsWithType(int type)
        {
            try
            {
                var resource = await _repository.GetReceiptsWithType(type);

                if (resource != null)
                {
                    var response = new CustomStatusResult<List<ReceiptResponse>>
                           (StatusCodes.Status200OK, "Get List Receipt Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<List<ReceiptResponse>>
                           (StatusCodes.Status400BadRequest, "Invalid Request", null, null);
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
    }
}