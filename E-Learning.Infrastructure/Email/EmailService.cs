using E_Learning.Application.Abstractions.Email;
using E_Learning.Domain.Abstractions.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace E_Learning.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
            => _mailSettings = mailSettings.Value;
        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.SenderEmail);
            email.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls, cancellationToken);

                await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, cancellationToken);
                await smtp.SendAsync(email, cancellationToken);
            }
            finally
            {
                await smtp.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
