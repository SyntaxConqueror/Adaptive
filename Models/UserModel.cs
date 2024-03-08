using System.ComponentModel.DataAnnotations;

namespace LR7.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Password { get; set; }

        public DateTime LastLogin { get; set; }

        public int FailedLoginAttempts { get; set; }
    }
}
