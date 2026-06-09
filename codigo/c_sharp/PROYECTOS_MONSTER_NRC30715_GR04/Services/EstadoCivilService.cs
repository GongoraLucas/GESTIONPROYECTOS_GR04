using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class EstadoCivilService : IEstadoCivilService
{
    private readonly ProyectoDbContext _context;

    public EstadoCivilService(ProyectoDbContext context)
    {
        _context = context;
    }

    public async Task<(List<EstadoCivilViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(
            string? buscar,
            int pagina,
            int registrosPorPagina)
    {
        var query = _context.EstadosCiviles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            query = query.Where(x =>
                x.Codigo.Contains(buscar) ||
                x.Descripcion.Contains(buscar));
        }

        int total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Descripcion)
            .Skip((pagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .Select(x => new EstadoCivilViewModel
            {
                Codigo      = x.Codigo,
                Descripcion = x.Descripcion
            })
            .ToListAsync();

        return (items, total);
    }

    public async Task CrearAsync(EstadoCivilViewModel model)
    {
        var entidad = new EstadoCivil
        {
            Codigo      = model.Codigo,
            Descripcion = model.Descripcion
        };

        _context.EstadosCiviles.Add(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task<EstadoCivilViewModel?> ObtenerPorIdAsync(string codigo)
    {
        var entidad = await _context.EstadosCiviles
            .FirstOrDefaultAsync(x => x.Codigo == codigo);

        if (entidad == null)
            return null;

        return new EstadoCivilViewModel
        {
            Codigo      = entidad.Codigo,
            Descripcion = entidad.Descripcion
        };
    }

    public async Task ActualizarAsync(EstadoCivilViewModel model)
    {
        var entidad = await _context.EstadosCiviles
            .FirstOrDefaultAsync(x => x.Codigo == model.Codigo);

        if (entidad == null)
            return;

        entidad.Descripcion = model.Descripcion;
        await _context.SaveChangesAsync();
    }

    public async Task<EstadoCivilViewModel?> ObtenerPorCodigoAsync(string codigo)
    {
        return await _context.EstadosCiviles
            .Where(x => x.Codigo == codigo)
            .Select(x => new EstadoCivilViewModel
            {
                Codigo      = x.Codigo,
                Descripcion = x.Descripcion
            })
            .FirstOrDefaultAsync();
    }

    public async Task EliminarAsync(string codigo)
    {
        bool tieneEmpleados = await _context.Empleados
            .AnyAsync(x => x.EstadoCivilCodigo == codigo);

        if (tieneEmpleados)
        {
            throw new Exception(
                "No se puede eliminar el estado civil porque tiene empleados asociados.");
        }

        var entidad = await _context.EstadosCiviles
            .FirstOrDefaultAsync(x => x.Codigo == codigo);

        if (entidad == null)
            return;

        _context.EstadosCiviles.Remove(entidad);
        await _context.SaveChangesAsync();
    }
}
