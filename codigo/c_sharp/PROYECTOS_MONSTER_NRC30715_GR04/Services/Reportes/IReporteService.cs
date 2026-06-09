namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

public interface IReporteService
{
    // ── Empleados ──────────────────────────────────────────────────────────
    /// <summary>Genera un reporte PDF con la lista de todos los empleados.</summary>
    Task<byte[]> GenerarEmpleadosPdfAsync();

    /// <summary>Genera un reporte Excel (.xlsx) con la lista de todos los empleados.</summary>
    Task<byte[]> GenerarEmpleadosExcelAsync();

    /// <summary>Genera un reporte CSV con la lista de todos los empleados.</summary>
    Task<byte[]> GenerarEmpleadosCsvAsync();

    // ── Departamentos ──────────────────────────────────────────────────────
    Task<byte[]> GenerarDepartamentosPdfAsync();
    Task<byte[]> GenerarDepartamentosExcelAsync();
    Task<byte[]> GenerarDepartamentosCsvAsync();

    // ── Sexo ──────────────────────────────────────────────────────────────
    Task<byte[]> GenerarSexosPdfAsync();
    Task<byte[]> GenerarSexosExcelAsync();
    Task<byte[]> GenerarSexosCsvAsync();

    // ── Estado Civil ──────────────────────────────────────────────────────
    Task<byte[]> GenerarEstadosCivilesPdfAsync();
    Task<byte[]> GenerarEstadosCivilesExcelAsync();
    Task<byte[]> GenerarEstadosCivilesCsvAsync();

    // ── Cargos ────────────────────────────────────────────────────────────
    Task<byte[]> GenerarCargosPdfAsync();
    Task<byte[]> GenerarCargosExcelAsync();
    Task<byte[]> GenerarCargosCsvAsync();
}
