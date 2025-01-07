using UserIdentityService.Application.Common.Interfaces;
using UserIdentityService.Application.Common.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using UserIdentityService.Infrastructure.Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UserIdentityService.Infrastructure.Services.EmailService;

public class SendGridEmailService : ISendGridEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly EmailOptions _emailConfiguration;
    private readonly ILogger<SendGridEmailService> _logger;

    public SendGridEmailService(
        ISendGridClient sendGridClient,
        IOptions<EmailOptions> emailConfiguration,
        ILogger<SendGridEmailService> logger
        )
    {
        _sendGridClient = sendGridClient;
        _emailConfiguration = emailConfiguration.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailModel email, CancellationToken cancellationToken)
    {
        var hostEmail = string.IsNullOrEmpty(email.From) ? _emailConfiguration.HostEmail : email.From;

        EmailAddress from = new EmailAddress(hostEmail);
        EmailAddress recipient = new EmailAddress(email.To);
        string subject = string.IsNullOrEmpty(email.Subject) ? "MJ Ansari" : email.Subject;

        var sendGridMessage = new SendGridMessage
        {
            From = from,
            Subject = subject,
            HtmlContent = email.Body,
            Personalizations = new List<Personalization>
            {
                new Personalization
                {
                    Ccs =email.Ccs?.Select(s=>new EmailAddress(s)).ToList(),
                }
            }
        };

        sendGridMessage.AddTo(recipient);

        Response response = await _sendGridClient.SendEmailAsync(sendGridMessage, cancellationToken).ConfigureAwait(false);
        if (response != null && response.StatusCode != System.Net.HttpStatusCode.Accepted)
        {
            _logger.LogError($"From: {email.From}, To: {recipient.Email}");
            _logger.LogError(await response.Body.ReadAsStringAsync(cancellationToken));
        }
    }
}
