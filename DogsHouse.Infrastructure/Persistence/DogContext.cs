using DogsHouse.Application.Persistence;
using DogsHouse.Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Infrastructure.Persistence
{
    public class DogContext : DbContext, IDogContext
    {
        // .ctor that meets the requirements of the Dependency Injection
        public DogContext(DbContextOptions<DogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applying IEntityTypeConfiguration classes which helps ef core to setup Models
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }

        public DbSet<Dog> Dogs => Set<Dog>();
    }
}
