using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer
{
    public class BL_Resources
    {
        /// <summary>
        /// Method to encript text to SHA256
        /// </summary>
        /// <param name="text">Text to encrypt</param>
        /// <returns></returns>
        public static string Sha256Converter(string text)
        {
            StringBuilder sb = new StringBuilder();


            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
