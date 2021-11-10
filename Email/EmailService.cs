using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UnicdaPlatform.Email
{
    public class EmailService
    {
        public async Task<string> Send(string from, string to, string subject, string html, 
                                       string compName, string custName, string email, string password)
        {
           
            try
            {
                using (MailMessage emailMessage = new MailMessage())
                {

                    emailMessage.Subject = subject;
                    emailMessage.Body = html;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Priority = MailPriority.Low;

                    emailMessage.From = new MailAddress(email);
                    emailMessage.To.Add(to);

                    SmtpClient smtp = new SmtpClient("mail.njautoimport.com");
                    NetworkCredential Credentials = new NetworkCredential(email, password);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 25;    //alternative port number is 8889
                    smtp.EnableSsl = false;
                    smtp.Send(emailMessage);

                };

                return "good";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
    }
}
