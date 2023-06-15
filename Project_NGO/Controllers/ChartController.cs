using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models.CustomResponse;
using Project_NGO.Repositories.Chart;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartRepository chartRepo;
        public ChartController(IChartRepository _chartRepo)
        {
            chartRepo = _chartRepo;
        }

        [HttpGet]
        public async Task<IActionResult> ListRegion()
        {
            try
            {
                var result = await chartRepo.GetListRegionAsync();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list region success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list region fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetListReceiptUserProgram()
        {
            try
            {
                var result = await chartRepo.GetListReceiptUserProgram();
                if (result != null)
                {
                    return CustomMethodResponse.GetListResponse200Ok(result, "Get list region success");
                }
                else
                {
                    return CustomMethodResponse.GetResponse404NotFound("Get list region fail");
                }
            }
            catch (Exception ex)
            {
                return CustomMethodResponse.Response500Error(ex);
            }

        }
    }
}
