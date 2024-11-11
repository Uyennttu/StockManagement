using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApiTeddySmith.Data;
using WeatherApiTeddySmith.Interfaces;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comment.ToListAsync();
        }
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null) return null;
            return comment;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(int id, Comment comment)
        {
            var existingComment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment == null) return null;

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x=>x.Id==id);
            if (comment == null) return null;
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
