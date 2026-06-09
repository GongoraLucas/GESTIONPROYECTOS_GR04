using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IPerfilService
{
    Task<AsignarPerfilViewModel>
        ObtenerUsuariosPerfilAsync(
            string perfilCodigo);

    Task AsignarUsuarioAsync(
        int usuarioId,
        string perfilCodigo);

    Task RetirarUsuarioAsync(
        int usuarioId,
        string perfilCodigo);

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