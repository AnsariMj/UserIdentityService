using MimeKit;
using MailKit.Net.Smtp;
using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Models;
using UserIdentityService.Application.Common.Services;

namespace UserIdentityService.Infrastructure.Services.EmailService;

public class MailKitEmailService : IMailKitEmailService
{
    private readonly EmailConfigurationModel _emailConfiguration;

    public MailKitEmailService(EmailConfigurationModel emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public void SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        Send(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = message.Content
        };
        return emailMessage;
    }

    private void Send(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);

            client.Send(message);
        }
        catch
        {
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
