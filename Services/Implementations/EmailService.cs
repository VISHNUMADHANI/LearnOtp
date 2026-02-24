using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Homecare_Dotnet.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpAsync(string toEmail, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HomeCare", _config["Gmail:Email"]));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Your Login OTP - HomeCare";

            message.Body = new TextPart("html")
            {
                Text = $@"
                    <h2>HomeCare Login</h2>
                    <p>Your OTP code is:</p>
                    <h1 style='letter-spacing:8px; color:#4F46E5;'>{otp}</h1>
                    <p>Expires in <strong>5 minutes</strong>.</p>"
            };

            using var client = new SmtpClient();

            // ✅ Bypass SSL
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await client.ConnectAsync(
                _config["Gmail:Host"],
                int.Parse(_config["Gmail:Port"]),
                SecureSocketOptions.StartTls
            );

            // ✅ Mailtrap uses Username (not email) to login
            await client.AuthenticateAsync(
                _config["Gmail:Username"],
                _config["Gmail:AppPassword"]
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}