using MailKit.Net.Smtp;
using EmailService.Models;
using MimeKit;

namespace EmailService.Service
{
    public class EmailSendService
    {
        public async Task sendEmail(UserMessage res, string message)
        {
            MimeMessage message1 = new MimeMessage();
            message1.From.Add(new MailboxAddress("THE Jitu Post ", "superdomestique254@gmail.com"));

            // Set the recipient's email address
            message1.To.Add(new MailboxAddress(res.Name, res.Email));

            message1.Subject = "Welcome to TheJitu Post";

            var body = new TextPart("html")
            {
                Text = message.ToString()
            };
            message1.Body = body;

            var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate("superdomestique254@gmail.com", "xnicusnklcqvhexw");

            await client.SendAsync(message1);

            await client.DisconnectAsync(true);
        }
    }
}