#region

using Domain;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Infrastructure.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> ops)
            : base(ops)
        {
        }
        
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: Remove this and configure all via Startup
            optionsBuilder.UseSqlite("Data Source=AppDb");
        }
    }
}