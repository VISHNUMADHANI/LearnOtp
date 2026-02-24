using Microsoft.EntityFrameworkCore;
using Homecare_Dotnet.Models.Entities;

namespace Homecare_Dotnet.Data
{
    public class HomeCareDbContext : DbContext
    {
        public HomeCareDbContext(DbContextOptions<HomeCareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
    }
}
