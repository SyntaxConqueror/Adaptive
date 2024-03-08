using LR7.Models;
using LR7.Services.Interfaces;

namespace LR7.Services
{
    public class UserService : IUserService
    {
        public List<UserModel> _users;

        public UserService()
        {
            _users = new List<UserModel>
            {
                new UserModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 5, 15), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", DateOfBirth = new DateTime(1988, 9, 20), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", DateOfBirth = new DateTime(1995, 3, 10), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 4, FirstName = "Bob", LastName = "Brown", Email = "bob@example.com", DateOfBirth = new DateTime(1987, 12, 5), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 5, FirstName = "Emily", LastName = "Davis", Email = "emily@example.com", DateOfBirth = new DateTime(1992, 8, 25), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 6, FirstName = "Michael", LastName = "Wilson", Email = "michael@example.com", DateOfBirth = new DateTime(1985, 6, 30), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 7, FirstName = "Sophia", LastName = "Martinez", Email = "sophia@example.com", DateOfBirth = new DateTime(1993, 10, 12), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 8, FirstName = "William", LastName = "Anderson", Email = "william@example.com", DateOfBirth = new DateTime(1984, 4, 8), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 9, FirstName = "Olivia", LastName = "Taylor", Email = "olivia@example.com", DateOfBirth = new DateTime(1998, 7, 18), Password = "h6VOUZ+tk/9aaA16OCGUnWoQPTQ+PcBxTviLuSAPOSo=", LastLogin = DateTime.Now, FailedLoginAttempts = 0 },
                new UserModel { Id = 10, FirstName = "James", LastName = "Thomas", Email = "james@example.com", DateOfBirth = new DateTime(1991, 11, 22), Password = "21312313", LastLogin = DateTime.Now, FailedLoginAttempts = 0 }
            };
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            user.Id = _users.Max(u => u.Id) + 1;
            user.Password = (new EncryptPasswordService()).EncryptPassword(user.Password);
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
                existingUser.FirstName = user.FirstName;
                existingUser.Email = user.Email;
                return existingUser;
            }
            return null;
        }

        public async Task<UserModel> ValidateUser(string email)
        {
            var existingUser = _users.FirstOrDefault(u => u.Email == email);
            if(existingUser != null)
            {
                return existingUser;
            }
            return null;
        }
    }
}
