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
    private readonly IPerfilService _perfilService;
    private readonly PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces.IEmailService _emailService;

    public AccountController(
     IAuthService authService,
     IPerfilService perfilService,
     PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces.IEmailService emailService)
    {
        _authService = authService;
        _perfilService = perfilService;
        _emailService = emailService;
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

        var perfiles =
    await _perfilService
        .ObtenerPerfilesUsuarioAsync(
            usuario.Id);

        foreach (var perfil in perfiles)
        {
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    perfil));
        }

        var opciones =
    await _perfilService
        .ObtenerOpcionesUsuarioAsync(
            usuario.Id);

        foreach (var opcion in opciones)
        {
            claims.Add(
                new Claim(
                    "OPCION",
                    opcion));
        }

        // Añadir claim de primer ingreso para control de UI
        claims.Add(new Claim("PRIMER_INGRESO", usuario.PrimerIngreso ? "True" : "False"));

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

        // Construir URL absoluto para reset
        var resetUrl = Url.Action(
            action: "ResetPassword",
            controller: "Account",
            values: new { token },
            protocol: Request.Scheme);

        var subject = "Recuperación de contraseña";
        var html = $@"<p>Se solicitó restablecer la contraseña. Haga clic en el siguiente enlace para establecer una nueva contraseña:</p>
                      <p><a href='{resetUrl}'>Restablecer contraseña</a></p>
                      <p>Si usted no solicitó este correo, puede ignorarlo.</p>";

        try
        {
            await _emailService.SendAsync(model.Email, subject, html);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al enviar el correo: " + ex.Message);
            return View(model);
        }

        return RedirectToAction(nameof(TokenGenerado));
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