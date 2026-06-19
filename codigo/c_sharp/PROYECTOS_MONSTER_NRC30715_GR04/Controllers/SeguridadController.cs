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

    [HttpPost]
    public async Task<IActionResult> GuardarUsuariosPerfil(
        string perfilCodigo,
        List<int> usuarioIds)
    {
        if (string.IsNullOrEmpty(perfilCodigo))
        {
            return Json(new { success = false, message = "El perfil es requerido." });
        }

        try
        {
            usuarioIds ??= new List<int>();
            await _perfilService.GuardarUsuariosPerfilAsync(perfilCodigo, usuarioIds);
            return Json(new { success = true, message = "Asignaciones guardadas exitosamente." });
        }
        catch (System.Exception ex)
        {
            return Json(new { success = false, message = $"Error al guardar: {ex.Message}" });
        }
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

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { success = true, message = "Opción asignada exitosamente." });
        }

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

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { success = true, message = "Opción retirada exitosamente." });
        }

        return RedirectToAction(
            nameof(AsignarOpcionPerfil),
            new
            {
                perfilCodigo
            });
    }
}