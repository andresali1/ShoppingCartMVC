using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer
{
    public class BL_Resources
    {
        private static string emailSmtp = ConfigurationManager.AppSettings["EmailSmtp"].ToString();
        private static string passSmtp = ConfigurationManager.AppSettings["PassSmtp"].ToString();

        /// <summary>
        /// Method to generate a Random Password
        /// </summary>
        /// <returns></returns>
        public static string GeneratePass()
        {
            string pass = Guid.NewGuid().ToString("N").Substring(0, 6);
            return pass;
        }

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

        public static bool SendEmail(string email, string subject, string message)
        {
            bool result = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress(emailSmtp);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential(emailSmtp, passSmtp),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
