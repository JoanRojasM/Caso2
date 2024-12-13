using Caso_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caso_2.Controllers
{
    [Authorize]
    public class InscripcionController : Controller
    {
        private readonly CasoContext _context;
        private readonly UserManager<Usuario> _userManager;

        public InscripcionController(CasoContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Inscribir
        [HttpGet]
        public async Task<IActionResult> Inscribir()
        {
            // Obtener el ID del usuario autenticado
            var usuarioId = _userManager.GetUserId(User);

            // Obtener todos los eventos activos y verificar si el usuario está inscrito
            var eventos = await _context.Eventos
                .Include(e => e.Categoria)
                .Select(e => new EventoViewModel
                {
                    Evento = e,
                    YaInscrito = _context.Inscripciones.Any(i => i.EventoId == e.Id && i.UsuarioId == usuarioId)
                })
                .ToListAsync();

            return View(eventos);
        }

        // POST: Inscribir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InscribirEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null || !evento.Estado)
            {
                return NotFound("El evento no está disponible.");
            }

            // Verificar que hay cupo disponible
            if (evento.CupoMaximo <= 0)
            {
                return BadRequest("El evento ya no tiene cupo disponible.");
            }

            // Obtener el ID del usuario autenticado
            var usuarioId = _userManager.GetUserId(User);

            // Calcular las fechas de inicio y fin del nuevo evento
            var fechaInicioNuevo = evento.Fecha.Add(evento.Hora);
            var fechaFinNuevo = fechaInicioNuevo.AddHours(evento.Duracion);

            // Traer todas las inscripciones del usuario a memoria
            var inscripcionesUsuario = await _context.Inscripciones
                .Include(i => i.Evento)
                .Where(i => i.UsuarioId == usuarioId)
                .ToListAsync();

            // Verificar conflictos de fechas en memoria
            var conflictos = inscripcionesUsuario
                .Where(i =>
                {
                    var eventoExistente = i.Evento;
                    var fechaInicioExistente = eventoExistente.Fecha.Add(eventoExistente.Hora);
                    var fechaFinExistente = fechaInicioExistente.AddHours(eventoExistente.Duracion);

                    return fechaInicioNuevo < fechaFinExistente && fechaInicioExistente < fechaFinNuevo;
                })
                .ToList();

            if (conflictos.Any())
            {
                return BadRequest("Ya estás inscrito en otro evento que se superpone en fecha y hora.");
            }

            // Registrar la inscripción
            var inscripcion = new Inscripcion
            {
                EventoId = id,
                UsuarioId = usuarioId
            };

            _context.Inscripciones.Add(inscripcion);

            // Reducir el cupo del evento
            evento.CupoMaximo--;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Inscribir));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuitarInscripcion(int id)
        {
            // Obtener el ID del usuario autenticado
            var usuarioId = _userManager.GetUserId(User);

            // Buscar la inscripción correspondiente
            var inscripcion = await _context.Inscripciones
                .FirstOrDefaultAsync(i => i.EventoId == id && i.UsuarioId == usuarioId);

            if (inscripcion == null)
            {
                return NotFound("No estás inscrito en este evento.");
            }

            // Buscar el evento correspondiente
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound("El evento no existe.");
            }

            // Eliminar la inscripción y aumentar el cupo disponible del evento
            _context.Inscripciones.Remove(inscripcion);
            evento.CupoMaximo++;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Inscribir));
        }

        [Authorize(Roles = "Administrador")]
        // GET: Inscritos
        public async Task<IActionResult> Inscritos(int id)
        {
            var evento = await _context.Eventos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
            {
                return NotFound("El evento no existe.");
            }

            var inscritos = await _context.Inscripciones
                .Where(i => i.EventoId == id)
                .Include(i => i.Usuario)
                .ToListAsync();

            ViewBag.CupoRestante = evento.CupoMaximo;
            ViewBag.EventoTitulo = evento.Titulo;

            return View(inscritos);
        }
    }
}