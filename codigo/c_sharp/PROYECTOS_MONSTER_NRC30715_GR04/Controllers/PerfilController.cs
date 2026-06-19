using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers;

[Authorize(Policy = "USR")]
public class PerfilController : Controller
{
    private readonly IPerfilService _service;

    public PerfilController(IPerfilService service)
    {
        _service = service;
    }

    // GET: Perfil
    public async Task<IActionResult> Index()
    {
        var perfiles = await _service.ObtenerTodosAsync();
        return View(perfiles);
    }

    // GET: Perfil/Create
    public IActionResult Create()
    {
        return View(new Perfil());
    }

    // POST: Perfil/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Perfil perfil)
    {
        if (string.IsNullOrWhiteSpace(perfil.Codigo))
        {
            ModelState.AddModelError("Codigo", "El código del perfil es obligatorio.");
        }
        else
        {
            var existente = await _service.ObtenerPorCodigoAsync(perfil.Codigo.ToUpper().Trim());
            if (existente != null)
            {
                ModelState.AddModelError("Codigo", "Ya existe un perfil con este código.");
            }
        }

        if (ModelState.IsValid)
        {
            perfil.Codigo = perfil.Codigo.ToUpper().Trim();
            perfil.Descripcion = perfil.Descripcion.Trim();
            perfil.Observacion = perfil.Observacion?.Trim();

            await _service.CrearPerfilAsync(perfil);
            TempData["SuccessMessage"] = "Perfil creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(perfil);
    }

    // GET: Perfil/Edit/ADMIN
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var perfil = await _service.ObtenerPorCodigoAsync(id);
        if (perfil == null)
        {
            return NotFound();
        }

        return View(perfil);
    }

    // POST: Perfil/Edit/ADMIN
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Perfil perfil)
    {
        if (id != perfil.Codigo)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            perfil.Descripcion = perfil.Descripcion.Trim();
            perfil.Observacion = perfil.Observacion?.Trim();

            await _service.ActualizarPerfilAsync(perfil);
            TempData["SuccessMessage"] = "Perfil actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(perfil);
    }

    // POST: Perfil/Delete/ADMIN
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var eliminado = await _service.EliminarPerfilAsync(id);
        if (eliminado)
        {
            TempData["SuccessMessage"] = "Perfil eliminado exitosamente.";
        }
        else
        {
            TempData["ErrorMessage"] = "No se puede eliminar el perfil porque tiene usuarios activos asignados.";
        }

        return RedirectToAction(nameof(Index));
    }
}
