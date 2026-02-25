public interface IEmailService
{
    Task<(bool IsSuccess, string ErrorMessage)> SendOtpAsync(string toEmail, string otp);
}