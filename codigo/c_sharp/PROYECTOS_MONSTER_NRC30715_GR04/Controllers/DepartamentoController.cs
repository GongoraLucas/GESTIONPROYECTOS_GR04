using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize(Policy = "EMP")]
public class DepartamentoController : Controller
{
    private readonly IDepartamentoService _service;
    private readonly IReporteService      _reporteService;

    public DepartamentoController(
        IDepartamentoService service,
        IReporteService reporteService)
    {
        _service        = service;
        _reporteService = reporteService;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  INDEX
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Index(
        string? buscar,
        int pagina = 1)
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
    public IActionResult Create()
        => View(new DepartamentoViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(DepartamentoViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _service.CrearAsync(model);
            TempData["Success"] = "Departamento creado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(model);
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    //  EDIT
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var model = await _service.ObtenerPorIdAsync(id);
        if (model == null)
            return NotFound();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DepartamentoViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _service.ActualizarAsync(model);
            TempData["Success"] = "Departamento actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(model);
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    //  DELETE
    // ─────────────────────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var model = await _service.ObtenerPorCodigoAsync(id);
        if (model == null)
            return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmado(string codigo)
    {
        try
        {
            await _service.EliminarAsync(codigo);
            TempData["Success"] = "Departamento eliminado correctamente.";
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
    public async Task<IActionResult> Details(string id)
    {
        var model = await _service.ObtenerPorIdAsync(id);
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
        var bytes = await _reporteService.GenerarDepartamentosPdfAsync();
        return File(bytes, "application/pdf",
            $"departamentos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> ExportarExcel()
    {
        var bytes = await _reporteService.GenerarDepartamentosExcelAsync();
        return File(bytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"departamentos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    public async Task<IActionResult> ExportarCsv()
    {
        var bytes = await _reporteService.GenerarDepartamentosCsvAsync();
        return File(bytes, "text/csv",
            $"departamentos_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
}
