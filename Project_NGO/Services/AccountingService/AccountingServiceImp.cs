using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.AccouttingRepo;

namespace Project_NGO.Services.AccountingService
{
    public class AccountingServiceImp : IAccountingRepository
    {
        private readonly DatabaseContext _dbContext;

        public AccountingServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Accounting>> GetAccountings()
        {
            return await _dbContext.Accountings.ToListAsync();
        }
    }
}