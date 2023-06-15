using Project_NGO.Models.Chart;

namespace Project_NGO.Repositories.Chart
{
    public interface IContactChartForm
    {
            List<ContactFormChartData> GetChartDataForWeek();
            List<ContactFormChartData> GetChartDataForDay();
    }
}
