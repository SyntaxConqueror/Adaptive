using System.ComponentModel.DataAnnotations;

namespace LR7.Models
{
    public class LoginRequestModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
