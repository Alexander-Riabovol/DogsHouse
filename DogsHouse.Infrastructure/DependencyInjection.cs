using DogsHouse.Application.Persistence;
using DogsHouse.Domain.Entities;
using DogsHouse.Infrastructure.Persistence;
using DogsHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DogsHouse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           string? connectionString)
        {
            // Add Repositories
            services.AddScoped<IDogRepository, DogRepository>();

            // Add DB context
            services.AddDbContext<DogContext>(options =>
            {
                options.UseSqlServer(connectionString,
                // Set Data project as source for migrations
                assembly =>
                {
                    assembly.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    assembly.EnableRetryOnFailure();
                });
            });
            return services;
        }

        public static void ApplyPendingMigrations(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DogContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    // Apply migrations
                    context.Database.Migrate();

                    // Prepopulate DB according to the task requirements
                    context.Add(new Dog() { name = "Neo", color = "red&amber", tail_length = 22, weight = 32 });
                    context.Add(new Dog() { name = "Jessy", color = "black&white", tail_length = 7, weight = 14 });
                    context.SaveChanges();
                }
            }
        }
    }
}
