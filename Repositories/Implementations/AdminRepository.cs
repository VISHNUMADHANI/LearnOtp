using AutoMapper;
using Homecare_Dotnet.Data;
using Homecare_Dotnet.Models.DTOs;
using Homecare_Dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly HomeCareDbContext _context;
    private readonly IMapper _mapper;

    public AdminRepository(HomeCareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddAsync(Admin admin)
    {
        await _context.Admins.AddAsync(admin);
    }

    public async Task<List<AllAdminDTO>> GetAllAsync()
    {
        if (_context.Admins == null)
            return new List<AllAdminDTO>();

        var admins = await _context.Admins
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        return _mapper.Map<List<AllAdminDTO>>(admins);
    }


    public async Task<Admin?> GetByEmailAsync(string email)
    {
        return await _context.Admins
            .FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
    }

    public Task GetByEmailAsync(object email)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

   
}
