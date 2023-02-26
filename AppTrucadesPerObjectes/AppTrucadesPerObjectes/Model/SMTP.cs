using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

namespace AppTrucadesPerObjectes.Model
{
    public class SMTP
    {
        private Properties propietats;

        public SMTP()
        {
            propietats = new Properties();
        }

        public void sendEmail(String subject, String message)
        {
            try
            {
                SmtpClient client = new SmtpClient(propietats.Smtp_host);
                client.Credentials = new NetworkCredential(propietats.Smtp_user, propietats.Smtp_password);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(propietats.Smtp_mail_from);
                mailMessage.To.Add(propietats.Smtp_mail_to);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
