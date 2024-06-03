using KillMech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KillMech.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KillmechContext _context;

        public HomeController(ILogger<HomeController> logger, KillmechContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var totalItems = await _context.Partidas_Ultimos_Seis_Meses.CountAsync();
            var viewModel = await _context.Partidas_Ultimos_Seis_Meses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new partidas_ultimos_seis_meses
                {
                    PartidaID = p.PartidaID,
                    JugadorID = p.JugadorID,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    CampanaCompleta = p.CampanaCompleta,
                    Mes = p.Mes,
                    NumeroDePartidas = p.NumeroDePartidas,
                    PartidasCompletadas = p.PartidasCompletadas
                })
                .ToListAsync();

            ViewBag.Meses = await _context.Partidas_Ultimos_Seis_Meses
                .Select(v => v.Mes)
                .Distinct()
                .ToListAsync();
            ViewBag.NumeroDePartidas = await _context.Partidas_Ultimos_Seis_Meses
                .GroupBy(v => v.Mes)
                .Select(g => g.First().NumeroDePartidas)
                .ToListAsync();
            ViewBag.PartidasCompletadas = await _context.Partidas_Ultimos_Seis_Meses
                .GroupBy(v => v.Mes)
                .Select(g => g.First().PartidasCompletadas)
                .ToListAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
