using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class PerfilService : IPerfilService
{
    private readonly ProyectoDbContext _context;

    public PerfilService(
        ProyectoDbContext context)
    {
        _context = context;
    }

    public async Task<AsignarPerfilViewModel>
     ObtenerUsuariosPerfilAsync(
         string perfilCodigo)
    {
        var model =
            new AsignarPerfilViewModel();

        model.PerfilCodigo = perfilCodigo;

        model.Perfiles =
            await _context.Perfiles
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        if (string.IsNullOrEmpty(perfilCodigo))
            return model;

        var usuariosAsignadosIds =
            await _context.Set<UsuarioPerfil>()
                .Where(x =>
                    x.PerfilCodigo == perfilCodigo &&
                    x.FechaRetiro == null)
                .Select(x => x.UsuarioId)
                .ToListAsync();

        model.UsuariosAsignados =
            await _context.Usuarios
                .Where(x =>
                    usuariosAsignadosIds.Contains(x.Id))
                .OrderBy(x => x.Login)
                .Select(x => new UsuarioItemViewModel
                {
                    Id = x.Id,
                    Login = x.Login
                })
                .ToListAsync();

        model.UsuariosDisponibles =
            await _context.Usuarios
                .Where(x =>
                    !usuariosAsignadosIds.Contains(x.Id))
                .OrderBy(x => x.Login)
                .Select(x => new UsuarioItemViewModel
                {
                    Id = x.Id,
                    Login = x.Login
                })
                .ToListAsync();

        return model;
    }

    public async Task AsignarUsuarioAsync(
    int usuarioId,
    string perfilCodigo)
    {
        var existe =
            await _context.Set<UsuarioPerfil>()
                .AnyAsync(x =>
                    x.UsuarioId == usuarioId &&
                    x.PerfilCodigo == perfilCodigo &&
                    x.FechaRetiro == null);

        if (existe)
            return;

        var asignacion =
            new UsuarioPerfil
            {
                UsuarioId = usuarioId,
                PerfilCodigo = perfilCodigo,
                FechaAsignacion = DateTime.Now
            };

        _context.Add(asignacion);

        await _context.SaveChangesAsync();
    }

    public async Task RetirarUsuarioAsync(
    int usuarioId,
    string perfilCodigo)
    {
        var registro =
            await _context.Set<UsuarioPerfil>()
                .FirstOrDefaultAsync(x =>
                    x.UsuarioId == usuarioId &&
                    x.PerfilCodigo == perfilCodigo &&
                    x.FechaRetiro == null);

        if (registro == null)
            return;

        registro.FechaRetiro =
            DateTime.Now;

        await _context.SaveChangesAsync();
    }


    public async Task<AsignarOpcionViewModel>
    ObtenerOpcionesPerfilAsync(
        string perfilCodigo)
    {
        var model =
            new AsignarOpcionViewModel();

        model.PerfilCodigo =
            perfilCodigo;

        model.Perfiles =
            await _context.Perfiles
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        if (string.IsNullOrEmpty(perfilCodigo))
            return model;

        var opcionesAsignadasIds =
            await _context.PerfilesOpciones
                .Where(x =>
                    x.PerfilCodigo == perfilCodigo &&
                    x.FechaRetiro == null)
                .Select(x => x.OpcionCodigo)
                .ToListAsync();

        model.OpcionesAsignadas =
            await _context.Opciones
                .Where(x =>
                    opcionesAsignadasIds.Contains(
                        x.Codigo))
                .OrderBy(x => x.Descripcion)
                .Select(x => new OpcionItemViewModel
                {
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                })
                .ToListAsync();

        model.OpcionesDisponibles =
            await _context.Opciones
                .Where(x =>
                    !opcionesAsignadasIds.Contains(
                        x.Codigo))
                .OrderBy(x => x.Descripcion)
                .Select(x => new OpcionItemViewModel
                {
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                })
                .ToListAsync();

        return model;
    }

    public async Task AsignarOpcionAsync(
    string perfilCodigo,
    string opcionCodigo)
    {
        var existe =
            await _context.PerfilesOpciones
                .AnyAsync(x =>
                    x.PerfilCodigo == perfilCodigo &&
                    x.OpcionCodigo == opcionCodigo &&
                    x.FechaRetiro == null);

        if (existe)
            return;

        _context.PerfilesOpciones.Add(
            new PerfilOpcion
            {
                PerfilCodigo = perfilCodigo,
                OpcionCodigo = opcionCodigo,
                FechaAsignacion = DateTime.Now
            });

        await _context.SaveChangesAsync();
    }

    public async Task RetirarOpcionAsync(
    string perfilCodigo,
    string opcionCodigo)
    {
        var registro =
            await _context.PerfilesOpciones
                .FirstOrDefaultAsync(x =>
                    x.PerfilCodigo == perfilCodigo &&
                    x.OpcionCodigo == opcionCodigo &&
                    x.FechaRetiro == null);

        if (registro == null)
            return;

        registro.FechaRetiro =
            DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task<List<string>>
    ObtenerPerfilesUsuarioAsync(
        int usuarioId)
    {
        return await _context.UsuariosPerfiles
            .Where(x =>
                x.UsuarioId == usuarioId &&
                x.FechaRetiro == null)
            .Select(x => x.PerfilCodigo)
            .ToListAsync();
    }


    public async Task<List<string>>
    ObtenerOpcionesUsuarioAsync(
        int usuarioId)
    {
        return await _context.UsuariosPerfiles
            .Where(up =>
                up.UsuarioId == usuarioId &&
                up.FechaRetiro == null)
            .Join(
                _context.PerfilesOpciones
                    .Where(po =>
                        po.FechaRetiro == null),
                up => up.PerfilCodigo,
                po => po.PerfilCodigo,
                (up, po) => po.OpcionCodigo)
            .Distinct()
            .ToListAsync();
    }


}