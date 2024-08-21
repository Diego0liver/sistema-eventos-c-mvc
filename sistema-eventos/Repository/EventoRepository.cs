using Microsoft.EntityFrameworkCore;
using sistema_eventos.Db;
using sistema_eventos.Enum;
using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly BancoContext _bancoContext;
        public EventoRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<EventosModel> GetEventos()
        {
            return _bancoContext.Eventos.ToList();
        }

        public async Task AdicionarAsync(EventosModel eventos)
        {
            _bancoContext.Eventos.Add(eventos);
            await _bancoContext.SaveChangesAsync();
        }

        EventosModel IEventoRepository.GetById(int id)
        {
            return _bancoContext.Eventos.FirstOrDefault(x => x.id == id);
        }
        HashSet<int> IEventoRepository.GetCadeirasReservadasIds(int evento_id)
        {
            return _bancoContext.CadeiraEvento
                    .Where(ce => ce.evento_id == evento_id)
                    .Select(ce => ce.cadeira_id)
                    .ToHashSet();
        }
        public EventosModel ListaPorId(int id)
        {
            return _bancoContext.Eventos.FirstOrDefault(x => x.id == id);
        }
        public bool Deletar(int id)
        {
            EventosModel deletarEvento = ListaPorId(id);
            if (deletarEvento == null) throw new System.Exception("Algo deu errado");
            _bancoContext.Eventos.Remove(deletarEvento);
            _bancoContext.SaveChanges();

            return true;
        }

        public EventosModel MudarStatus(int id, Status novoStatus)
        {
            EventosModel evento = ListaPorId(id);
            if (evento == null) throw new System.Exception("Algo deu errado");
            evento.status = novoStatus;
            _bancoContext.SaveChanges();
            return evento;
        }
    }
}
