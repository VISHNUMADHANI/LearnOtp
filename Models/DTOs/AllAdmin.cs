namespace Homecare_Dotnet.Models.DTOs;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

public class AllAdminDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = string.Empty;

    public required string IsActive { get; set; }
}