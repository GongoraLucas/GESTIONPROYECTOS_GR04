using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface ISexoService
{
    Task<(List<SexoViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(string? buscar, int pagina, int registrosPorPagina);

    Task CrearAsync(SexoViewModel model);

    Task<SexoViewModel?> ObtenerPorIdAsync(string codigo);

    Task ActualizarAsync(SexoViewModel model);

    Task<SexoViewModel?> ObtenerPorCodigoAsync(string codigo);

    Task EliminarAsync(string codigo);
}
