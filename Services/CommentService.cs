using LR7.Models;
using LR7.Services.Interfaces;

namespace LR7.Services
{
    public class CommentService : ICommentService
    {
        private readonly List<CommentModel> _comments;

        public CommentService()
        {
            _comments = new List<CommentModel>
            {
                new CommentModel { Id = 1, UserID = 1, PostID = 1, Text = "Great post!" },
                new CommentModel { Id = 2, UserID = 2, PostID = 1, Text = "I agree!" },
                new CommentModel { Id = 3, UserID = 3, PostID = 2, Text = "Interesting topic." },
                new CommentModel { Id = 4, UserID = 1, PostID = 2, Text = "Looking forward to more." },
                new CommentModel { Id = 5, UserID = 2, PostID = 3, Text = "Thanks for sharing!" },
                new CommentModel { Id = 6, UserID = 3, PostID = 3, Text = "Really informative." },
                new CommentModel { Id = 7, UserID = 1, PostID = 4, Text = "Well written!" },
                new CommentModel { Id = 8, UserID = 2, PostID = 4, Text = "Impressive." },
                new CommentModel { Id = 9, UserID = 3, PostID = 5, Text = "Nice work!" },
                new CommentModel { Id = 10, UserID = 1, PostID = 5, Text = "Keep it up!" }
            };
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsAsync()
        {
            return _comments;
        }

        public async Task<CommentModel> GetCommentByIdAsync(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        public async Task<CommentModel> CreateCommentAsync(CommentModel comment)
        {
            comment.Id = _comments.Count + 1;
            _comments.Add(comment);
            return comment;
        }

        public async Task<CommentModel> UpdateCommentAsync(int id, CommentModel comment)
        {
            var existingComment = _comments.FirstOrDefault(c => c.Id == id);
            if (existingComment != null)
            {
                existingComment.Text = comment.Text;
               
                return existingComment;
            }
            return null;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var commentToDelete = _comments.FirstOrDefault(c => c.Id == id);
            if (commentToDelete != null)
            {
                _comments.Remove(commentToDelete);
                return true;
            }
            return false;
        }
    }
}
