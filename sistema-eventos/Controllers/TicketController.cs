using Microsoft.AspNetCore.Mvc;
using sistema_eventos.Helper;
using sistema_eventos.Repository;

namespace sistema_eventos.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionAdmin _sessionAdmin;
        public TicketController(ITicketRepository ticketRepository, ISessionAdmin sessionAdmin)
        {
            _ticketRepository = ticketRepository;
            _sessionAdmin = sessionAdmin;
        }
        public async Task<IActionResult> Index()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            var tickets = await _ticketRepository.GetTicketsAdmin();

            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }
        public async Task<IActionResult> DetalhesTicket(int id)
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            var ticketDetails = await _ticketRepository.TicketsDetalhesAdminAsync(id);

            if (ticketDetails == null)
            {
                return NotFound();
            }

            return View(ticketDetails);
        }
    }
}
