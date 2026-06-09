using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize(Policy = "EMP")]
public class EmpleadoController : Controller
{
    private readonly IEmpleadoService _service;
    private readonly IWebHostEnvironment _environment;

    public EmpleadoController(
        IEmpleadoService empleadoService,
        IWebHostEnvironment environment)
    {
        _service = empleadoService;
        _environment = environment;
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model =
            await _service
                .ObtenerFormularioAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    EmpleadoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(
                await _service
                    .ObtenerFormularioAsync());
        }

        if (model.ArchivoFoto != null)
        {
            model.Foto =
                await GuardarFotoAsync(
                    model.ArchivoFoto);
        }

        await _service.CrearAsync(
            model);

        return RedirectToAction(
            nameof(Index));
    }

    private async Task<string?> GuardarFotoAsync(
    IFormFile? archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return null;

        var nombreArchivo =
            $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

        var carpeta =
            Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "empleados");

        if (!Directory.Exists(carpeta))
        {
            Directory.CreateDirectory(carpeta);
        }

        var rutaCompleta =
            Path.Combine(
                carpeta,
                nombreArchivo);

        using var stream =
            new FileStream(
                rutaCompleta,
                FileMode.Create);

        await archivo.CopyToAsync(stream);

        return $"/uploads/empleados/{nombreArchivo}";
    }


    [HttpGet]
    public async Task<IActionResult> Edit(
    string id)
    {
        var model =
            await _service
                .ObtenerPorIdAsync(id);

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
    EmpleadoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var vm =
                await _service
                    .ObtenerPorIdAsync(
                        model.Codigo);

            return View(vm);
        }

        await _service.ActualizarAsync(
            model);

        return RedirectToAction(
            nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult>
    Delete(string id)
    {
        var model =
            await _service
                .ObtenerPorCodigoAsync(id);

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>
    DeleteConfirmado(string codigo)
    {
        try
        {
            await _service
                .EliminarAsync(codigo);

            TempData["Success"] =
                "Empleado eliminado correctamente";
        }
        catch (Exception ex)
        {
            TempData["Error"] =
                ex.Message;
        }

        return RedirectToAction(
            nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult>
Details(string id)
    {
        var empleado =
            await _service
                .ObtenerDetalleAsync(id);

        if (empleado == null)
            return NotFound();

        return View(empleado);
    }




    [HttpGet]
    public async Task<IActionResult>
Index(
    string? buscar,
    int pagina = 1)
    {
        const int registrosPorPagina = 10;

        var resultado =
            await _service
                .ObtenerTodosAsync(
                    buscar,
                    pagina,
                    registrosPorPagina);

        ViewBag.Buscar =
            buscar;

        ViewBag.PaginaActual =
            pagina;

        ViewBag.RegistrosPorPagina =
            registrosPorPagina;

        ViewBag.TotalRegistros =
            resultado.TotalRegistros;

        ViewBag.TotalPaginas =
            (int)Math.Ceiling(
                (double)resultado.TotalRegistros /
                registrosPorPagina);

        return View(
            resultado.Empleados);
    }
}