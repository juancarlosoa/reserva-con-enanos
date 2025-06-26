using Microsoft.EntityFrameworkCore;
using RCE_Providers.CoreData;
using RCE_Providers.EscapeRoomProviders.Mappings;
using RCE_Providers.EscapeRoomProviders.Repositories;
using RCE_Providers.EscapeRoomProviders.Services;
using RCE_Providers.Rooms.Mappings;
using RCE_Providers.Rooms.Repositories;
using RCE_Providers.Rooms.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(RoomProfile));
builder.Services.AddAutoMapper(typeof(EscapeRoomProviderProfile));

builder.Services.AddDbContext<ProvidersDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IEscapeRoomProviderService, EscapeRoomProviderService>();
builder.Services.AddScoped<IEscapeRoomProviderRepository, EscapeRoomProviderRepository>();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProvidersDbContext>();
    db.Database.Migrate();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
