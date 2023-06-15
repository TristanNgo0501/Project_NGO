using Project_NGO.Models.Chart;

namespace Project_NGO.Repositories.Chart
{
    public interface IChartRepository
    {
        Task<IEnumerable<ChartMapModel>> GetListRegionAsync();
        Task<IEnumerable<ReceptUserProgram>> GetListReceiptUserProgram();
    }
}
