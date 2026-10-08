using System.Security.Cryptography;
using System.Text;

namespace TuristickaAgencija.Services.Security
{
    public static class PasswordHasher
    {
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            RandomNumberGenerator.Fill(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            // SHA256 umjesto zastarjelog SHA1.
            byte[] hash = SHA256.HashData(dst);
            return Convert.ToBase64String(hash);
        }

        public static bool Verify(string password, string salt, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(hash))
            {
                return false;
            }

            var computed = Convert.FromBase64String(GenerateHash(salt, password));
            var stored = Convert.FromBase64String(hash);
            return CryptographicOperations.FixedTimeEquals(computed, stored);
        }
    }
}
