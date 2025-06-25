using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .ConfigureHttpClient((context, handler) =>
    {
        if (handler is SocketsHttpHandler socketsHandler)
        {
            socketsHandler.SslOptions = new System.Net.Security.SslClientAuthenticationOptions
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
        }
    })
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "RCE",
            ValidAudience = "RCE_Client",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("clave-super-secreta")
            ),
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAuthenticatedUser", policy => policy.RequireAuthenticatedUser())
    .AddPolicy("ProvidersOnly", policy => policy.RequireRole("Provider"))
    .AddPolicy("RoomsOnly", policy => policy.RequireRole("Room"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        builder =>
        {
            builder.WithOrigins("https://heroui:5173").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.Run();
