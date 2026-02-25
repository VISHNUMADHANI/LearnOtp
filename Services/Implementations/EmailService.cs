using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Text.RegularExpressions;

namespace Homecare_Dotnet.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> SendOtpAsync(string toEmail, string otp)
        {
            // ✅ Step 1: Validate Email Format Before Sending
            if (string.IsNullOrWhiteSpace(toEmail))
                return (false, "Email address is required.");

            if (!IsValidEmail(toEmail))
                return (false, "Invalid email address. Please enter a correct email ID.");

            try
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

                return (true, null); // ✅ Success
            }
            catch (ParseException)
            {
                return (false, "Invalid email address format.");
            }
            catch (SmtpCommandException ex) when (ex.ErrorCode == SmtpErrorCode.RecipientNotAccepted)
            {
                return (false, "Email address not accepted. Please check your email ID.");
            }
            catch (SmtpCommandException ex) when (ex.ErrorCode == SmtpErrorCode.MessageNotAccepted)
            {
                return (false, "Message was rejected by the mail server. Please try again.");
            }
            catch (SmtpProtocolException)
            {
                return (false, "Mail server error. Please try again later.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to send OTP: {ex.Message}");
            }
        }

        // ✅ Email Format Validator
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}