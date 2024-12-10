using Caso_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class EventosController : Controller
{
    private readonly CasoContext _context;

    public EventosController(CasoContext context)
    {
        _context = context;
    }

    // GET: Eventos Index + Categorias 
    public async Task<IActionResult> Index()
    {
        var eventos = await _context.Eventos
                                    .Include(e => e.Categoria)
                                    .ToListAsync();
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


    // POST Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Titulo,Descripcion,CategoriaId,Fecha,Hora,Duracion,Ubicacion,CupoMaximo")] Evento evento)
    {
        if (ModelState.IsValid)
        {
            evento.FechaRegistro = DateTime.Now; // Configurar fecha de registro
            _context.Add(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
        return View(evento);
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
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,CategoriaId,Fecha,Hora,Duracion,Ubicacion,CupoMaximo")] Evento evento)
    {
        if (id != evento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
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

        ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(c => c.Estado), "Id", "Nombre", evento.CategoriaId);
        return View(evento);
    }

    // GET Delete
    public async Task<IActionResult> Delete(int? id)
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

    // POST Delete 
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento != null)
        {
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool EventoExists(int id)
    {
        return _context.Eventos.Any(e => e.Id == id);
    }
}
