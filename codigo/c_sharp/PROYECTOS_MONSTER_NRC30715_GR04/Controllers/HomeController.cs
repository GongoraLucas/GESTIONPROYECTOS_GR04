using Microsoft.AspNetCore.Mvc;
using PROYECTOS_MONSTER_NRC30715_GR04.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly PROYECTOS_MONSTER_NRC30715_GR04.Data.ProyectoDbContext _context;

        public HomeController(PROYECTOS_MONSTER_NRC30715_GR04.Data.ProyectoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels.DashboardViewModel
            {
                TotalEmpleados = await _context.Empleados.CountAsync(),
                TotalUsuarios = await _context.Usuarios.CountAsync()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
