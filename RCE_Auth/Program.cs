using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using RCE_Auth.CoreData;
using RCE_Auth.UsersRoles.Entities;
using Scalar.AspNetCore;

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
        options.SetAuthorizationEndpointUris("/auth/connect/authorize");
        options.SetTokenEndpointUris("/auth/connect/token");
        options.SetIssuer(new Uri("https://localhost:5101/auth"));

        options.AllowAuthorizationCodeFlow()
               .RequireProofKeyForCodeExchange();

        options.AllowRefreshTokenFlow();

        options.RegisterScopes("openid", "profile", "email", "api");

        options.AcceptAnonymousClients();

        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();

        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough()
               .EnableTokenEndpointPassthrough()
               .EnableUserInfoEndpointPassthrough();
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
        "Endpoints:Authorization" => OpenIddictConstants.Permissions.Endpoints.Authorization,
        "Endpoints:Token" => OpenIddictConstants.Permissions.Endpoints.Token,
        "GrantTypes:AuthorizationCode" => OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
        "GrantTypes:RefreshToken" => OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
        "ResponseTypes:Code" => OpenIddictConstants.Permissions.ResponseTypes.Code,
        "Features:ProofKeyForCodeExchange" => OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange,
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
                ClientSecret = client["ClientSecret"],
                DisplayName = client["DisplayName"]
            };

            var redirectUri = client["RedirectUri"];
            if (!string.IsNullOrEmpty(redirectUri))
                descriptor.RedirectUris.Add(new Uri(redirectUri));
            var scopes = client.GetSection("Scopes").Get<string[]>();
            if (scopes != null)
            {
                foreach (var sc in scopes)
                {
                    descriptor.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + sc);
                }
            }
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
