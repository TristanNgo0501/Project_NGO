using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.ReceiptRepo;
using Project_NGO.Responses;

namespace Project_NGO.Services.ReceiptService
{
    public class ReceiptServiceImp : IReceiptRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public ReceiptServiceImp(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ReceiptResponse>> GetReceipts()
        {
            var receipts = await _dbContext.Receipts.Where(r => r.ProgramId.HasValue).ToListAsync();
            var response = _mapper.Map<List<ReceiptResponse>>(receipts);
            return response;
        }

        public async Task<List<ReceiptResponse>> GetReceiptsWithType(int type)
        {
            var receiptWithType = await _dbContext.Receipts.Where(r => r.Type == (ReceiptType)type).ToListAsync();
            var receiptResponse = _mapper.Map<List<ReceiptResponse>>(receiptWithType);
            return receiptResponse;
        }
    }
}