using DogsHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogsHouse.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configures the <see cref="Dog"/> database entity for code-first migrations.
    /// </summary>
    internal class DogConfigurations : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            // Since dogs can't have same names, we'll make this property a primary key.
            builder.HasKey(d => d.name);
            // All other properties are just required.
            builder.Property(d => d.color).IsRequired();
            builder.Property(d => d.tail_length).IsRequired();
            builder.Property(d => d.weight).IsRequired();
        }
    }
}
