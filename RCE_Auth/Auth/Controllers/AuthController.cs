using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.Server.AspNetCore;
using RCE_Auth.UsersRoles.Entities;
using RCE_Auth.Auth.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using OpenIddict.EntityFrameworkCore.Models;

namespace RCE_Auth.Auth.Controllers;

[Route("connect")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        ILogger<AuthController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet("authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (request == null)
        {
            _logger.LogWarning("Invalid OpenID Connect request");
            return BadRequest("Invalid OpenID Connect request");
        }

        // Verificar que el cliente existe
        if (string.IsNullOrEmpty(request.ClientId))
        {
            _logger.LogWarning("ClientId is null or empty");
            return BadRequest("Invalid client");
        }

        // Verificar si el usuario ya está autenticado
        var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        if (result?.Principal != null)
        {
            // Si el usuario ya está autenticado, procedemos con la autorización
            return SignIn(result.Principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        // Si no está autenticado, redirigimos al login
        return Challenge(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties
            {
                RedirectUri = Request.PathBase + Request.Path + Request.QueryString
            });
    }

    [HttpPost("authorize")]
    public async Task<IActionResult> AuthorizePost(string email, string password)
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (request == null)
        {
            _logger.LogWarning("Invalid OpenID Connect request");
            return BadRequest("Invalid OpenID Connect request");
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning("User not found: {Email}", email);
            return Unauthorized(new { error = "Invalid credentials" });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Invalid password for user: {Email}", email);
            return Unauthorized(new { error = "Invalid credentials" });
        }

        // Crear claims principal
        var principal = await _signInManager.CreateUserPrincipalAsync(user);

        // Emite el código de autorización
        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (request == null)
        {
            _logger.LogWarning("Invalid OpenID Connect request");
            return BadRequest("Invalid OpenID Connect request");
        }

        if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
        {
            // Validar el token de actualización
            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Authentication failed");
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The specified authorization code is invalid."
                    }));
            }

            // Crear un nuevo principal basado en la información del token
            var principal = new ClaimsPrincipal(new ClaimsIdentity(result.Principal.Claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme));

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new NotImplementedException("The specified grant type is not implemented.");
    }

    public static async Task SeedClientsAsync(
        OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> manager,
        IConfiguration configuration)
    {
        var clients = configuration.GetSection("OAuth:Clients")
            .Get<List<OAuthClientConfiguration>>();

        if (clients == null || !clients.Any())
        {
            throw new InvalidOperationException("No OAuth clients configured");
        }

        foreach (var client in clients)
        {
            if (string.IsNullOrEmpty(client.ClientId) || string.IsNullOrEmpty(client.RedirectUri))
            {
                throw new InvalidOperationException($"Invalid configuration for client {client.ClientId}");
            }

            if (!Uri.TryCreate(client.RedirectUri, UriKind.Absolute, out _))
            {
                throw new InvalidOperationException($"Invalid redirect URI format for client {client.ClientId}");
            }

            var existingClient = await manager.FindByClientIdAsync(client.ClientId);
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = client.ClientId,
                ClientSecret = client.ClientSecret,
                ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                DisplayName = client.DisplayName,
                RedirectUris = { new Uri(client.RedirectUri) },
                // Add permissions after descriptor creation
            };

            foreach (var permission in client.Permissions.Select(p => MapPermission(p)))
            {
                descriptor.Permissions.Add(permission);
            }

            if (existingClient == null)
            {
                await manager.CreateAsync(descriptor);
            }
            else
            {
                await manager.UpdateAsync(existingClient, descriptor);
            }
        }
    }

    private static string MapPermission(string permission)
    {
        return permission switch
        {
            "Endpoints:Authorization" => OpenIddictConstants.Permissions.Endpoints.Authorization,
            "Endpoints:Token" => OpenIddictConstants.Permissions.Endpoints.Token,
            "GrantTypes:AuthorizationCode" => OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
            "GrantTypes:RefreshToken" => OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
            "ResponseTypes:Code" => OpenIddictConstants.Permissions.ResponseTypes.Code,
            "Scopes:Profile" => OpenIddictConstants.Permissions.Scopes.Profile,
            "Scopes:Email" => OpenIddictConstants.Permissions.Scopes.Email,
            "Scopes:OpenId" => OpenIddictConstants.Permissions.Prefixes.Scope + "openid",
            "Scopes:Api" => OpenIddictConstants.Permissions.Prefixes.Scope + "api",
            "Features:ProofKeyForCodeExchange" => "features:proof_key_for_code_exchange",
            _ => throw new InvalidOperationException($"Unknown permission: {permission}")
        };
    }
}