using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize]
public class ReporteController : Controller
{
    private readonly IReporteService _reporteService;

    public ReporteController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    // ═════════════════════════════════════════════════════════════════════
    //  VISTAS DEL CENTRO DE REPORTES
    // ═════════════════════════════════════════════════════════════════════

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public IActionResult Personal()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Policy = "USR")]
    public IActionResult Seguridad()
    {
        return View();
    }

    // ═════════════════════════════════════════════════════════════════════
    //  DESCARGAS - PERSONAL
    // ═════════════════════════════════════════════════════════════════════

    #region Empleados
    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarEmpleadoPdf()
    {
        var bytes = await _reporteService.GenerarEmpleadosPdfAsync();
        return File(bytes, "application/pdf", $"empleados_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarEmpleadoExcel()
    {
        var bytes = await _reporteService.GenerarEmpleadosExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"empleados_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarEmpleadoCsv()
    {
        var bytes = await _reporteService.GenerarEmpleadosCsvAsync();
        return File(bytes, "text/csv", $"empleados_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
    #endregion

    #region Departamentos
    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarDepartamentoPdf()
    {
        var bytes = await _reporteService.GenerarDepartamentosPdfAsync();
        return File(bytes, "application/pdf", $"departamentos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarDepartamentoExcel()
    {
        var bytes = await _reporteService.GenerarDepartamentosExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"departamentos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarDepartamentoCsv()
    {
        var bytes = await _reporteService.GenerarDepartamentosCsvAsync();
        return File(bytes, "text/csv", $"departamentos_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
    #endregion

    #region Cargos
    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarCargoPdf()
    {
        var bytes = await _reporteService.GenerarCargosPdfAsync();
        return File(bytes, "application/pdf", $"cargos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarCargoExcel()
    {
        var bytes = await _reporteService.GenerarCargosExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"cargos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarCargoCsv()
    {
        var bytes = await _reporteService.GenerarCargosCsvAsync();
        return File(bytes, "text/csv", $"cargos_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
    #endregion

    #region Sexos
    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarSexoPdf()
    {
        var bytes = await _reporteService.GenerarSexosPdfAsync();
        return File(bytes, "application/pdf", $"sexos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarSexoExcel()
    {
        var bytes = await _reporteService.GenerarSexosExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"sexos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarSexoCsv()
    {
        var bytes = await _reporteService.GenerarSexosCsvAsync();
        return File(bytes, "text/csv", $"sexos_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
    #endregion

    #region Estados Civiles
    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarEstadoCivilPdf()
    {
        var bytes = await _reporteService.GenerarEstadosCivilesPdfAsync();
        return File(bytes, "application/pdf", $"estados_civiles_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarEstadoCivilExcel()
    {
        var bytes = await _reporteService.GenerarEstadosCivilesExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"estados_civiles_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    [Authorize(Policy = "EMP")]
    public async Task<IActionResult> DescargarEstadoCivilCsv()
    {
        var bytes = await _reporteService.GenerarEstadosCivilesCsvAsync();
        return File(bytes, "text/csv", $"estados_civiles_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
    #endregion

    // ═════════════════════════════════════════════════════════════════════
    //  DESCARGAS - SEGURIDAD
    // ═════════════════════════════════════════════════════════════════════

    #region Usuarios
    [HttpGet]
    [Authorize(Policy = "USR")]
    public async Task<IActionResult> DescargarUsuarioPdf()
    {
        var bytes = await _reporteService.GenerarUsuariosPdfAsync();
        return File(bytes, "application/pdf", $"usuarios_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }

    [HttpGet]
    [Authorize(Policy = "USR")]
    public async Task<IActionResult> DescargarUsuarioExcel()
    {
        var bytes = await _reporteService.GenerarUsuariosExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"usuarios_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
    }

    [HttpGet]
    [Authorize(Policy = "USR")]
    public async Task<IActionResult> DescargarUsuarioCsv()
    {
        var bytes = await _reporteService.GenerarUsuariosCsvAsync();
        return File(bytes, "text/csv", $"usuarios_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
    #endregion
}
