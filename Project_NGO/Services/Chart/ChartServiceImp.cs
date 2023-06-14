using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models.Chart;
using Project_NGO.Repositories.Chart;


namespace Project_NGO.Services.Chart
{
    public class ChartServiceImp : IChartRepository
    {
        private readonly DatabaseContext dbcontext;
        public ChartServiceImp(DatabaseContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public async Task<IEnumerable<ChartMapModel>> GetListRegionAsync()
        {
            var listUsers = await dbcontext.Users.ToListAsync();
            var query = from member in listUsers group member by member.Region into regionGroup
                        select new ChartMapModel
                        {
                            Region = regionGroup.Key,
                            Member = regionGroup.Count()
                        };
            var list = query.ToList();
            if (list.Count > 0 && list.Any())
            {
                return list;
            }
            return null;
        }
    }
}
