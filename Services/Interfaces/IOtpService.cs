namespace Homecare_Dotnet.Services.Interfaces
{
    public interface IOtpService
    {
        string GenerateOtp(string email);
        bool ValidateOtp(string email, string otp);
    }
}