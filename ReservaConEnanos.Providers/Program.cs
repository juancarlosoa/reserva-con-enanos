using ReservaConEnanos.Providers.CoreData;
using Microsoft.EntityFrameworkCore;
using ReservaConEnanos.Providers.EscapeRoomProviders.Services;
using ReservaConEnanos.Providers.EscapeRoomProviders.Repositories;
using ReservaConEnanos.Providers.Rooms.Repositories;
using ReservaConEnanos.Providers.Rooms.Services;
using ReservaConEnanos.Providers.Rooms.Mappings;
using ReservaConEnanos.Providers.EscapeRoomProviders.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5030") // Dirección de tu Blazor (frontend)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(RoomProfile));
builder.Services.AddAutoMapper(typeof(EscapeRoomProviderProfile));

builder.Services.AddDbContext<ProvidersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IEscapeRoomProviderService, EscapeRoomProviderService>();
builder.Services.AddScoped<IEscapeRoomProviderRepository, EscapeRoomProviderRepository>();
    
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();