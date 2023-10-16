using DogsHouse.Application;
using DogsHouse.Contracts;
using DogsHouse.Domain;
using DogsHouse.Infrastructure;
using Mapster;
using MapsterMapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.

builder.Services.AddApplication()
                .AddInfrastructure();

builder.Services.AddControllers();

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

app.Run();
