using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;
using System.Security.Claims;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
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

        
        if (usuario.PrimerIngreso)
        {
            return RedirectToAction(
                "CambiarPassword",
                "Account");
        }

        return RedirectToAction(
            "Index",
            "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(
            "Login",
            "Account");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CambiarPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CambiarPassword(
    CambiarPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var usuarioId =
            int.Parse(
                User.FindFirst("UsuarioId")!.Value);

        bool resultado =
            await _authService.CambiarPasswordAsync(
                usuarioId,
                model.PasswordActual,
                model.PasswordNueva);

        if (!resultado)
        {
            ModelState.AddModelError(
                string.Empty,
                "La contraseña actual es incorrecta");

            return View(model);
        }

        TempData["Success"] =
            "Contraseña actualizada correctamente";

        return RedirectToAction(
            "Index",
            "Home");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult RecuperarPassword()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RecuperarPassword(
    RecuperarPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var token =
            await _authService
                .GenerarTokenRecuperacionAsync(
                    model.Email);

        if (token == null)
        {
            ModelState.AddModelError(
                string.Empty,
                "No existe un usuario con ese correo");

            return View(model);
        }

        TempData["Token"] = token;

        return RedirectToAction(
            nameof(TokenGenerado));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult TokenGenerado()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult ResetPassword(
    string token)
    {
        var model =
            new ResetPasswordViewModel
            {
                Token = token
            };

        return View(model);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ResetPassword(
    ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        bool resultado =
            await _authService
                .RestablecerPasswordAsync(
                    model.Token,
                    model.PasswordNueva);

        if (!resultado)
        {
            ModelState.AddModelError(
                string.Empty,
                "Token inválido o expirado");

            return View(model);
        }

        TempData["Success"] =
            "Contraseña restablecida correctamente";

        return RedirectToAction(
            nameof(Login));
    }



}