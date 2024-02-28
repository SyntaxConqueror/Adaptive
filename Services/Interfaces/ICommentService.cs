using LR7.Models;

namespace LR7.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetCommentsAsync();
        Task<CommentModel> GetCommentByIdAsync(int id);
        Task<CommentModel> CreateCommentAsync(CommentModel comment);
        Task<CommentModel> UpdateCommentAsync(int id, CommentModel comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
