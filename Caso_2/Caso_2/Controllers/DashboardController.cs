using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Caso_2.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Caso_2.Controllers
{
    [Authorize(Roles = "Administrador,Organizador")]
    public class DashboardController : Controller
    {
        private readonly CasoContext _context;

        public DashboardController(CasoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalUsuarios = await _context.Users.CountAsync();
            var totalEventos = await _context.Eventos.CountAsync(e => e.Estado == true);

            var dashboardViewModel = new DashboardViewModel
            {
                TotalUsuarios = totalUsuarios,
                TotalEventos = totalEventos
            };

            return View(dashboardViewModel);
        }
    }
}
