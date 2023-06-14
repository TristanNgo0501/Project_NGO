using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Repositories.CashOutRepo;
using Project_NGO.Requests.Cash;
using Project_NGO.Responses;

namespace Project_NGO.Services.CashOutService
{
    public class CashOutServiceImp : ICashOutRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public CashOutServiceImp(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CashResponse> CashIn(CashRequest cashRequest)
        {
            if (cashRequest == null)
            {
                throw new ArgumentNullException(nameof(cashRequest));
            }
            var response = new CashResponse();
            var receipt = new Receipt();

            if (cashRequest.userId == null)
            {
                throw new ArgumentNullException(nameof(cashRequest.userId));
            }
            receipt.ProgramId = cashRequest.programId;
            receipt.UserId = cashRequest.userId;
            receipt.Money = cashRequest.money;
            receipt.Type = ReceiptType.Price_In;
            receipt.CreatedAt = DateTime.UtcNow;

            await _dbContext.Receipts.AddAsync(receipt);
            await _dbContext.SaveChangesAsync();
            response.IsSuccessful = true;
            response.Money = cashRequest.money;

            return response;
        }

        public async Task<CashResponse> CashOut(CashRequest cashRequest)
        {
            if (cashRequest == null)
            {
                throw new ArgumentNullException(nameof(cashRequest));
            }
            var response = new CashResponse();
            var receipt = new Receipt();

            if (cashRequest.userId == null)
            {
                throw new ArgumentNullException(nameof(cashRequest.userId));
            }

            else
            {
                receipt.ProgramId = cashRequest.programId;
                receipt.UserId = cashRequest.userId;
                receipt.Money = cashRequest.money;
                receipt.Type = ReceiptType.Price_Out;
                receipt.CreatedAt = DateTime.UtcNow;

                await _dbContext.Receipts.AddAsync(receipt);
                await _dbContext.SaveChangesAsync();
                response.IsSuccessful = true;
                response.Money = cashRequest.money;

                return response;
            }
        }

        public async Task<decimal> CashShow(int? programId)
        {
            return await _dbContext
                .Receipts.Where(r => r.ProgramId == programId && r.Type == ReceiptType.Price_In)
                .SumAsync(r => r.Money);
        }

        public async Task<decimal> CashOutShow(int programId)
        {
            return await _dbContext
                .Receipts.Where(r => r.ProgramId == programId && r.Type == ReceiptType.Price_Out)
                .SumAsync(r => r.Money);
        }
    }
}