using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(int id, Comment comment);
        Task<Comment> DeleteCommentAsync(int id);
    }
}
