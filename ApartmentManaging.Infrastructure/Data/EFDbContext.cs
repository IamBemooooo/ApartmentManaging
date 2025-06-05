using Microsoft.EntityFrameworkCore;

namespace ApartmentManaging.Infrastructure.Data
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() { }

        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=FilmMoi;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
