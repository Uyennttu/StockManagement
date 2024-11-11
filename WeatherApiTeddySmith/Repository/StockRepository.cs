using Microsoft.EntityFrameworkCore;
using WeatherApiTeddySmith.Data;
using WeatherApiTeddySmith.DTOs;
using WeatherApiTeddySmith.Helpers;
using WeatherApiTeddySmith.Interfaces;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;   
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteStockAsync(int id)
        {
           var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null) return null;
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(c=>c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s =>s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol)) 
            {
                stocks = stocks.Where(s=>s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy)) 
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecending? stocks.OrderByDescending(s=>s.Symbol): stocks.OrderBy(s=>s.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(x=>x.Id==id);
            if (stock == null) return null;
            return stock;
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(s =>s.Symbol ==symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(x=>x.Id==id);
        }

        public async Task<Stock> UpdateStockAsync(int id, StockDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null) return null;
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync(); return existingStock;
        }

    }
}
