using DogsHouse.Application;
using DogsHouse.Infrastructure;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.

builder.Services.AddApplication()
                .AddInfrastructure(
    builder.Configuration.GetConnectionString(/*AppData.InDocker ? "Docker" : */"Default"));

builder.Services.AddControllers();

// Add Rate Limiter
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    // Too Many Requests
    rateLimiterOptions.RejectionStatusCode = 429;

    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.QueueLimit = 0;
        options.Window = TimeSpan.FromSeconds(1);
        // You can configure the amount of allowed requests per second in appsettings.json
        int perSecond;
        if (builder.Configuration["AllowedRequestsPerSecond"] == null ||
            !int.TryParse(builder.Configuration["AllowedRequestsPerSecond"], out perSecond))
        {
            perSecond = 10;
        }
        options.PermitLimit = perSecond;
    });
});

// Add Mappings
var config = TypeAdapterConfig.GlobalSettings;
//Scan this assembly for the configs
config.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

var app = builder.Build();

app.MapControllers();

// curl -X GET http://localhost:5171/ping
app.MapGet("/ping", () =>
{
    return "Dogshouseservice.Version1.0.1";
});

app.UseRateLimiter();

app.ApplyPendingMigrations();

app.Run();
