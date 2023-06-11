using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models.Authenication;
using Project_NGO.Repositories.AccouttingRepo;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IAccountingRepository accountingRepository;

        public AccountingController(IAccountingRepository accountingRepository)
        {
            this.accountingRepository = accountingRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetListAccounting()
        {
            try
            {
                var resource = await accountingRepository.GetAccountings();
                if (resource != null)
                {
                    return Ok(resource);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "Can not get list" });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Error Server" });
            }
        }
    }
}