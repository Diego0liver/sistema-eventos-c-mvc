using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public interface ICadeiraEventoRepository
    {
        CadeiraEventoModel Adicionar(CadeiraEventoModel cadeirasEventos);
    }
}
