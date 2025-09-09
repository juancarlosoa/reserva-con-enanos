using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ✅ Health checks
builder.Services.AddHealthChecks();

// ✅ Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("ApiPolicy", limiterOptions =>
    {
        limiterOptions.PermitLimit = 100;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 10;
    });
});

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

// ✅ Configurar autenticación con OpenIddict
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://gateway:8080/auth";
    options.Audience = "api";
    options.RequireHttpsMetadata = false; // Solo para desarrollo
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://gateway:8080/auth",
        ValidAudiences = new[] { "api" },
        ClockSkew = TimeSpan.Zero
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
            builder.WithOrigins("https://localhost:5173", "http://localhost:5174")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // ✅ Para cookies/auth
        }
    );
});

// ✅ Configuración adicional de seguridad
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                              Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// ✅ Orden correcto de middlewares
app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseRateLimiter(); // ✅ Rate limiting
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

// ✅ Health check endpoint
app.MapHealthChecks("/health");

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UseCors("AllowFrontend");
});

app.Run();
