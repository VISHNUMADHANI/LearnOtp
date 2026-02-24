using Homecare_Dotnet.Models.DTOs;
using Homecare_Dotnet.Models.Entities;

public interface IAdminRepository
{
    Task AddAsync(Admin admin);
    Task<List<AllAdminDTO>> GetAllAsync(); 
        Task<Admin?> GetByEmailAsync(string email);
    Task SaveChangesAsync();
}
