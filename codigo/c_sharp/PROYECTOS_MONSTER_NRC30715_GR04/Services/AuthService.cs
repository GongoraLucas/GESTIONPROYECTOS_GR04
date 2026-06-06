using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class AuthService : IAuthService
{
    private readonly ProyectoDbContext _context;

    public AuthService(
        ProyectoDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ValidarUsuarioAsync(
        string login,
        string password)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x =>
                x.Login == login);

        if (usuario == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(
                password,
                usuario.Password))
            return null;

        return usuario;
    }
}