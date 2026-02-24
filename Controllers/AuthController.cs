using Microsoft.AspNetCore.Mvc;
using Homecare_Dotnet.Services;
using Homecare_Dotnet.Models;
using Homecare_Dotnet.Services.Interfaces;

namespace Homecare_Dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;

        public AuthController(IOtpService otpService, IEmailService emailService)
        {
            _otpService = otpService;
            _emailService = emailService;
        }

        // STEP 1 → Send OTP to email
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest(new { message = "Email is required." });

            var otp = _otpService.GenerateOtp(request.Email);
            await _emailService.SendOtpAsync(request.Email, otp);

            return Ok(new { message = "OTP sent to " + request.Email });
        }

        // STEP 2 → Verify OTP
        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
                return BadRequest(new { message = "Email and OTP are required." });

            var isValid = _otpService.ValidateOtp(request.Email, request.Otp);

            if (!isValid)
                return BadRequest(new { message = "Invalid or expired OTP." });

            return Ok(new { message = "Login successful!", email = request.Email });
        }
    }
}