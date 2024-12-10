using Caso_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caso_2.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CasoContext _context;

        public CategoriaController(CasoContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            // Devuelve todas las categorías directamente sin seleccionar propiedades específicas
            var categorias = await _context.Categorias.ToListAsync();

            return View(categorias);
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion")] Categoria categoria) // Excluye Estado
        {
            if (ModelState.IsValid)
            {
                categoria.FechaRegistro = DateTime.Now; 
                categoria.UsuarioRegistro = User.Identity?.Name ?? "Sistema"; 
                categoria.Estado = true; 

                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoriaDb = await _context.Categorias.FindAsync(id);
                    if (categoriaDb == null)
                    {
                        return NotFound();
                    }

                    // Actualizar campos
                    categoriaDb.Nombre = categoria.Nombre;
                    categoriaDb.Descripcion = categoria.Descripcion;
                    categoriaDb.Estado = categoria.Estado;

                    _context.Update(categoriaDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
