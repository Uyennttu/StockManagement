using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync (Portfolio portfolio);
        Task<Portfolio> Delete (AppUser user, string symbol);
    }
}
