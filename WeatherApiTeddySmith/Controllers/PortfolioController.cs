using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherApiTeddySmith.Extensions;
using WeatherApiTeddySmith.Interfaces;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }
        [HttpGet]
        [Authorize] //check claims
        public async Task<IActionResult> GetUserPorfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPorfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock not found");
            var userPorfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if (userPorfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Cannot add same stock");
            var portfolio = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };
            await _portfolioRepo.CreateAsync(portfolio);
            if (portfolio == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPorfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var filteredStock = userPorfolio.Where(s=>s.Symbol.ToLower()== symbol.ToLower()).ToList();
            if(filteredStock.Count() == 1)
            {
                await _portfolioRepo.Delete(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();
        }
    }
}
