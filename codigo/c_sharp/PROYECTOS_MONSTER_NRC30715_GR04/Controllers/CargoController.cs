using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize(Policy = "EMP")]
public class CargoController : Controller
{
    private readonly ICargoService   _service;
    private readonly IReporteService _reporteService;

    public CargoController(
        ICargoService service,
        IReporteService reporteService)
    {
        _service        = service;
        _reporteService = reporteService;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  INDEX
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Index(string? buscar, int pagina = 1)
    {
        const int registrosPorPagina = 10;

        var resultado = await _service.ObtenerTodosAsync(
            buscar, pagina, registrosPorPagina);

        ViewBag.Buscar             = buscar;
        ViewBag.PaginaActual       = pagina;
        ViewBag.RegistrosPorPagina = registrosPorPagina;
        ViewBag.TotalRegistros     = resultado.TotalRegistros;
        ViewBag.TotalPaginas       = (int)Math.Ceiling(
            (double)resultado.TotalRegistros / registrosPorPagina);

        return View(resultado.Items);
    }

    // ─────────────────────────────────────────────────────────────────────
    //  CREATE
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = await _service.ObtenerFormularioAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CargoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Departamentos = (await _service.ObtenerFormularioAsync()).Departamentos;
            return View(model);
        }

        try
        {
            await _service.CrearAsync(model);
            TempData["Success"] = "Cargo creado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            model.Departamentos = (await _service.ObtenerFormularioAsync()).Departamentos;
            return View(model);
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    //  EDIT  – clave compuesta: dep + cod en la ruta
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Edit(string dep, string cod)
    {
        var model = await _service.ObtenerPorIdAsync(dep, cod);
        if (model == null)
            return NotFound();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CargoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Departamentos = (await _service.ObtenerFormularioAsync()).Departamentos;
            return View(model);
        }

        try
        {
            await _service.ActualizarAsync(
                model,
                model.DepartamentoCodigoOriginal,
                model.CodigoOriginal);

            TempData["Success"] = "Cargo actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            model.Departamentos = (await _service.ObtenerFormularioAsync()).Departamentos;
            return View(model);
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    //  DELETE
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Delete(string dep, string cod)
    {
        var model = await _service.ObtenerPorCodigoAsync(dep, cod);
        if (model == null)
            return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmado(
        string departamentoCodigo,
        string codigo)
    {
        try
        {
            await _service.EliminarAsync(departamentoCodigo, codigo);
            TempData["Success"] = "Cargo eliminado correctamente.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    // ─────────────────────────────────────────────────────────────────────
    //  DETAILS
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Details(string dep, string cod)
    {
        var model = await _service.ObtenerPorIdAsync(dep, cod);
        if (model == null)
            return NotFound();
        return View(model);
    }

    // ─────────────────────────────────────────────────────────────────────
    //  REPORTES
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> ExportarPdf()
    {
        var bytes = await _reporteService.GenerarCargosPdfAsync();
        return File(bytes, "application/pdf",
            $"cargos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> ExportarExcel()
    {
        var bytes = await _reporteService.GenerarCargosExcelAsync();
        return File(bytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"cargos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    public async Task<IActionResult> ExportarCsv()
    {
        var bytes = await _reporteService.GenerarCargosCsvAsync();
        return File(bytes, "text/csv",
            $"cargos_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
}
