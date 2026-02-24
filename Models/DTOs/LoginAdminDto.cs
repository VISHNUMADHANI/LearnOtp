namespace Homecare_Dotnet.Models.DTOs;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

public class LoginAdminDto
{
        [DefaultValue("johndoe@example.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}