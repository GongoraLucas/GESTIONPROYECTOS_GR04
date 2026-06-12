using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IEmpleadoService
{
    Task<(List<EmpleadoViewModel> Empleados,
          int TotalRegistros)>
        ObtenerTodosAsync(
            string? buscar,
            int pagina,
            int registrosPorPagina);

    Task<EmpleadoViewModel>
        ObtenerFormularioAsync(EmpleadoViewModel? model = null);

    Task CrearAsync(
        EmpleadoViewModel model);

    Task<EmpleadoViewModel?>
        ObtenerPorIdAsync(
            string codigo);

    Task ActualizarAsync(
        EmpleadoViewModel model);

    Task<EmpleadoViewModel?>
        ObtenerPorCodigoAsync(
            string codigo);

    Task EliminarAsync(
        string codigo);

    Task<EmpleadoViewModel?>
        ObtenerDetalleAsync(
            string codigo);
}