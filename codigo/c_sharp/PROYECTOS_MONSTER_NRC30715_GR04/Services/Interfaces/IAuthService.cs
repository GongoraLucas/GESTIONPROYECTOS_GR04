using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IAuthService
{
    Task<Usuario?> ValidarUsuarioAsync(
        string login,
        string password);
}