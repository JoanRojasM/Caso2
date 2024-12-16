using Caso_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

[Authorize(Roles = "Administrador,Organizador")]
public class EventosController : Controller
{
    private readonly CasoContext _context;

    public EventosController(CasoContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index(int? categoriaId, bool? activo)
    {
        // Obtener las categorías para el filtro
        var categorias = await _context.Categorias.ToListAsync();
        ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", categoriaId);

        // Crear una lista para el filtro de activos
        var activos = new List<SelectListItem>
    {
        
        new SelectListItem { Text = "Activos", Value = "true", Selected = activo == true },
        new SelectListItem { Text = "Inactivos", Value = "false", Selected = activo == false }
    };
        ViewBag.Activos = activos;

        // Crear la consulta inicial
        var eventosQuery = _context.Eventos.Include(e => e.Categoria).AsQueryable();



        // Filtrar por estado si se especifica
        if (!activo.HasValue)
        {
            activo = true; // Mostrar activos por defecto
        }

        eventosQuery = eventosQuery.Where(e => e.Estado == activo.Value);

        // Ejecutar la consulta
        var eventos = await eventosQuery.ToListAsync();
        return View(eventos);
    }


    // GET Details 
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

    // GET Create
    public IActionResult Create()
    {

        ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(c => c.Estado), "Id", "Nombre");
        return View();
    }


 // Este view model se usa como intermediario para asignar el usuario registro y la fecha de registro 
    public class EventoCreateViewModel
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int Duracion { get; set; }
        public string Ubicacion { get; set; }
        public int CupoMaximo { get; set; }
        
    }
    // POST Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventoCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var evento = new Evento
            {
                Titulo = model.Titulo,
                Descripcion = model.Descripcion,
                CategoriaId = model.CategoriaId,
                Fecha = model.Fecha,
                Hora = model.Hora,
                Duracion = model.Duracion,
                Ubicacion = model.Ubicacion,
                CupoMaximo = model.CupoMaximo,
                UsuarioRegistro = User.Identity?.Name,
                Estado = true
            };

            _context.Add(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", model.CategoriaId);
        return View(model);
    }


    // GET Editar
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var evento = await _context.Eventos.FindAsync(id);
        if (evento == null)
        {
            return NotFound();
        }

        ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(c => c.Estado), "Id", "Nombre", evento.CategoriaId);
        return View(evento);
    }


    // POST Editar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,CategoriaId,Fecha,Hora,Duracion,Ubicacion,CupoMaximo,Estado,UsuarioRegistro")] Evento evento)
    {
        if (id != evento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Obtener el evento original desde la base de datos
                var originalEvento = await _context.Eventos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                if (originalEvento == null)
                {
                    return NotFound();
                }

                // Asignar el UsuarioRegistro del evento original
                evento.UsuarioRegistro = originalEvento.UsuarioRegistro;

                // Actualizar el evento
                _context.Update(evento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(evento.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(c => c.Estado), "Id", "Nombre", evento.CategoriaId);
        return View(evento);
    }

    // GET Inactivar
public async Task<IActionResult> Inactivar(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var evento = await _context.Eventos.FindAsync(id);
    if (evento == null)
    {
        return NotFound();
    }

    return View(evento);
}

// POST Inactivar
[HttpPost, ActionName("Inactivar")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> InactivarConfirmed(int id)
{
    var evento = await _context.Eventos.FindAsync(id);
    if (evento == null)
    {
        return NotFound();
    }

    evento.Estado = false;

    try
    {
        _context.Update(evento);
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!EventoExists(evento.Id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return RedirectToAction(nameof(Index));
}
    
    private bool EventoExists(int id)
    {
        return _context.Eventos.Any(e => e.Id == id);
    }

    public async Task<IActionResult> EventosConInscritos()
    {
        // Obtener todos los eventos con la información básica
        var eventos = await _context.Eventos
            .Include(e => e.Categoria)
            .ToListAsync();

        return View(eventos);
    }
}
