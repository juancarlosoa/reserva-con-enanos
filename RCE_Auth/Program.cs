using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using RCE_Auth.CoreData;
using RCE_Auth.UsersRoles.Entities;
using Scalar.AspNetCore;
using DotNetEnv;
using static OpenIddict.Abstractions.OpenIddictConstants;

// Cargar variables de entorno desde el archivo .env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseOpenIddict();
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<AuthDbContext>();
    })
    .AddServer(options =>
    {
        // ✅ Configurar endpoints correctamente
        options.SetAuthorizationEndpointUris("/auth/connect/authorize")
               .SetTokenEndpointUris("/auth/connect/token");

        // ✅ Configurar issuer correcto (debe coincidir con el gateway)
        options.SetIssuer(new Uri("https://gateway:8080/auth"));

        // ✅ Configurar flujos permitidos
        options.AllowAuthorizationCodeFlow()
               .AllowRefreshTokenFlow()
               .RequireProofKeyForCodeExchange();

        // ✅ Registrar scopes
        options.RegisterScopes(
            OpenIddict.Abstractions.OpenIddictConstants.Scopes.OpenId,
            OpenIddict.Abstractions.OpenIddictConstants.Scopes.Email,
            OpenIddict.Abstractions.OpenIddictConstants.Scopes.Profile,
            OpenIddict.Abstractions.OpenIddictConstants.Scopes.Roles,
            "api"
        );

        // ✅ Solo para desarrollo - usar certificados de desarrollo
        if (builder.Environment.IsDevelopment())
        {
            options.AddDevelopmentEncryptionCertificate()
                   .AddDevelopmentSigningCertificate();
        }

        // ✅ Configurar ASP.NET Core integration
        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough()
               .EnableTokenEndpointPassthrough()
               .DisableTransportSecurityRequirement(); // Solo para desarrollo
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";

})
.AddGoogle("Google", options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
    options.CallbackPath = "/signin-google";
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
var app = builder.Build();
app.UseForwardedHeaders();
app.UseRouting();

static string MapPermission(string permission)
{
    return permission switch
    {
        "Endpoints:Authorization" => Permissions.Endpoints.Authorization,
        "Endpoints:Token" => Permissions.Endpoints.Token,

        "GrantTypes:AuthorizationCode" => Permissions.GrantTypes.AuthorizationCode,
        "GrantTypes:RefreshToken" => Permissions.GrantTypes.RefreshToken,
        "ResponseTypes:Code" => Permissions.ResponseTypes.Code,
        "Features:ProofKeyForCodeExchange" => Requirements.Features.ProofKeyForCodeExchange,
        _ => permission
    };
}

using (var scope = app.Services.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var clientsSection = configuration.GetSection("OAuth:Clients");
    var clients = clientsSection.GetChildren();

    foreach (var client in clients)
    {
        var clientId = client["ClientId"];
        if (string.IsNullOrEmpty(clientId))
            continue;

        if (await manager.FindByClientIdAsync(clientId) is null)
        {
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = clientId,
                DisplayName = client["DisplayName"]
            };

            // ✅ Agregar redirect URIs
            var redirectUri = client["RedirectUri"];
            if (!string.IsNullOrEmpty(redirectUri))
                descriptor.RedirectUris.Add(new Uri(redirectUri));

            var postLogoutRedirectUri = client["PostLogoutRedirectUri"];
            if (!string.IsNullOrEmpty(postLogoutRedirectUri))
                descriptor.PostLogoutRedirectUris.Add(new Uri(postLogoutRedirectUri));

            // ✅ Agregar scopes
            var scopes = client.GetSection("Scopes").Get<string[]>();
            if (scopes != null)
            {
                foreach (var sc in scopes)
                {
                    descriptor.Permissions.Add(Permissions.Prefixes.Scope + sc);
                }
            }

            // ✅ Agregar permisos
            var permissions = client.GetSection("Permissions").Get<string[]>();
            if (permissions != null)
            {
                foreach (var permission in permissions)
                {
                    descriptor.Permissions.Add(MapPermission(permission));
                }
            }

            await manager.CreateAsync(descriptor);
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod()
);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultControllerRoute();
app.Run();
