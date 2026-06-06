using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(
        IAuthService authService)
    {
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

   

    [HttpPost]
    public async Task<IActionResult> Login(
    LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var usuario =
            await _authService.ValidarUsuarioAsync(
                model.Usuario,
                model.Password);

        if (usuario == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "Usuario o contraseña incorrectos");

            return View(model);
        }

        var claims = new List<Claim>
    {
        new Claim(
            ClaimTypes.Name,
            usuario.Login),

        new Claim(
            "UsuarioId",
            usuario.Id.ToString())
    };

        var identity =
            new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

        var principal =
            new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return RedirectToAction(
            "Index",
            "Home");
    }



}