using WeatherApiTeddySmith.DTOs;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {                
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto createCommentDto, int stockId)
        {
            return new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,                
                StockId = stockId
            };
        }
        public static Comment ToCommentFromUpdate(this CreateCommentDto updateCommentDto)
        {
            return new Comment
            {
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content,                
            };
        }
    }
}
