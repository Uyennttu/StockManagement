using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApiTeddySmith.Data;
using WeatherApiTeddySmith.DTOs;
using WeatherApiTeddySmith.Helpers;
using WeatherApiTeddySmith.Interfaces;
using WeatherApiTeddySmith.Mappers;
using WeatherApiTeddySmith.Models;


namespace WeatherApiTeddySmith.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {       
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {          
            _stockRepository = stockRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepository.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto()); //deffered execution for sql
            return Ok(stockDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)  return NotFound();
            else return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockDto stockDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = stockDto.ToStock();
            await _stockRepository.CreateStockAsync(stock);
            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute]int id, [FromBody] StockDto stockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _stockRepository.UpdateStockAsync(id, stockDto);
            if (stock == null) return NotFound();           
            return Ok(stock);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _stockRepository.DeleteStockAsync(id);
            if (stock == null) return NotFound();
            return NoContent();
        }
    }
}
