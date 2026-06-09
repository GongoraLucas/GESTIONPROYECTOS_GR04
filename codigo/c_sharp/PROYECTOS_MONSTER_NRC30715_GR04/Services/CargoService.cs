using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class CargoService : ICargoService
{
    private readonly ProyectoDbContext _context;

    public CargoService(ProyectoDbContext context)
    {
        _context = context;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Listar con paginación y búsqueda
    // ─────────────────────────────────────────────────────────────────────
    public async Task<(List<CargoViewModel> Items, int TotalRegistros)>
        ObtenerTodosAsync(
            string? buscar,
            int pagina,
            int registrosPorPagina)
    {
        var query = _context.Cargos
            .Include(c => c.Departamento)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            query = query.Where(x =>
                x.Codigo.Contains(buscar) ||
                x.Descripcion.Contains(buscar) ||
                x.Departamento!.Descripcion.Contains(buscar));
        }

        int total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Departamento!.Descripcion)
            .ThenBy(x => x.Descripcion)
            .Skip((pagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .Select(x => new CargoViewModel
            {
                DepartamentoCodigo      = x.DepartamentoCodigo,
                Codigo                  = x.Codigo,
                Descripcion             = x.Descripcion,
                DepartamentoDescripcion = x.Departamento!.Descripcion
            })
            .ToListAsync();

        return (items, total);
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Formulario (con lista de departamentos)
    // ─────────────────────────────────────────────────────────────────────
    public async Task<CargoViewModel> ObtenerFormularioAsync()
    {
        var model = new CargoViewModel();
        model.Departamentos = await CargarDepartamentosAsync();
        return model;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Crear
    // ─────────────────────────────────────────────────────────────────────
    public async Task CrearAsync(CargoViewModel model)
    {
        var entidad = new Cargo
        {
            DepartamentoCodigo = model.DepartamentoCodigo,
            Codigo             = model.Codigo,
            Descripcion        = model.Descripcion
        };

        _context.Cargos.Add(entidad);
        await _context.SaveChangesAsync();
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Obtener por clave compuesta (para Edit)
    // ─────────────────────────────────────────────────────────────────────
    public async Task<CargoViewModel?> ObtenerPorIdAsync(
        string departamentoCodigo,
        string codigo)
    {
        var entidad = await _context.Cargos
            .Include(c => c.Departamento)
            .FirstOrDefaultAsync(x =>
                x.DepartamentoCodigo == departamentoCodigo &&
                x.Codigo == codigo);

        if (entidad == null)
            return null;

        var model = new CargoViewModel
        {
            DepartamentoCodigo         = entidad.DepartamentoCodigo,
            Codigo                     = entidad.Codigo,
            Descripcion                = entidad.Descripcion,
            CodigoOriginal             = entidad.Codigo,
            DepartamentoCodigoOriginal = entidad.DepartamentoCodigo,
            DepartamentoDescripcion    = entidad.Departamento?.Descripcion
        };

        model.Departamentos = await CargarDepartamentosAsync();
        return model;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Actualizar  (la PK no cambia en este contexto; solo Descripcion)
    // ─────────────────────────────────────────────────────────────────────
    public async Task ActualizarAsync(
        CargoViewModel model,
        string depOriginal,
        string codOriginal)
    {
        var entidad = await _context.Cargos
            .FirstOrDefaultAsync(x =>
                x.DepartamentoCodigo == depOriginal &&
                x.Codigo == codOriginal);

        if (entidad == null)
            return;

        entidad.Descripcion = model.Descripcion;
        await _context.SaveChangesAsync();
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Obtener para confirmación de Delete
    // ─────────────────────────────────────────────────────────────────────
    public async Task<CargoViewModel?> ObtenerPorCodigoAsync(
        string departamentoCodigo,
        string codigo)
    {
        return await _context.Cargos
            .Include(c => c.Departamento)
            .Where(x =>
                x.DepartamentoCodigo == departamentoCodigo &&
                x.Codigo == codigo)
            .Select(x => new CargoViewModel
            {
                DepartamentoCodigo      = x.DepartamentoCodigo,
                Codigo                  = x.Codigo,
                Descripcion             = x.Descripcion,
                DepartamentoDescripcion = x.Departamento!.Descripcion
            })
            .FirstOrDefaultAsync();
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Eliminar
    // ─────────────────────────────────────────────────────────────────────
    public async Task EliminarAsync(
        string departamentoCodigo,
        string codigo)
    {
        bool tieneEmpleados = await _context.Empleados
            .AnyAsync(x =>
                x.DepartamentoCodigo == departamentoCodigo &&
                x.CargoCodigo == codigo);

        if (tieneEmpleados)
        {
            throw new Exception(
                "No se puede eliminar el cargo porque tiene empleados asociados.");
        }

        var entidad = await _context.Cargos
            .FirstOrDefaultAsync(x =>
                x.DepartamentoCodigo == departamentoCodigo &&
                x.Codigo == codigo);

        if (entidad == null)
            return;

        _context.Cargos.Remove(entidad);
        await _context.SaveChangesAsync();
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Helper privado
    // ─────────────────────────────────────────────────────────────────────
    private async Task<List<SelectListItem>> CargarDepartamentosAsync()
    {
        return await _context.Departamentos
            .OrderBy(d => d.Descripcion)
            .Select(d => new SelectListItem
            {
                Value = d.Codigo,
                Text  = d.Descripcion
            })
            .ToListAsync();
    }
}
