using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KillMech.Models;

namespace KillMech.Controllers
{
    public class EnemigoesController : Controller
    {
        private readonly KillmechContext _context;

        public EnemigoesController(KillmechContext context)
        {
            _context = context;
        }

        // GET: Enemigoes
        public async Task<IActionResult> Index()
        {
            var killmechContext = _context.Enemigos.Include(e => e.Categoria);
            return View(await killmechContext.ToListAsync());
        }

        // GET: Enemigoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemigo = await _context.Enemigos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.EnemigoId == id);
            if (enemigo == null)
            {
                return NotFound();
            }

            return View(enemigo);
        }

        // GET: Enemigoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId");
            return View();
        }

        // POST: Enemigoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnemigoId,Nombre,CategoriaId,Mortalidad")] Enemigo enemigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enemigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", enemigo.CategoriaId);
            return View(enemigo);
        }

        // GET: Enemigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemigo = await _context.Enemigos.FindAsync(id);
            if (enemigo == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", enemigo.CategoriaId);
            return View(enemigo);
        }

        // POST: Enemigoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnemigoId,Nombre,CategoriaId,Mortalidad")] Enemigo enemigo)
        {
            if (id != enemigo.EnemigoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enemigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnemigoExists(enemigo.EnemigoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", enemigo.CategoriaId);
            return View(enemigo);
        }

        // GET: Enemigoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemigo = await _context.Enemigos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.EnemigoId == id);
            if (enemigo == null)
            {
                return NotFound();
            }

            return View(enemigo);
        }

        // POST: Enemigoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enemigo = await _context.Enemigos.FindAsync(id);
            if (enemigo != null)
            {
                _context.Enemigos.Remove(enemigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnemigoExists(int id)
        {
            return _context.Enemigos.Any(e => e.EnemigoId == id);
        }

        public async Task<IActionResult> VistaEnemigos()
        {
            // Obtiene los datos de la vista y los mapea a una lista del tipo deseado
            var viewModel = await _context.EnemigoCategorias
                .Select(e => new EnemigoCategoria
                {
                    NombreEnemigo = e.NombreEnemigo,
                    Categoria = e.Categoria,
                    Mortalidad = e.Mortalidad
                })
                .ToListAsync();

            // Preparar los datos para los gráficos u otra visualización
            ViewBag.NombresEnemigos = viewModel.Select(v => v.NombreEnemigo).ToList();
            ViewBag.Categorias = viewModel.Select(v => v.Categoria).ToList();
            ViewBag.Mortalidades = viewModel.Select(v => v.Mortalidad).ToList();

            // Retorna la vista 'GraficosEnemigos' con el modelo 'viewModel'
            return View("GraficosEnemigos", viewModel);
        }


    }
}
