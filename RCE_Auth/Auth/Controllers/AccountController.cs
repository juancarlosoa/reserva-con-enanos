using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCE_Auth.Auth.Models;
using RCE_Auth.UsersRoles.Entities;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("/account/login")]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost("/account/login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(model);
        }

        // Redirige al flujo original de OpenIddict
        if (!string.IsNullOrEmpty(model.ReturnUrl))
            return Redirect(model.ReturnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("/account/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}