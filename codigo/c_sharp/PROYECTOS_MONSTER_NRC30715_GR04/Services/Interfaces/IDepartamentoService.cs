using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IDepartamentoService
{
    Task<(List<DepartamentoViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(string? buscar, int pagina, int registrosPorPagina);

    Task CrearAsync(DepartamentoViewModel model);

    Task<DepartamentoViewModel?> ObtenerPorIdAsync(string codigo);

    Task ActualizarAsync(DepartamentoViewModel model);

    Task<DepartamentoViewModel?> ObtenerPorCodigoAsync(string codigo);

    Task EliminarAsync(string codigo);
}
