using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Caso_2.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Caso_2.Controllers
{
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

            var fechaInicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var fechaFinMes = fechaInicioMes.AddMonths(1).AddDays(-1);
            var totalInscritosMes = await _context.Inscripciones
                .Where(i => i.FechaInscripcion >= fechaInicioMes && i.FechaInscripcion <= fechaFinMes)
                .CountAsync();

            var totalCategorias = await _context.Categorias.CountAsync(e => e.Estado == true);

           
            var topEventos = await _context.Inscripciones
                .GroupBy(i => i.Evento)
                .Select(g => new
                {
                    Evento = g.Key.Titulo, 
                    TotalInscripciones = g.Count() 
                })
                .OrderByDescending(e => e.TotalInscripciones)
                .Take(5)
                .ToListAsync();

           
            var topEventosList = topEventos
                .Select((e, index) => $"{index + 1}. {e.Evento} ({e.TotalInscripciones} inscripciones)")
                .ToList();

            var dashboardViewModel = new DashboardViewModel
            {
                TotalUsuarios = totalUsuarios,
                TotalEventos = totalEventos,
                TotalInscritosMes = totalInscritosMes,
                TotalCategorias = totalCategorias,
                MesAnioActual = DateTime.Now.ToString("MMMM yyyy"),
                TopEventos = topEventosList
            };

            return View(dashboardViewModel);
        }
    }
}
