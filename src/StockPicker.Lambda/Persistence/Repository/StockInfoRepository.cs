using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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

        public async Task<List<StockInfo>> GetStockInfoByStockCodeAsync(string stockCode)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("StockCode", ScanOperator.Equal, stockCode)
            };

            var result = await _context.ScanAsync<StockInfo>(conditions).GetRemainingAsync();
            return result;
        }

        public async Task UpdateStockInfoAsync(StockInfo stockInfo)
        {
            await _context.SaveAsync(stockInfo);
        }
    }
}
