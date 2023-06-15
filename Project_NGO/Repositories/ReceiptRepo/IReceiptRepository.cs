using Project_NGO.Responses;

namespace Project_NGO.Repositories.ReceiptRepo
{
    public interface IReceiptRepository
    {
        Task<List<ReceiptResponse>> GetReceipts();

        Task<List<ReceiptResponse>> GetReceiptsWithType(int type);
    }
}