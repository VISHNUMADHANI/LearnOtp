namespace Homecare_Dotnet.Models.DTOs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;


public class CreateAdminDto
{
    [DefaultValue("John Doe")]
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;


    [DefaultValue("johndoe@example.com")]
    [RegularExpression(
@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$",
ErrorMessage = "Enter a valid email address"
)]
    public string Email { get; set; } = string.Empty;
     
    [DefaultValue("Dou@9078J")]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(
        @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&]).{8,15}$",
        ErrorMessage = "Password must be 8-15 characters with letter, number and special character"
    )]
    public string Password { get; set; } = string.Empty;
}
