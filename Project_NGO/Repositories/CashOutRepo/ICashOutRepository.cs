using Project_NGO.Requests.Cash;
using Project_NGO.Responses;

namespace Project_NGO.Repositories.CashOutRepo
{
    public interface ICashOutRepository
    {
        Task<CashResponse> CashOut(CashRequest cashRequest);

        Task<CashResponse> CashIn(CashRequest cashRequest);

        Task<decimal> CashShow(int? programId);

        Task<decimal> CashOutShow(int programId);

        Task<decimal> BalanceRemain(int programId);
    }
}