using sistema_eventos.Enum;
using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public interface IEventoRepository
    {
        List<EventosModel> GetEventos();
        EventosModel GetById(int id);
        HashSet<int> GetCadeirasReservadasIds(int evento_id);
        EventosModel Adicionar(EventosModel eventos);
        EventosModel ListaPorId(int id);
        EventosModel MudarStatus(int id, Status novoStatus);
        bool Deletar(int id);
    }
}
