using LR7.Models;

namespace LR7.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> UpdateUserAsync(int id, UserModel user);
        Task<bool> DeleteUserAsync(int id);
    }
}
