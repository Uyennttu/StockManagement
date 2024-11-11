using WeatherApiTeddySmith.DTOs;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {                
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,   
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }
        public static Stock ToStock(this StockDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}
