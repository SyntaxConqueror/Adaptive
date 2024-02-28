using LR7.Models;

namespace LR7.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetPostsAsync();
        Task<PostModel> GetPostByIdAsync(int id);
        Task<PostModel> CreatePostAsync(PostModel post);
        Task<PostModel> UpdatePostAsync(int id, PostModel post);
        Task<bool> DeletePostAsync(int id);
    }
}
