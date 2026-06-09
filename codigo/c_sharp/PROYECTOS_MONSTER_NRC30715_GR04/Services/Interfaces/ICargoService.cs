using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface ICargoService
{
    Task<(List<CargoViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(string? buscar, int pagina, int registrosPorPagina);

    Task<CargoViewModel> ObtenerFormularioAsync();

    Task CrearAsync(CargoViewModel model);

    Task<CargoViewModel?> ObtenerPorIdAsync(
        string departamentoCodigo,
        string codigo);

    Task ActualizarAsync(
        CargoViewModel model,
        string depOriginal,
        string codOriginal);

    Task<CargoViewModel?> ObtenerPorCodigoAsync(
        string departamentoCodigo,
        string codigo);

    Task EliminarAsync(
        string departamentoCodigo,
        string codigo);
}
