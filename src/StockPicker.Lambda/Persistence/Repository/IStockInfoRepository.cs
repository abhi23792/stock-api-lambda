using StockPicker.Lambda.Persistence.Models;

namespace StockPicker.Lambda.Persistence.Repository
{
    public interface IStockInfoRepository
    {
        Task<List<StockInfo>> GetStockInfoByStockCodeAsync(string stockCode);

        Task UpdateStockInfoAsync(StockInfo stockInfo);
    }
}
