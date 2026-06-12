using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize(Policy = "USR")]
public class UsuarioController : Controller
{
    private readonly IUsuarioService _service;
    private readonly PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes.IReporteService _reporteService;

    public UsuarioController(IUsuarioService service,
        PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes.IReporteService reporteService)
    {
        _service = service;
        _reporteService = reporteService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? buscar, int pagina = 1)
    {
        const int registrosPorPagina = 10;

        var resultado = await _service.ObtenerTodosAsync(buscar, pagina, registrosPorPagina);

        ViewBag.Buscar = buscar;
        ViewBag.Pagina = pagina;
        ViewBag.RegistrosPorPagina = registrosPorPagina;
        ViewBag.TotalRegistros = resultado.TotalRegistros;
        ViewBag.TotalPaginas = (int)Math.Ceiling((double)resultado.TotalRegistros / registrosPorPagina);

        return View(resultado.Usuarios);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = await _service.ObtenerFormularioAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsuarioViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            ModelState.AddModelError("Password", "La contraseña es requerida para el primer ingreso.");
        }

        if (!ModelState.IsValid)
        {
            var repopulatedModel = await _service.ObtenerFormularioAsync(model);
            return View(repopulatedModel);
        }

        try
        {
            await _service.CrearAsync(model);
            TempData["Success"] = "Usuario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al crear el usuario: " + ex.Message);
            var repopulatedModel = await _service.ObtenerFormularioAsync(model);
            return View(repopulatedModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _service.ObtenerPorIdAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var repopulatedModel = await _service.ObtenerFormularioAsync(model);
            return View(repopulatedModel);
        }

        try
        {
            await _service.ActualizarAsync(model);
            TempData["Success"] = "Usuario actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al actualizar el usuario: " + ex.Message);
            var repopulatedModel = await _service.ObtenerFormularioAsync(model);
            return View(repopulatedModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await _service.ObtenerPorIdAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmado(int id)
    {
        try
        {
            await _service.EliminarAsync(id);
            TempData["Success"] = "Usuario eliminado correctamente";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> CambiarEstado(int usuarioId, string estadoCodigo)
    {
        await _service.CambiarEstadoAsync(usuarioId, estadoCodigo);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RestablecerPasswordAdmin(int usuarioId)
    {
        // Restablecer a contraseña temporal
        await _service.RestablecerPasswordAdminAsync(usuarioId, "Temp1234");
        TempData["Success"] = "Contraseña restablecida y el usuario deberá cambiarla en su primer ingreso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ExportarPdf()
    {
        var bytes = await _reporteService.GenerarUsuariosPdfAsync();
        return File(bytes, "application/pdf", $"usuarios_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> ExportarExcel()
    {
        var bytes = await _reporteService.GenerarUsuariosExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"usuarios_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    public async Task<IActionResult> ExportarCsv()
    {
        var bytes = await _reporteService.GenerarUsuariosCsvAsync();
        return File(bytes, "text/csv", $"usuarios_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
}
