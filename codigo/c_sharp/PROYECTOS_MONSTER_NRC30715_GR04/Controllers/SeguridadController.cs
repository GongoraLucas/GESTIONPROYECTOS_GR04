using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize(Policy = "USR")]
public class SeguridadController : Controller
{
    private readonly IPerfilService _perfilService;

    public SeguridadController(
        IPerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpGet]
    public async Task<IActionResult>
        AsignarUsuarioPerfil(
            string? perfilCodigo)
    {
        var model =
            await _perfilService
                .ObtenerUsuariosPerfilAsync(
                    perfilCodigo ?? string.Empty);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AsignarUsuario(
    int usuarioId,
    string perfilCodigo)
    {
        await _perfilService.AsignarUsuarioAsync(
            usuarioId,
            perfilCodigo);

        return RedirectToAction(
            nameof(AsignarUsuarioPerfil),
            new { perfilCodigo });
    }

    [HttpPost]
    public async Task<IActionResult> RetirarUsuario(
        int usuarioId,
        string perfilCodigo)
    {
        await _perfilService.RetirarUsuarioAsync(
            usuarioId,
            perfilCodigo);

        return RedirectToAction(
            nameof(AsignarUsuarioPerfil),
            new { perfilCodigo });
    }

    [HttpGet]
    public async Task<IActionResult>
    AsignarOpcionPerfil(
        string? perfilCodigo)
    {
        var model =
            await _perfilService
                .ObtenerOpcionesPerfilAsync(
                    perfilCodigo ?? string.Empty);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult>
    AsignarOpcionPerfil(
        string perfilCodigo,
        string opcionCodigo)
    {
        await _perfilService
            .AsignarOpcionAsync(
                perfilCodigo,
                opcionCodigo);

        return RedirectToAction(
            nameof(AsignarOpcionPerfil),
            new
            {
                perfilCodigo
            });
    }

    [HttpPost]
    public async Task<IActionResult>
    RetirarOpcionPerfil(
        string perfilCodigo,
        string opcionCodigo)
    {
        await _perfilService
            .RetirarOpcionAsync(
                perfilCodigo,
                opcionCodigo);

        return RedirectToAction(
            nameof(AsignarOpcionPerfil),
            new
            {
                perfilCodigo
            });
    }
}