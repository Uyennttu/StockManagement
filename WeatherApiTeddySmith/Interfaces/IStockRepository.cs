using WeatherApiTeddySmith.DTOs;
using WeatherApiTeddySmith.Helpers;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>>GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id); //FirstOrDefault can be null
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock> UpdateStockAsync(int id, StockDto stockDto);
        Task<Stock> DeleteStockAsync(int id);
        Task<bool> StockExists(int id);

    }
}
