using LR7.Models;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace LR7.Services
{
    public class AuthService
    {
        protected UserService _userService;
        protected EncryptPasswordService _encryptPasswordService;
        public AuthService() 
        {
            _userService = new UserService();
            _encryptPasswordService = new EncryptPasswordService();
        }
        [HttpPost("register")]
        public async Task<UserModel> Register(UserModel user)
        {
            user.Password = _encryptPasswordService.EncryptPassword(user.Password);
            _userService._users.Add(user);
            return user;
        }

        [HttpPost("login")]
        public async Task<string> Login(string email, string password)
        {
            var existingUser = await _userService.ValidateUser(email);
            if (existingUser == null) return "Invalid credentials";

            existingUser.LastLogin = DateTime.UtcNow;
            if(_encryptPasswordService.ValidatePassword(password, existingUser.Password))
            {
                var token = GenerateJwtToken(existingUser);
                return token;
            }

            existingUser.FailedLoginAttempts += 1;

            return "Password is not valid";
        }

        private string GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here_some_of_them_should_Be_longer"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: "AppIssuer",
                audience: "AppAudience",
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble("60")),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
