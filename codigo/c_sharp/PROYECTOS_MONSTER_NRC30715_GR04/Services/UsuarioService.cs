using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ProyectoDbContext _context;

    public UsuarioService(ProyectoDbContext context)
    {
        _context = context;
    }

    public async Task<(
        List<UsuarioItemViewModel> Usuarios,
        int TotalRegistros
    )> ObtenerTodosAsync(string? buscar, int pagina, int registrosPorPagina)
    {
        var query = _context.Usuarios.AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            query = query.Where(x => x.Login.Contains(buscar) || x.Email.Contains(buscar));
        }

        int total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Login)
            .Skip((pagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .Select(x => new UsuarioItemViewModel { Id = x.Id, Login = x.Login })
            .ToListAsync();

        return (items, total);
    }

    public async Task<UsuarioViewModel> ObtenerFormularioAsync(UsuarioViewModel? model = null)
    {
        model ??= new UsuarioViewModel();

        model.Empleados = await _context.Empleados
            .OrderBy(e => e.Apellidos)
            .Select(e => new SelectListItem { Value = e.Codigo, Text = e.Apellidos + " " + e.Nombres })
            .ToListAsync();

        model.Estados = await _context.Estados
            .OrderBy(s => s.Descripcion)
            .Select(s => new SelectListItem { Value = s.Codigo, Text = s.Descripcion })
            .ToListAsync();

        return model;
    }

    public async Task CrearAsync(UsuarioViewModel model)
    {
        var usuario = new Usuario
        {
            Login = model.Login,
            Email = model.Email,
            EmpleadoCodigo = model.EmpleadoCodigo,
            EstadoCodigo = model.EstadoCodigo,
            Password = BCrypt.Net.BCrypt.HashPassword(string.IsNullOrWhiteSpace(model.Password) ? "123456" : model.Password),
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now,
            PieFirma = string.Empty,
            PrimerIngreso = true
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<UsuarioViewModel?> ObtenerPorIdAsync(int id)
    {
        var u = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        if (u == null) return null;

        var model = new UsuarioViewModel
        {
            Id = u.Id,
            Login = u.Login,
            Email = u.Email,
            EmpleadoCodigo = u.EmpleadoCodigo,
            EstadoCodigo = u.EstadoCodigo
        };

        model.Empleados = await _context.Empleados
            .OrderBy(e => e.Apellidos)
            .Select(e => new SelectListItem { Value = e.Codigo, Text = e.Apellidos + " " + e.Nombres })
            .ToListAsync();

        model.Estados = await _context.Estados
            .OrderBy(s => s.Descripcion)
            .Select(s => new SelectListItem { Value = s.Codigo, Text = s.Descripcion })
            .ToListAsync();

        return model;
    }

    public async Task ActualizarAsync(UsuarioViewModel model)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (usuario == null) return;

        usuario.Login = model.Login;
        usuario.Email = model.Email;
        usuario.EmpleadoCodigo = model.EmpleadoCodigo;
        usuario.EstadoCodigo = model.EstadoCodigo;
        usuario.FechaModificacion = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        if (usuario == null) return;

        // Remover perfiles asociados
        var ups = await _context.UsuariosPerfiles.Where(x => x.UsuarioId == id && x.FechaRetiro == null).ToListAsync();
        foreach (var up in ups)
        {
            up.FechaRetiro = DateTime.Now;
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task CambiarEstadoAsync(int usuarioId, string estadoCodigo)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
        if (usuario == null) return;

        usuario.EstadoCodigo = estadoCodigo;
        usuario.FechaModificacion = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task RestablecerPasswordAdminAsync(int usuarioId, string nuevaPassword)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
        if (usuario == null) return;

        usuario.Password = BCrypt.Net.BCrypt.HashPassword(nuevaPassword);
        usuario.PrimerIngreso = true;
        usuario.FechaModificacion = DateTime.Now;

        await _context.SaveChangesAsync();
    }
}
