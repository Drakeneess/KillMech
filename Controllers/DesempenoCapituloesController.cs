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
    public class DesempenoCapituloesController : Controller
    {
        private readonly KillmechContext _context;

        public DesempenoCapituloesController(KillmechContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string nivel, int page = 1)
        {
            int pageSize = 3;
            int startIndex = (page - 1) * pageSize;

            IQueryable<DesempenoCapitulo> desempenoCapitulosQuery = _context.DesempenoCapitulos
                .Include(d => d.Nivel)
                .Include(d => d.Partida);

            // Filtrar por nivel si se especifica
            if (!string.IsNullOrEmpty(nivel))
            {
                desempenoCapitulosQuery = desempenoCapitulosQuery.Where(d => d.Nivel.Nombre == nivel);
            }

            var desempenoCapitulos = await desempenoCapitulosQuery
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            int totalDesempenoCapitulos = await desempenoCapitulosQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalDesempenoCapitulos / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(desempenoCapitulos);
        }




        // GET: DesempenoCapituloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desempenoCapitulo = await _context.DesempenoCapitulos
                .Include(d => d.Nivel)
                .Include(d => d.Partida)
                .FirstOrDefaultAsync(m => m.DesempenoCapituloId == id);
            if (desempenoCapitulo == null)
            {
                return NotFound();
            }

            return View(desempenoCapitulo);
        }

        // GET: DesempenoCapituloes/Create
        public IActionResult Create()
        {
            ViewData["NivelId"] = new SelectList(_context.Capitulos, "NivelID", "NivelID");
            ViewData["PartidaId"] = new SelectList(_context.Partida, "PartidaId", "PartidaId");
            return View();
        }

        // POST: DesempenoCapituloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesempenoCapituloId,PartidaId,NivelId,Completo,TiempoCompletacion,DanoTotalHecho,DanoTotalRecibido,EvasionesRealizadas,DisparosRealizados,DisparosAcertados,TransformacionesRealizadas,DanoJetTotal,DanoMechaTotal")] DesempenoCapitulo desempenoCapitulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desempenoCapitulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NivelId"] = new SelectList(_context.Capitulos, "NivelID", "NivelID", desempenoCapitulo.NivelId);
            ViewData["PartidaId"] = new SelectList(_context.Partida, "PartidaId", "PartidaId", desempenoCapitulo.PartidaId);
            return View(desempenoCapitulo);
        }

        // GET: DesempenoCapituloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desempenoCapitulo = await _context.DesempenoCapitulos.FindAsync(id);
            if (desempenoCapitulo == null)
            {
                return NotFound();
            }
            ViewData["NivelId"] = new SelectList(_context.Capitulos, "NivelID", "NivelID", desempenoCapitulo.NivelId);
            ViewData["PartidaId"] = new SelectList(_context.Partida, "PartidaId", "PartidaId", desempenoCapitulo.PartidaId);
            return View(desempenoCapitulo);
        }

        // POST: DesempenoCapituloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesempenoCapituloId,PartidaId,NivelId,Completo,TiempoCompletacion,DanoTotalHecho,DanoTotalRecibido,EvasionesRealizadas,DisparosRealizados,DisparosAcertados,TransformacionesRealizadas,DanoJetTotal,DanoMechaTotal")] DesempenoCapitulo desempenoCapitulo)
        {
            if (id != desempenoCapitulo.DesempenoCapituloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desempenoCapitulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesempenoCapituloExists(desempenoCapitulo.DesempenoCapituloId))
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
            ViewData["NivelId"] = new SelectList(_context.Capitulos, "NivelID", "NivelID", desempenoCapitulo.NivelId);
            ViewData["PartidaId"] = new SelectList(_context.Partida, "PartidaId", "PartidaId", desempenoCapitulo.PartidaId);
            return View(desempenoCapitulo);
        }

        // GET: DesempenoCapituloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desempenoCapitulo = await _context.DesempenoCapitulos
                .Include(d => d.Nivel)
                .Include(d => d.Partida)
                .FirstOrDefaultAsync(m => m.DesempenoCapituloId == id);
            if (desempenoCapitulo == null)
            {
                return NotFound();
            }

            return View(desempenoCapitulo);
        }

        // POST: DesempenoCapituloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desempenoCapitulo = await _context.DesempenoCapitulos.FindAsync(id);
            if (desempenoCapitulo != null)
            {
                _context.DesempenoCapitulos.Remove(desempenoCapitulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesempenoCapituloExists(int id)
        {
            return _context.DesempenoCapitulos.Any(e => e.DesempenoCapituloId == id);
        }

        public async Task<IActionResult> VistaDificultadCapitulo()
        {
            // Obtiene los datos de la vista y los mapea a una lista del tipo deseado
            var viewModel = await _context.DificultadCapitulos
                .Select(v => new DificultadCapitulo
                {
                    NombreCapitulo = v.NombreCapitulo,
                    HorasPromedioCompletacion = v.HorasPromedioCompletacion,
                    DanoRecibidoPromedio = v.DanoRecibidoPromedio,
                    DisparosRealizadosPromedio = v.DisparosRealizadosPromedio,
                    PorcentajeDisparosAcertados = v.PorcentajeDisparosAcertados,
                    TransformacionesRealizadasPromedio = v.TransformacionesRealizadasPromedio,
                    PorcentajeCompletados = v.PorcentajeCompletados,
                    IndiceDificultad = v.IndiceDificultad
                })
                .ToListAsync();

            // Preparar los datos para los gráficos u otra visualización
            ViewBag.NombreCapitulos = viewModel.Select(v => v.NombreCapitulo).ToList();
            ViewBag.HorasPromedioCompletacion = viewModel.Select(v => v.HorasPromedioCompletacion).ToList();
            ViewBag.DanoRecibidoPromedio = viewModel.Select(v => v.DanoRecibidoPromedio).ToList();
            ViewBag.DisparosRealizadosPromedio = viewModel.Select(v => v.DisparosRealizadosPromedio).ToList();
            ViewBag.PorcentajeDisparosAcertados = viewModel.Select(v => v.PorcentajeDisparosAcertados).ToList();
            ViewBag.TransformacionesRealizadasPromedio = viewModel.Select(v => v.TransformacionesRealizadasPromedio).ToList();
            ViewBag.PorcentajeCompletados = viewModel.Select(v => v.PorcentajeCompletados).ToList();
            ViewBag.IndiceDificultad = viewModel.Select(v => v.IndiceDificultad).ToList();

            // Retorna la vista con el modelo 'viewModel'
            return View("GraficosCapitulo",viewModel);
        }

    }
}
