using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Framework.Helper
{
    public class Email
    {
        public static void Send(string email, string subject, string content)
        {
            MailMessage mM = new MailMessage
            {
                From = new MailAddress("myprojecteceptions@gmail.com "),
                Subject = subject,
                Body = content,
                IsBodyHtml = true
            };

            mM.To.Add(email);

            SmtpClient sC = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("myprojecteceptions@gmail.com ", "Ph4ng14nh4t")
            };

            sC.Send(mM);
            mM.Dispose();
        }
    }
}
