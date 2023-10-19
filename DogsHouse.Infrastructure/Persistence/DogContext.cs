using DogsHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DogsHouse.Infrastructure.Persistence
{
    /// <summary>
    /// Ef Core custom database context.
    /// </summary>
    public class DogContext : DbContext
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

        /// <summary>
        /// Gives access to the Dog table.
        /// </summary>
        public DbSet<Dog> Dogs => Set<Dog>();
    }
}
