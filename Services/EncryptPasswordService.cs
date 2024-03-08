using System.Security.Cryptography;
using System.Text;

namespace LR7.Services
{
    public class EncryptPasswordService
    {
       

        public EncryptPasswordService(){}

        public string EncryptPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public bool ValidatePassword(string inputPassword, string encryptedPassword)
        {
            return EncryptPassword(inputPassword) == encryptedPassword;
        }
    }
}
