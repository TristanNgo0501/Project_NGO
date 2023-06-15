using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_NGO.Models.Chart;
using Project_NGO.Repositories.Chart;
using Project_NGO.Services.Chart;

namespace Project_NGO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactChartFormController : ControllerBase
    {
        private readonly IContactChartForm _contactChartForm;

        public ContactChartFormController(IContactChartForm contactChartForm)
        {
            _contactChartForm = contactChartForm;
        }

        [HttpGet]
        public IActionResult ChartDataForWeek()
        {
            var chartData = _contactChartForm.GetChartDataForWeek();
            return Ok(chartData);
        }

        [HttpGet]
        public IActionResult ChartDataForDay()
        {
            var chartData = _contactChartForm.GetChartDataForDay();
            return Ok(chartData);
        }
    }
}
