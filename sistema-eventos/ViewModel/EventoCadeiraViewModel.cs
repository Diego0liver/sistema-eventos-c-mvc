using sistema_eventos.Models;

namespace sistema_eventos.ViewModel
{
    public class EventoCadeiraViewModel
    {
        public EventosModel Evento { get; set; }
        public List<CadeiraModel> Cadeiras { get; set; }
        public CadeiraEventoModel CadeirasEventos { get; set; }
        public HashSet<int> CadeirasReservadasIds { get; set; }
    }
}
