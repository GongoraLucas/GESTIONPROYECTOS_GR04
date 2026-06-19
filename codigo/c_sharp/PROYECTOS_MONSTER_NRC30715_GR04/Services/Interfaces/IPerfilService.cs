using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IPerfilService
{
    // CRUD Perfil
    Task<List<Perfil>> ObtenerTodosAsync();
    Task<Perfil?> ObtenerPorCodigoAsync(string codigo);
    Task CrearPerfilAsync(Perfil perfil);
    Task ActualizarPerfilAsync(Perfil perfil);
    Task<bool> EliminarPerfilAsync(string codigo);

    Task<AsignarPerfilViewModel>
        ObtenerUsuariosPerfilAsync(
            string perfilCodigo);

    Task AsignarUsuarioAsync(
        int usuarioId,
        string perfilCodigo);

    Task RetirarUsuarioAsync(
        int usuarioId,
        string perfilCodigo);

    Task GuardarUsuariosPerfilAsync(
        string perfilCodigo,
        List<int> usuarioIds);

    Task<AsignarOpcionViewModel>
        ObtenerOpcionesPerfilAsync(
            string perfilCodigo);

    Task AsignarOpcionAsync(
        string perfilCodigo,
        string opcionCodigo);

    Task RetirarOpcionAsync(
        string perfilCodigo,
        string opcionCodigo);


    Task<List<string>>
    ObtenerPerfilesUsuarioAsync(
        int usuarioId);

    Task<List<string>>
    ObtenerOpcionesUsuarioAsync(
        int usuarioId);
}