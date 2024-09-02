using System.Text;

namespace Web_API__Task2.DTOs
{
    public class PasswordHasherNew
    {

        public static void createPasswordHash(int password, out byte[] passwordHash , out byte[] passwordSalt)
        {

            using (var h = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = h.Key;  // key come from method sha512
                passwordHash = h.ComputeHash(Encoding.UTF8.GetBytes(Convert.ToString(password)));  // password ==> hashing 
            }

        }

        public static bool VerifyPasswordHash(int password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Convert.ToString(password)));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
