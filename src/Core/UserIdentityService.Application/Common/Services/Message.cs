using MimeKit;

namespace UserIdentityService.Application.Common.Services;
public class Message
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public Message(IEnumerable<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress("email", x)));
        Subject = subject;
        Content = content;
    }

    public Message(string[] strings, string v, Task<string> otp)
    {
    }
}
