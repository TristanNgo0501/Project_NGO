using Project_NGO.Models;

namespace Project_NGO.Repositories.AccouttingRepo
{
    public interface IAccountingRepository
    {
        Task<IEnumerable<Accounting>> GetAccountings();
    }
}