using Microsoft.AspNetCore.Mvc;
using sistema_eventos.Models;
using sistema_eventos.Repository;
using System.Diagnostics;

namespace sistema_eventos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventoRepository _eventoRepository;
        public HomeController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public IActionResult Index()
        {
            var eventos = _eventoRepository.GetEventos().Where(e => e.status == 0).ToList();
            return View(eventos);
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
