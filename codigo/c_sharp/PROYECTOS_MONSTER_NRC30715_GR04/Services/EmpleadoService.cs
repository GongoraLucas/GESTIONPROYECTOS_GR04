using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly ProyectoDbContext _context;

    public EmpleadoService(
        ProyectoDbContext context)
    {
        _context = context;
    }

    public async Task<
(
    List<EmpleadoViewModel> Empleados,
    int TotalRegistros
)>
ObtenerTodosAsync(
    string? buscar,
    int pagina,
    int registrosPorPagina)
    {
        var query =
            _context.Empleados.AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            query =
                query.Where(x =>
                    x.Codigo.Contains(buscar) ||
                    x.Cedula.Contains(buscar) ||
                    x.Nombres.Contains(buscar) ||
                    x.Apellidos.Contains(buscar));
        }

        int totalRegistros =
            await query.CountAsync();

        var empleados =
            await query
                .OrderBy(x => x.Apellidos)
                .ThenBy(x => x.Nombres)
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .Select(x => new EmpleadoViewModel
                {
                    Codigo = x.Codigo,
                    Cedula = x.Cedula,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    Email = x.Email,
                    Telefono = x.Telefono,
                    Foto = x.Foto
                })
                .ToListAsync();

        return (
            empleados,
            totalRegistros
        );
    }

    public async Task<EmpleadoViewModel>
        ObtenerFormularioAsync()
    {
        var model =
            new EmpleadoViewModel();

        model.Sexos =
            await _context.Sexos
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.EstadosCiviles =
            await _context.EstadosCiviles
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.Departamentos =
            await _context.Departamentos
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.Cargos =
            await _context.Cargos
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.Jefes =
            await _context.Empleados
                .OrderBy(x => x.Apellidos)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text =
                        x.Apellidos +
                        " " +
                        x.Nombres
                })
                .ToListAsync();

        return model;
    }

    public async Task CrearAsync(
    EmpleadoViewModel model)
    {
        var empleado =
            new Empleado
            {
                Codigo = model.Codigo,
                Cedula = model.Cedula,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email,

                FechaNacimiento =
                    model.FechaNacimiento,

                FechaSalida =
                    model.FechaSalida,

                Salario =
                    model.Salario,

                SexoCodigo =
                    model.SexoCodigo,

                EstadoCivilCodigo =
                    model.EstadoCivilCodigo,

                DepartamentoCodigo =
                    model.DepartamentoCodigo,

                CargoCodigo =
                    model.CargoCodigo,

                JefeCodigo =
                    string.IsNullOrWhiteSpace(
                        model.JefeCodigo)
                        ? null
                        : model.JefeCodigo,

                Foto =
                    model.Foto
            };

        _context.Empleados.Add(
            empleado);

        await _context.SaveChangesAsync();
    }

    public async Task<EmpleadoViewModel?>
    ObtenerPorIdAsync(string codigo)
    {
        var empleado =
            await _context.Empleados
                .FirstOrDefaultAsync(
                    x => x.Codigo == codigo);

        if (empleado == null)
            return null;

        var model =
            new EmpleadoViewModel
            {
                Codigo = empleado.Codigo,
                Cedula = empleado.Cedula,
                Nombres = empleado.Nombres,
                Apellidos = empleado.Apellidos,
                Direccion = empleado.Direccion,
                Telefono = empleado.Telefono,
                Email = empleado.Email,
                FechaNacimiento = empleado.FechaNacimiento,
                FechaSalida = empleado.FechaSalida,
                Salario = empleado.Salario,
                SexoCodigo = empleado.SexoCodigo,
                EstadoCivilCodigo = empleado.EstadoCivilCodigo,
                DepartamentoCodigo = empleado.DepartamentoCodigo,
                CargoCodigo = empleado.CargoCodigo,
                JefeCodigo = empleado.JefeCodigo,
                Foto = empleado.Foto
            };

        model.Sexos =
            await _context.Sexos
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.EstadosCiviles =
            await _context.EstadosCiviles
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.Departamentos =
            await _context.Departamentos
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.Cargos =
            await _context.Cargos
                .OrderBy(x => x.Descripcion)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Descripcion
                })
                .ToListAsync();

        model.Jefes =
            await _context.Empleados
                .OrderBy(x => x.Apellidos)
                .Select(x => new SelectListItem
                {
                    Value = x.Codigo,
                    Text = x.Apellidos + " " + x.Nombres
                })
                .ToListAsync();

        return model;
    }

    public async Task ActualizarAsync(
    EmpleadoViewModel model)
    {
        var empleado =
            await _context.Empleados
                .FirstOrDefaultAsync(
                    x => x.Codigo == model.Codigo);

        if (empleado == null)
            return;

        empleado.Cedula = model.Cedula;
        empleado.Nombres = model.Nombres;
        empleado.Apellidos = model.Apellidos;
        empleado.Direccion = model.Direccion;
        empleado.Telefono = model.Telefono;
        empleado.Email = model.Email;
        empleado.FechaNacimiento =
            model.FechaNacimiento;

        empleado.FechaSalida =
            model.FechaSalida;

        empleado.Salario =
            model.Salario;

        empleado.SexoCodigo =
            model.SexoCodigo;

        empleado.EstadoCivilCodigo =
            model.EstadoCivilCodigo;

        empleado.DepartamentoCodigo =
            model.DepartamentoCodigo;

        empleado.CargoCodigo =
            model.CargoCodigo;

        empleado.JefeCodigo =
            model.JefeCodigo;

        if (model.ArchivoFoto != null)
        {
            var nombreArchivo =
                Guid.NewGuid() +
                Path.GetExtension(
                    model.ArchivoFoto.FileName);

            var carpeta =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "empleados");

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(
                    carpeta);
            }

            var rutaCompleta =
                Path.Combine(
                    carpeta,
                    nombreArchivo);

            using var stream =
                new FileStream(
                    rutaCompleta,
                    FileMode.Create);

            await model.ArchivoFoto
                .CopyToAsync(stream);

            empleado.Foto =
                "/uploads/empleados/" +
                nombreArchivo;
        }
        await _context.SaveChangesAsync();
    }


    public async Task<EmpleadoViewModel?>
    ObtenerPorCodigoAsync(string codigo)
    {
        return await _context.Empleados
            .Where(x => x.Codigo == codigo)
            .Select(x => new EmpleadoViewModel
            {
                Codigo = x.Codigo,
                Cedula = x.Cedula,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Email = x.Email,
                Telefono = x.Telefono,
                Foto = x.Foto
            })
            .FirstOrDefaultAsync();
    }

    public async Task EliminarAsync(
    string codigo)
    {
        bool tieneUsuario =
            await _context.Usuarios
                .AnyAsync(x =>
                    x.EmpleadoCodigo == codigo);

        if (tieneUsuario)
        {
            throw new Exception(
                "No se puede eliminar el empleado porque tiene usuarios asociados.");
        }

        bool esJefe =
            await _context.Empleados
                .AnyAsync(x =>
                    x.JefeCodigo == codigo);

        if (esJefe)
        {
            throw new Exception(
                "No se puede eliminar el empleado porque tiene subordinados asignados.");
        }

        var empleado =
            await _context.Empleados
                .FirstOrDefaultAsync(x =>
                    x.Codigo == codigo);

        if (empleado == null)
            return;

        if (!string.IsNullOrWhiteSpace(
            empleado.Foto))
        {
            string rutaFisica =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    empleado.Foto.TrimStart('/')
                        .Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(rutaFisica))
            {
                File.Delete(rutaFisica);
            }
        }

        _context.Empleados.Remove(
            empleado);

        await _context.SaveChangesAsync();
    }


    public async Task<EmpleadoViewModel?>
ObtenerDetalleAsync(string codigo)
    {
        return await _context.Empleados
            .Include(x => x.Sexo)
            .Include(x => x.EstadoCivil)
            .Include(x => x.Cargo)
            .Include(x => x.Jefe)
            .Where(x => x.Codigo == codigo)
            .Select(x => new EmpleadoViewModel
            {
                Codigo = x.Codigo,
                Cedula = x.Cedula,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Direccion = x.Direccion,
                Telefono = x.Telefono,
                Email = x.Email,
                FechaNacimiento = x.FechaNacimiento,
                FechaSalida = x.FechaSalida,
                Salario = x.Salario,
                Foto = x.Foto,

                SexoDescripcion =
                    x.Sexo!.Descripcion,

                EstadoCivilDescripcion =
                    x.EstadoCivil != null
                        ? x.EstadoCivil.Descripcion
                        : "",

                CargoDescripcion =
                    x.Cargo!.Descripcion,

                JefeNombre =
                    x.Jefe != null
                        ? x.Jefe.Apellidos + " " +
                          x.Jefe.Nombres
                        : "Sin jefe"
            })
            .FirstOrDefaultAsync();
    }
}