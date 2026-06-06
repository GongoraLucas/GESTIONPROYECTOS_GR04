using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IAuthService
{
    Task<Usuario?> ValidarUsuarioAsync(
        string login,
        string password);

    Task<bool> CambiarPasswordAsync(
    int usuarioId,
    string passwordActual,
    string passwordNueva);

    Task<string?> GenerarTokenRecuperacionAsync(
    string email);

    Task<bool> RestablecerPasswordAsync(
        string token,
        string passwordNueva);
}