using LR7.Models;
using LR7.Services.Interfaces;

namespace LR7.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserModel> _users;

        public UserService()
        {
            _users = new List<UserModel>
            {
                new UserModel { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new UserModel { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
                new UserModel { Id = 3, Name = "Alice Johnson", Email = "alice@example.com" },
                new UserModel { Id = 4, Name = "Bob Brown", Email = "bob@example.com" },
                new UserModel { Id = 5, Name = "Emily Davis", Email = "emily@example.com" },
                new UserModel { Id = 6, Name = "Michael Wilson", Email = "michael@example.com" },
                new UserModel { Id = 7, Name = "Sophia Martinez", Email = "sophia@example.com" },
                new UserModel { Id = 8, Name = "William Anderson", Email = "william@example.com" },
                new UserModel { Id = 9, Name = "Olivia Taylor", Email = "olivia@example.com" },
                new UserModel { Id = 10, Name = "James Thomas", Email = "james@example.com" }
            };
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            user.Id = _users.Max(u => u.Id) + 1; // Генерация нового ID.
            _users.Add(user);
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userToRemove = _users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
                return true;
            }
            return false;
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return _users;
        }

        public async Task<UserModel> UpdateUserAsync(int id, UserModel user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                return existingUser;
            }
            return null;
        }
    }
}
