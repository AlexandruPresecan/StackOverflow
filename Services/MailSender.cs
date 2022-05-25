using StackOverflow.Models;
using System.Net.Mail;
using System.Text;

namespace StackOverflow.Services
{
    public static class MailSender
    {
        public static void Send(ApplicationUser user)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("newstack.newoverflow@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "You have been banned from StackOverflow";
                mail.Body = "You have been banned from StackOverflow due to inappropriate behaviour.";

                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("newstack.newoverflow@gmail.com", Encoding.UTF8.GetString(Convert.FromBase64String("QjMzZjM0dDNyIQ==")));
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }   
    }
}
