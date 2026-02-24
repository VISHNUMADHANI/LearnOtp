using System;
using System.ComponentModel.DataAnnotations;

namespace Homecare_Dotnet.Models.Entities
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]

        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]

        [RegularExpression(
     @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$",
     ErrorMessage = "Enter a valid email address"
 )]
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;


        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpiry { get; set; }

    }
}
