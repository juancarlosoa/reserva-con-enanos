using Microsoft.EntityFrameworkCore;
using RCE_Providers.CoreData;
using RCE_Providers.EscapeRoomProviders.Mappings;
using RCE_Providers.EscapeRoomProviders.Repositories;
using RCE_Providers.EscapeRoomProviders.Services;
using RCE_Providers.Rooms.Mappings;
using RCE_Providers.Rooms.Repositories;
using RCE_Providers.Rooms.Services;
using RCE_Providers.Common.Middleware;
using RCE_Providers.Common.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
using DotNetEnv;
using Serilog;

// Cargar variables de entorno
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// AutoMapper
builder.Services.AddAutoMapper(typeof(RoomProfile));
builder.Services.AddAutoMapper(typeof(EscapeRoomProviderProfile));

// Database
builder.Services.AddDbContext<ProvidersDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories y Services
builder.Services.AddScoped<IEscapeRoomProviderService, EscapeRoomProviderService>();
builder.Services.AddScoped<IEscapeRoomProviderRepository, EscapeRoomProviderRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database");

// Controllers
builder.Services.AddControllers();

// Swagger (solo para desarrollo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProvidersDbContext>();
    db.Database.Migrate();
}

// Middleware pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<InternalRateLimitMiddleware>();

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseHttpsRedirection();

// Health checks
app.MapHealthChecks("/health");

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
