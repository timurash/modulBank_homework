using System;
using System.Security.Cryptography;
using System.Text;

namespace Auth
{
    public class Password
    {
        public Password(string password)
        {
            Salt = Guid.NewGuid().ToString().Substring(0, 8);
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(string.Concat(password, Salt));
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                PasswordHash = sb.ToString();
            }
        }
        public string PasswordHash { get; }
        public string Salt { get; }

        public static bool CheckPassword(string password, string salt, string hash)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(string.Concat(password, salt));
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                if (sb.ToString() == hash)
                    return true;
                else
                    return false;
            }
        }

        public string GenHash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(string.Concat(password, Salt));
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return  sb.ToString();
                
            }
        }
    }
}