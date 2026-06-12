using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IUsuarioService
{
    Task<(
        List<UsuarioItemViewModel> Usuarios,
        int TotalRegistros
    )> ObtenerTodosAsync(string? buscar, int pagina, int registrosPorPagina);

    Task<UsuarioViewModel> ObtenerFormularioAsync(UsuarioViewModel? model = null);

    Task CrearAsync(UsuarioViewModel model);

    Task<UsuarioViewModel?> ObtenerPorIdAsync(int id);

    Task ActualizarAsync(UsuarioViewModel model);

    Task EliminarAsync(int id);

    Task CambiarEstadoAsync(int usuarioId, string estadoCodigo);

    Task RestablecerPasswordAdminAsync(int usuarioId, string nuevaPassword);
}
