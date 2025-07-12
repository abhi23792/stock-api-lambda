using Amazon.DynamoDBv2.DataModel;
using StockPicker.Lambda.Persistence.Models;

namespace StockPicker.Lambda.Persistence.Repository
{
    public class StockInfoRepository : IStockInfoRepository
    {
        private readonly IDynamoDBContext _context;

        public StockInfoRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<StockInfo?> GetStockInfoAsync(string uuid)
        {
            var result = await _context.LoadAsync<StockInfo>(uuid);
            return result;
        }

        public async Task UpdateStockInfoAsync(StockInfo stockInfo)
        {
            await _context.SaveAsync(stockInfo);
        }
    }
}
