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

    public async Task<bool> CambiarPasswordAsync(
        int usuarioId,
        string passwordActual,
        string passwordNueva)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Id == usuarioId);

        if (usuario == null)
            return false;

        bool passwordValido =
            BCrypt.Net.BCrypt.Verify(
                passwordActual,
                usuario.Password);

        if (!passwordValido)
            return false;

        usuario.Password =
            BCrypt.Net.BCrypt.HashPassword(
                passwordNueva);

        usuario.FechaModificacion =
            DateTime.Now;

        usuario.PrimerIngreso = false;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<string?> GenerarTokenRecuperacionAsync(
        string email)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x =>
                x.Email == email);

        if (usuario == null)
            return null;

        var token =
            Guid.NewGuid().ToString();

        var recuperacion =
            new RecuperacionPassword
            {
                Token = token,
                UsuarioId = usuario.Id,
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddHours(1),
                Utilizado = false
            };

        _context.Recuperaciones.Add(
            recuperacion);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<bool> RestablecerPasswordAsync(
        string token,
        string passwordNueva)
    {
        var recuperacion =
            await _context.Recuperaciones
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r =>
                    r.Token == token &&
                    !r.Utilizado &&
                    r.FechaExpiracion > DateTime.Now);

        if (recuperacion == null)
            return false;

        recuperacion.Usuario!.Password =
            BCrypt.Net.BCrypt.HashPassword(
                passwordNueva);

        recuperacion.Usuario.FechaModificacion =
            DateTime.Now;

        recuperacion.Usuario.PrimerIngreso =
            false;

        recuperacion.Utilizado = true;

        await _context.SaveChangesAsync();

        return true;
    }
}