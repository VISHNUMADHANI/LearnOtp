using Homecare_Dotnet.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Homecare_Dotnet.Services
{

    public class OtpService : IOtpService
    {
        private readonly IMemoryCache _cache;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set($"OTP_{email}", otp, options);
            return otp;
        }

        public bool ValidateOtp(string email, string otp)
        {
            if (_cache.TryGetValue($"OTP_{email}", out string cachedOtp))
            {
                if (cachedOtp == otp)
                {
                    _cache.Remove($"OTP_{email}"); // one time use
                    return true;
                }
            }
            return false;
        }
    }
}   