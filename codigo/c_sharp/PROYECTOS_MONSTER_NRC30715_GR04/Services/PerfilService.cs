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

    public async Task<List<Perfil>> ObtenerTodosAsync()
    {
        return await _context.Perfiles
            .OrderBy(x => x.Descripcion)
            .ToListAsync();
    }

    public async Task<Perfil?> ObtenerPorCodigoAsync(string codigo)
    {
        return await _context.Perfiles
            .FirstOrDefaultAsync(x => x.Codigo == codigo);
    }

    public async Task CrearPerfilAsync(Perfil perfil)
    {
        _context.Perfiles.Add(perfil);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarPerfilAsync(Perfil perfil)
    {
        var dbPerfil = await _context.Perfiles
            .FirstOrDefaultAsync(x => x.Codigo == perfil.Codigo);

        if (dbPerfil != null)
        {
            dbPerfil.Descripcion = perfil.Descripcion;
            dbPerfil.Observacion = perfil.Observacion;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> EliminarPerfilAsync(string codigo)
    {
        var tieneUsuarios = await _context.UsuariosPerfiles
            .AnyAsync(x => x.PerfilCodigo == codigo && x.FechaRetiro == null);

        if (tieneUsuarios)
        {
            return false;
        }

        var perfil = await _context.Perfiles
            .FirstOrDefaultAsync(x => x.Codigo == codigo);

        if (perfil != null)
        {
            var opcionesAsociadas = await _context.PerfilesOpciones
                .Where(x => x.PerfilCodigo == codigo)
                .ToListAsync();
                
            if (opcionesAsociadas.Any())
            {
                _context.PerfilesOpciones.RemoveRange(opcionesAsociadas);
            }

            _context.Perfiles.Remove(perfil);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
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

    public async Task GuardarUsuariosPerfilAsync(
        string perfilCodigo,
        List<int> usuarioIds)
    {
        var asignacionesActivas = await _context.UsuariosPerfiles
            .Where(x => x.PerfilCodigo == perfilCodigo && x.FechaRetiro == null)
            .ToListAsync();

        var activeUserIds = asignacionesActivas.Select(x => x.UsuarioId).ToList();

        // Retirar usuarios que ya no están asignados
        var usuariosARetirar = asignacionesActivas
            .Where(x => !usuarioIds.Contains(x.UsuarioId))
            .ToList();
            
        foreach (var reg in usuariosARetirar)
        {
            reg.FechaRetiro = DateTime.Now;
        }

        // Asignar nuevos usuarios
        var usuariosAAsignar = usuarioIds
            .Where(id => !activeUserIds.Contains(id))
            .ToList();

        foreach (var id in usuariosAAsignar)
        {
            var asignacion = new UsuarioPerfil
            {
                UsuarioId = id,
                PerfilCodigo = perfilCodigo,
                FechaAsignacion = DateTime.Now
            };
            _context.Add(asignacion);
        }

        await _context.SaveChangesAsync();
    }


    private static string ObtenerNombreGrupo(string codigo)
    {
        if (codigo.StartsWith("DEP_")) return "Departamentos";
        if (codigo.StartsWith("CAR_")) return "Cargos";
        if (codigo.StartsWith("EMP_")) return "Empleados";
        if (codigo.StartsWith("SEX_")) return "Sexos (Catálogo de géneros)";
        if (codigo.StartsWith("ECI_")) return "Estados Civiles (Catálogo de estados)";
        if (codigo.StartsWith("REP_")) return "Reportes Analíticos";
        if (codigo.StartsWith("USR_")) return "Usuarios";
        if (codigo.StartsWith("PER_")) return "Perfiles";
        return "General";
    }

    public async Task<AsignarOpcionViewModel>
    ObtenerOpcionesPerfilAsync(
        string perfilCodigo)
    {
        var model = new AsignarOpcionViewModel
        {
            PerfilCodigo = perfilCodigo
        };

        model.Perfiles = await _context.Perfiles
            .OrderBy(x => x.Descripcion)
            .Select(x => new SelectListItem
            {
                Value = x.Codigo,
                Text = x.Descripcion
            })
            .ToListAsync();

        if (string.IsNullOrEmpty(perfilCodigo))
            return model;

        var opcionesAsignadasIds = await _context.PerfilesOpciones
            .Where(x => x.PerfilCodigo == perfilCodigo && x.FechaRetiro == null)
            .Select(x => x.OpcionCodigo)
            .ToListAsync();

        var sistemas = await _context.Sistemas
            .Include(s => s.Opciones)
            .OrderBy(s => s.Descripcion)
            .ToListAsync();

        model.Sistemas = sistemas.Select(s => new SistemaTreeItemViewModel
        {
            Codigo = s.Codigo,
            Descripcion = s.Descripcion,
            Grupos = s.Opciones
                .GroupBy(o => ObtenerNombreGrupo(o.Codigo))
                .Select(g => new GrupoTreeItemViewModel
                {
                    Nombre = g.Key,
                    Opciones = g.OrderBy(o => o.Descripcion)
                        .Select(o => new OpcionTreeItemViewModel
                        {
                            Codigo = o.Codigo,
                            Descripcion = o.Descripcion,
                            Asignada = opcionesAsignadasIds.Contains(o.Codigo)
                        })
                        .ToList()
                })
                .OrderBy(g => g.Nombre)
                .ToList()
        })
        .ToList();

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