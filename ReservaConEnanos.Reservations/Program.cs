using ReservaConEnanos.Reservations.Mappings;
using ReservaConEnanos.Reservations.Repositories;
using ReservaConEnanos.Reservations.Repositories.Interfaces;
using ReservaConEnanos.Reservations.Services;
using ReservaConEnanos.Reservations.Services.Interfaces;
using ReservaConEnanos.Reservations.CoreData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ReservationProfile));

builder.Services.AddDbContext<ReservationsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
