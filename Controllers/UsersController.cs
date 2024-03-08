using LR7.Models;
using LR7.Services;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LR7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        protected AuthService _authService;
        protected EncryptPasswordService _encryptPasswordService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
            _authService = new AuthService();
        }

        [HttpGet("users/")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
                return NotFound();
            return Ok(updatedUser);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            var createdUser = await _authService.Register(user);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("user/login")]
        public async Task<string> Login([FromBody] LoginRequestModel login)
        {
            var jwtToken = await _authService.Login(login.Email, login.Password);
            return "Authorization: Bearer " + jwtToken;
        }
    }
}
