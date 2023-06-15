using Microsoft.AspNetCore.Mvc;
using Project_NGO.Repositories.CashOutRepo;
using Project_NGO.Requests.Cash;
using Project_NGO.Responses;
using Project_NGO.Utils;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashController : ControllerBase
    {
        private readonly ICashOutRepository _repository;

        public CashController(ICashOutRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> Donate([FromBody] CashRequest cashRequest)
        {
            try
            {
                var resource = await _repository.CashIn(cashRequest);
                if (resource != null)
                {
                    var response = new CustomStatusResult<CashResponse>
                           (StatusCodes.Status200OK, "Donate Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<CashResponse>
                           (StatusCodes.Status400BadRequest, "This program is not allowed to donate", null, null);
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

        [HttpPost]
        public async Task<ActionResult> Withdraw(CashRequest cashRequest)
        {
            try
            {
                var resource = await _repository.CashOut(cashRequest);
                if (resource != null)
                {
                    var response = new CustomStatusResult<CashResponse>
                           (StatusCodes.Status200OK, "Withdraw Successfully", resource, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<CashResponse>
                           (StatusCodes.Status400BadRequest, "Your balance is not enough or not allow to withdraw", null, null);
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
        public async Task<ActionResult> TotalPriceIn(int? programId)
        {
            try
            {
                decimal cash = await _repository.CashShow(programId);
                if (cash != 0)
                {
                    var response = new CustomStatusResult<CashResponse>
                               (StatusCodes.Status200OK,
                               "Get Cash List Successfully", new CashResponse { Money = cash }, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<CashResponse>
                               (StatusCodes.Status404NotFound, "Can not get list cash", null, null);
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
        public async Task<ActionResult> TotalPriceOut(int programId)
        {
            try
            {
                decimal cash = await _repository.CashOutShow(programId);
                if (cash != 0)
                {
                    var response = new CustomStatusResult<CashResponse>
                               (StatusCodes.Status200OK,
                               "Get Total Amount  Successfully", new CashResponse { Money = cash }, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<CashResponse>
                               (StatusCodes.Status404NotFound, "The money was 0", new CashResponse { Money = cash }, null);
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
        public async Task<ActionResult> TotalRemainBalance(int programId)
        {
            try
            {
                decimal balance = await _repository.BalanceRemain(programId);
                if (balance != 0)
                {
                    var response = new CustomStatusResult<CashResponse>
                    (StatusCodes.Status200OK,
                               "Get Remain Balance Successfully", new CashResponse { Money = balance }, null);
                    return Ok(response);
                }
                else
                {
                    var response = new CustomStatusResult<CashResponse>
                               (StatusCodes.Status404NotFound, "Your Balance is empty", null, null);
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