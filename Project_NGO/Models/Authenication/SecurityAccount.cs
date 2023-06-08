using System.Security.Cryptography;
using System.Text;

namespace Project_NGO.Models.Authenication
{
    public class SecurityAccount
    {
        private static string key = "group4projectNGO_Charity";
        public static string EncodePlanText(string password)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] stringBytes = Encoding.UTF8.GetBytes(password);
            using (var hash = new HMACSHA256(keyBytes))
            {
                // băm stringBytes thành mảng bytes
                byte[] hashBytes = hash.ComputeHash(stringBytes);
                string encodedString = Convert.ToBase64String(stringBytes) + "." + Convert.ToBase64String(hashBytes);
                return encodedString;
            }
        }
        public static string DecodePlanText(string encodedString)
        {
            string[] parts = encodedString.Split('.');
            if (parts.Length != 2)
            {
                throw new Exception("Invalid encoding string format");
            }
            byte[] decodingStringBytes = Convert.FromBase64String(parts[0]);
            byte[] decodingHashBytes = Convert.FromBase64String(parts[1]);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hash = new HMACSHA256(keyBytes))
            {
                bool hasMatch = hash.ComputeHash(decodingStringBytes).SequenceEqual(decodingHashBytes);
                if (!hasMatch)
                {
                    throw new Exception("Hash does not matching");
                }
                string decodedString = Encoding.UTF8.GetString(decodingStringBytes);
                return decodedString;
            }
        }
    }
}
