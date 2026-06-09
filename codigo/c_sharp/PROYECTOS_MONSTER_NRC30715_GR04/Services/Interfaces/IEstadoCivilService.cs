using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IEstadoCivilService
{
    Task<(List<EstadoCivilViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(string? buscar, int pagina, int registrosPorPagina);

    Task CrearAsync(EstadoCivilViewModel model);

    Task<EstadoCivilViewModel?> ObtenerPorIdAsync(string codigo);

    Task ActualizarAsync(EstadoCivilViewModel model);

    Task<EstadoCivilViewModel?> ObtenerPorCodigoAsync(string codigo);

    Task EliminarAsync(string codigo);
}
