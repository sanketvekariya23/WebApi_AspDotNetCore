using Login_Register.Model;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;

namespace Login_Register.Process
{
    public class EmailProcess : GlobelVeriable
    {
        private readonly EmailSetting setting;
        public EmailProcess(IOptions<EmailSetting> _setting) { setting = _setting.Value; }
        public async Task SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Name", setting.Email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html")
            {
                Text = message
            };
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(setting.Host, setting.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(setting.Email, setting.Password);
                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex) { throw new Exception("An error occurred while sending the email.", ex); }
        }
    }
}
