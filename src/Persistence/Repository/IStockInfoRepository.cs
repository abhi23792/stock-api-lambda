using StockPicker.Lambda.Persistence.Models;

namespace StockPicker.Lambda.Persistence.Repository
{
    public interface IStockInfoRepository
    {
        Task<StockInfo?> GetStockInfoAsync(string uuid);

        Task UpdateStockInfoAsync(StockInfo stockInfo);
    }
}
