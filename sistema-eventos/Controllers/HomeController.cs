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

        public IActionResult Index(string busca)
        {
            ViewBag.Busca = busca;
            var eventos = _eventoRepository.GetEventos().Where(e => e.status == 0);
            if(!string.IsNullOrEmpty(busca))
            {
                eventos = eventos.Where(e => e.nome.Contains(busca, StringComparison.OrdinalIgnoreCase));
            }
            return View(eventos.ToList());
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
