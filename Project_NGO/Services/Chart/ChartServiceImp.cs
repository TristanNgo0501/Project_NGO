using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Models.Chart;
using Project_NGO.Repositories.Chart;
using System.Collections.Generic;


namespace Project_NGO.Services.Chart
{
    public class ChartServiceImp : IChartRepository
    {
        private readonly DatabaseContext dbcontext;
        public ChartServiceImp(DatabaseContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public async Task<IEnumerable<ReceptUserProgram>> GetListReceiptUserProgram()
        {
            var receipt = await dbcontext.Receipts
                                         .Include(r => r.User)
                                         .Include(r => r.Programs)
                                         .Where(r => r.Type == ReceiptType.Price_In)
                                         .Select(u => new ReceptUserProgram
                                         {
                                             Id = u.Id,
                                             Money = u.Money,
                                             Description = u.Description,
                                             CreatedAt = u.CreatedAt,
                                             Type = u.Type,
                                             Name = u.User.Name,
                                             UserId = u.UserId,
                                             ProgramName = u.Programs.Title,
                                             Status = u.Programs.Status
                                         }).ToListAsync();
            if (receipt.Count > 0 && receipt.Any())
            {
                return receipt;
            }
            return null;
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
