using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;

namespace TripsBlogCoreProject.Email
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string firstName, string content)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("yourmailadres");
            mailMessage.To.Add("tomailadres");

            mailMessage.Subject = firstName + " kişisinden Bir Mesajınız Var..!";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = content;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("yourmailadres", "yourpassword");

            //wqrbmhiumkxjqkqv
            client.Host = "smtp.mail.com";
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
        public bool SendEmailConfirmation(string userEmail, string confirmationLink)
        {

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("yourmailadres");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("yourmailadres", "yourpassword");
            client.Host = "smtp.mail.com";
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("yourmailadres");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("yourmailadres", "yourpassword");
            client.Host = "smtp.mail.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch
            {
                // log exception
            }
            return false;
        }

    }
}
