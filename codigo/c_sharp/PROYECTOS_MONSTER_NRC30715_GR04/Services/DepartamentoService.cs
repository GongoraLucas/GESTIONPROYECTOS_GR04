using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class DepartamentoService : IDepartamentoService
{
    private readonly ProyectoDbContext _context;

    public DepartamentoService(ProyectoDbContext context)
    {
        _context = context;
    }

    public async Task<(List<DepartamentoViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(
            string? buscar,
            int pagina,
            int registrosPorPagina)
    {
        var query = _context.Departamentos.AsQueryable();

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
            .Select(x => new DepartamentoViewModel
            {
                Codigo      = x.Codigo,
                Descripcion = x.Descripcion
            })
            .ToListAsync();

        return (items, total);
    }

    public async Task CrearAsync(DepartamentoViewModel model)
    {
        var entidad = new Departamento
        {
            Codigo      = model.Codigo,
            Descripcion = model.Descripcion
        };

        _context.Departamentos.Add(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task<DepartamentoViewModel?> ObtenerPorIdAsync(string codigo)
    {
        var entidad = await _context.Departamentos
            .FirstOrDefaultAsync(x => x.Codigo == codigo);

        if (entidad == null)
            return null;

        return new DepartamentoViewModel
        {
            Codigo      = entidad.Codigo,
            Descripcion = entidad.Descripcion
        };
    }

    public async Task ActualizarAsync(DepartamentoViewModel model)
    {
        var entidad = await _context.Departamentos
            .FirstOrDefaultAsync(x => x.Codigo == model.Codigo);

        if (entidad == null)
            return;

        entidad.Descripcion = model.Descripcion;
        await _context.SaveChangesAsync();
    }

    public async Task<DepartamentoViewModel?> ObtenerPorCodigoAsync(string codigo)
    {
        return await _context.Departamentos
            .Where(x => x.Codigo == codigo)
            .Select(x => new DepartamentoViewModel
            {
                Codigo      = x.Codigo,
                Descripcion = x.Descripcion
            })
            .FirstOrDefaultAsync();
    }

    public async Task EliminarAsync(string codigo)
    {
        bool tieneCargos = await _context.Cargos
            .AnyAsync(x => x.DepartamentoCodigo == codigo);

        if (tieneCargos)
        {
            throw new Exception(
                "No se puede eliminar el departamento porque tiene cargos asociados.");
        }

        var entidad = await _context.Departamentos
            .FirstOrDefaultAsync(x => x.Codigo == codigo);

        if (entidad == null)
            return;

        _context.Departamentos.Remove(entidad);
        await _context.SaveChangesAsync();
    }
}
