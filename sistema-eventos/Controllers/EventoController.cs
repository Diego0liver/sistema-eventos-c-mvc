using Microsoft.AspNetCore.Mvc;
using sistema_eventos.Enum;
using sistema_eventos.Helper;
using sistema_eventos.Models;
using sistema_eventos.Repository;
using sistema_eventos.ViewModel;

namespace sistema_eventos.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ICadeiraRepository _cadeiraRepository;
        private readonly ICadeiraEventoRepository _cadeiraEventoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionCliente _sessionCliente;
        private readonly ISessionAdmin _sessionAdmin;
        public EventoController(IEventoRepository eventoRepository, ICadeiraRepository cadeiraRepository, ICadeiraEventoRepository cadeiraEventoRepository, ITicketRepository ticketRepository, ISessionCliente sessionCliente, ISessionAdmin sessionAdmin)
        {
            _eventoRepository = eventoRepository;
            _cadeiraRepository = cadeiraRepository;
            _cadeiraEventoRepository = cadeiraEventoRepository;
            _ticketRepository = ticketRepository;
            _sessionCliente = sessionCliente;
            _sessionAdmin = sessionAdmin;
        }

        public IActionResult Index()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            var eventos = _eventoRepository.GetEventos();
            return View(eventos);
        }

        public IActionResult NovoEvento()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            return View();
        }
        [HttpPost]
        public IActionResult NovoEvento(EventosModel eventos)
        {
            try
            {
                // Convertendo a datatime
                if (eventos.data_hora.Kind == DateTimeKind.Unspecified)
                {
                    eventos.data_hora = DateTime.SpecifyKind(eventos.data_hora, DateTimeKind.Utc);
                }
                _eventoRepository.Adicionar(eventos);
                TempData["success"] = "Evento adicionado com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Error ao adicionar evento. {err.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult DetalhesEvento(int id)
        {
            EventosModel eventoId = _eventoRepository.GetById(id);
            List<CadeiraModel> cadeiras = _cadeiraRepository.GetCadeiras();
            CadeiraEventoModel cadeirasEventosModel = new CadeiraEventoModel();
            var cadeirasReservadasIds = _eventoRepository.GetCadeirasReservadasIds(id);

            var cadeiraEvento = new EventoCadeiraViewModel
            {
                Evento = eventoId,
                Cadeiras = cadeiras,
                CadeirasReservadasIds = cadeirasReservadasIds,
            };

            return View(cadeiraEvento);
        }
        [HttpPost]
        public ActionResult reservarCadeira(CadeiraEventoModel cadeirasEventos)
        {
            try
            {
                var usuarioLogado = _sessionCliente.BuscarSession();
                //Adcicionar ticket
                if (usuarioLogado != null)
                {
                    _cadeiraEventoRepository.Adicionar(cadeirasEventos);
                    var novoTicket = new TicketsModel
                    {
                        tickets_codigo = DateTime.Now.ToString("yyyyMMddHHmmss") + usuarioLogado.id,
                        cadeiras_eventos_id = cadeirasEventos.id,
                        user_id = usuarioLogado.id,
                        data_compra = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
                    };
                    _ticketRepository.Adicionar(novoTicket);

                    TempData["success"] = "Compra efetuada com sucesso.";
                    return RedirectToAction("DetalhesEvento", new { id = cadeirasEventos.evento_id });
                }
                TempData["error"] = "Voce precisa estar logado para efetuar a compra";
                return RedirectToAction("DetalhesEvento", new { id = cadeirasEventos.evento_id });
            }
            catch (Exception err)
            {
                TempData["error"] = $"Error ao adicionar compra. {err.Message}";
                return RedirectToAction("DetalhesEvento", new { id = cadeirasEventos.evento_id });
            }
        }
        [HttpPost]
        public IActionResult Excluir(int id)
        {
            try
            {
                _eventoRepository.Deletar(id);
                TempData["success"] = "Evento deletado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Erro ao deletar evento {err.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult MudarStatus(int id, Status novoStatus)
        {
            try
            {
                var eventoAtualizado = _eventoRepository.MudarStatus(id, novoStatus);

                TempData["success"] = "Status do evento mudado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Erro ao atualizar status do evento {err.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
