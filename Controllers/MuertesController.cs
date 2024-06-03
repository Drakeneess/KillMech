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
    public class MuertesController : Controller
    {
        private readonly KillmechContext _context;

        public MuertesController(KillmechContext context)
        {
            _context = context;
        }

        // GET: Muertes
        public async Task<IActionResult> Index()
        {
            var killmechContext = _context.Muertes.Include(m => m.DesempenoNivel).Include(m => m.Enemigo);
            return View(await killmechContext.ToListAsync());
        }

        // GET: Muertes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Muertes == null)
            {
                return NotFound();
            }

            var muerte = await _context.Muertes
                .Include(m => m.DesempenoNivel)
                .Include(m => m.Enemigo)
                .FirstOrDefaultAsync(m => m.MuerteId == id);
            if (muerte == null)
            {
                return NotFound();
            }

            return View(muerte);
        }

        // GET: Muertes/Create
        public IActionResult Create()
        {
            ViewData["DesempenoNivelId"] = new SelectList(_context.DesempenoCapitulos, "DesempenoCapituloId", "DesempenoCapituloId");
            ViewData["EnemigoId"] = new SelectList(_context.Enemigos, "EnemigoId", "EnemigoId");
            return View();
        }

        // POST: Muertes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MuerteId,EnemigoId,DesempenoNivelId,TiempoMuerte,X,Y")] Muerte muerte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muerte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesempenoNivelId"] = new SelectList(_context.DesempenoCapitulos, "DesempenoCapituloId", "DesempenoCapituloId", muerte.DesempenoNivelId);
            ViewData["EnemigoId"] = new SelectList(_context.Enemigos, "EnemigoId", "EnemigoId", muerte.EnemigoId);
            return View(muerte);
        }

        // GET: Muertes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Muertes == null)
            {
                return NotFound();
            }

            var muerte = await _context.Muertes.FindAsync(id);
            if (muerte == null)
            {
                return NotFound();
            }
            ViewData["DesempenoNivelId"] = new SelectList(_context.DesempenoCapitulos, "DesempenoCapituloId", "DesempenoCapituloId", muerte.DesempenoNivelId);
            ViewData["EnemigoId"] = new SelectList(_context.Enemigos, "EnemigoId", "EnemigoId", muerte.EnemigoId);
            return View(muerte);
        }

        // POST: Muertes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MuerteId,EnemigoId,DesempenoNivelId,TiempoMuerte,X,Y")] Muerte muerte)
        {
            if (id != muerte.MuerteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muerte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuerteExists(muerte.MuerteId))
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
            ViewData["DesempenoNivelId"] = new SelectList(_context.DesempenoCapitulos, "DesempenoCapituloId", "DesempenoCapituloId", muerte.DesempenoNivelId);
            ViewData["EnemigoId"] = new SelectList(_context.Enemigos, "EnemigoId", "EnemigoId", muerte.EnemigoId);
            return View(muerte);
        }

        // GET: Muertes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Muertes == null)
            {
                return NotFound();
            }

            var muerte = await _context.Muertes
                .Include(m => m.DesempenoNivel)
                .Include(m => m.Enemigo)
                .FirstOrDefaultAsync(m => m.MuerteId == id);
            if (muerte == null)
            {
                return NotFound();
            }

            return View(muerte);
        }

        // POST: Muertes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Muertes == null)
            {
                return Problem("Entity set 'KillmechContext.Muertes'  is null.");
            }
            var muerte = await _context.Muertes.FindAsync(id);
            if (muerte != null)
            {
                _context.Muertes.Remove(muerte);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuerteExists(int id)
        {
          return (_context.Muertes?.Any(e => e.MuerteId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> VistaHeatmap()
        {
            // Get the data from the view and map it to a list of the desired type
            var viewModel = await _context.Zona_Mas_Muertes
                .Select(h => new zona_mas_muertes
                {
                    XZone = h.XZone,
                    YZone = h.YZone,
                    NumMuertes = h.NumMuertes
                })
                .ToListAsync();

            // Prepare the data for charts or other visualization
            ViewBag.XZones = viewModel.Select(v => v.XZone).ToList();
            ViewBag.YZones = viewModel.Select(v => v.YZone).ToList();
            ViewBag.NumMuertes = viewModel.Select(v => v.NumMuertes).ToList();

            // Return the view 'GraficosHeatmap' with the model 'viewModel'
            return View("GraficosHeatmap", viewModel);
        }
    }
}
