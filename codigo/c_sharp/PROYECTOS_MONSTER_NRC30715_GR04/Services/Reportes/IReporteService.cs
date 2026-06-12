namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

public interface IReporteService
{
    // ── Empleados ──────────────────────────────────────────────────────────
    
    Task<byte[]> GenerarEmpleadosPdfAsync();

   
    Task<byte[]> GenerarEmpleadosExcelAsync();

    
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

    // ── Usuarios ──────────────────────────────────────────────────────────
    Task<byte[]> GenerarUsuariosPdfAsync();
    Task<byte[]> GenerarUsuariosExcelAsync();
    Task<byte[]> GenerarUsuariosCsvAsync();
}
