using System.Security.Cryptography;
using System.Text;

namespace SLRAS_Demo.Helper
{
    public class PasswordHelper
    {
        public static string GeneratePassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
            StringBuilder password = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (password.Length < length)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    long index = num % validChars.Length;
                    password.Append(validChars[(int)index]);
                }
            }

            return password.ToString();
        }
        public static (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            byte[] salt = hmac.Key; // randomly generated key
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            // compare byte arrays in constant time
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
