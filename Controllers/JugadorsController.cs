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
    public class JugadorsController : Controller
    {
        private readonly KillmechContext _context;

        public JugadorsController(KillmechContext context)
        {
            _context = context;
        }

        // GET: Jugadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jugadors.ToListAsync());
        }

        // GET: Jugadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadors
                .FirstOrDefaultAsync(m => m.JugadorId == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jugadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JugadorId,CodigoJugador,Nombre,TiempoJugado")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jugador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jugador);
        }

        // GET: Jugadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadors.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }
            return View(jugador);
        }

        // POST: Jugadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JugadorId,CodigoJugador,Nombre,TiempoJugado")] Jugador jugador)
        {
            if (id != jugador.JugadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorExists(jugador.JugadorId))
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
            return View(jugador);
        }

        // GET: Jugadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadors
                .FirstOrDefaultAsync(m => m.JugadorId == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugador = await _context.Jugadors.FindAsync(id);
            if (jugador != null)
            {
                _context.Jugadors.Remove(jugador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorExists(int id)
        {
            return _context.Jugadors.Any(e => e.JugadorId == id);
        }

        public async Task<IActionResult> VistaDesempenoJugadores()
        {
            // Obtiene los datos de la vista y los mapea a una lista del tipo deseado
            var viewModel = await _context.Desempeñoporjudadors
                .Select(j => new desempeñoporjudador
                {
                    JugadorID = j.JugadorID,
                    NombreJugador = j.NombreJugador,
                    TiempoTotalJugado = j.TiempoTotalJugado,
                    PartidasJugadas = j.PartidasJugadas,
                    PartidasCompletadas = j.PartidasCompletadas,
                    PartidasAbandonadas = j.PartidasAbandonadas
                })
                .ToListAsync();

            // Preparar los datos para los gráficos u otra visualización
            ViewBag.NombresJugadores = viewModel.Select(v => v.NombreJugador).ToList();
            ViewBag.TiempoTotalJugado = viewModel.Select(v => v.TiempoTotalJugado).ToList();
            ViewBag.PartidasJugadas = viewModel.Select(v => v.PartidasJugadas).ToList();
            ViewBag.PartidasCompletadas = viewModel.Select(v => v.PartidasCompletadas).ToList();
            ViewBag.PartidasAbandonadas = viewModel.Select(v => v.PartidasAbandonadas).ToList();

            // Retorna la vista 'GraficosJugadores' con el modelo 'viewModel'
            return View("GraficosJugadores", viewModel);
        }

    }
}
