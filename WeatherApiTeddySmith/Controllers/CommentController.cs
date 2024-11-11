using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherApiTeddySmith.DTOs;
using WeatherApiTeddySmith.Extensions;
using WeatherApiTeddySmith.Interfaces;
using WeatherApiTeddySmith.Mappers;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment =  await _commentRepo.GetCommentByIdAsync(id);
            if (comment == null)  return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _stockRepo.StockExists(stockId)) 
                return BadRequest("Stock does not exist");
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username); 

            var comment = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new {id = comment.Id}, comment.ToCommentDto());

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CreateCommentDto commentDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.UpdateCommentAsync(id, commentDtoUpdate.ToCommentFromUpdate());
            if(comment==null) return NotFound("Comment not found");
            return Ok(comment.ToCommentDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.DeleteCommentAsync(id);
            if (comment == null) return NotFound();
            return NoContent();
        }
    }
}
