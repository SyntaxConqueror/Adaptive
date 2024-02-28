using LR7.Models;
using LR7.Services.Interfaces;

namespace LR7.Services
{
    public class PostService : IPostService
    {
        private readonly List<PostModel> _posts;

        public PostService()
        {
            _posts = new List<PostModel>
            {
                new PostModel { Id = 1, Title = "Introduction to ASP.NET Core", Content = "This is an introduction to ASP.NET Core." },
                new PostModel { Id = 2, Title = "Getting Started with Entity Framework Core", Content = "A guide to getting started with Entity Framework Core." },
                new PostModel { Id = 3, Title = "Building RESTful APIs with ASP.NET Core", Content = "Learn how to build RESTful APIs using ASP.NET Core." },
                new PostModel { Id = 4, Title = "Authentication and Authorization in ASP.NET Core", Content = "Understanding authentication and authorization in ASP.NET Core." },
                new PostModel { Id = 5, Title = "Deploying ASP.NET Core Applications", Content = "Learn different ways to deploy ASP.NET Core applications." },
                new PostModel { Id = 6, Title = "ASP.NET Core MVC Fundamentals", Content = "Fundamentals of ASP.NET Core MVC." },
                new PostModel { Id = 7, Title = "Working with Razor Pages in ASP.NET Core", Content = "Understanding Razor Pages in ASP.NET Core." },
                new PostModel { Id = 8, Title = "Using Dependency Injection in ASP.NET Core", Content = "How to use dependency injection in ASP.NET Core applications." },
                new PostModel { Id = 9, Title = "Testing ASP.NET Core Applications", Content = "Guide to testing ASP.NET Core applications." },
                new PostModel { Id = 10, Title = "ASP.NET Core Best Practices", Content = "Best practices for developing ASP.NET Core applications." }
            };
        }

        public async Task<IEnumerable<PostModel>> GetPostsAsync()
        {
            return _posts;
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }

        public async Task<PostModel> CreatePostAsync(PostModel post)
        {
            post.Id = _posts.Count + 1;
            _posts.Add(post);
            return post;
        }

        public async Task<PostModel> UpdatePostAsync(int id, PostModel post)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
             
                return existingPost;
            }
            return null;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var postToDelete = _posts.FirstOrDefault(p => p.Id == id);
            if (postToDelete != null)
            {
                _posts.Remove(postToDelete);
                return true;
            }
            return false;
        }
    }
}
