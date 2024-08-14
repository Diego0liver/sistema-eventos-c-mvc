using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public interface ICadeiraRepository
    {
        List<CadeiraModel> GetCadeiras();
        CadeiraModel Adicionar(CadeiraModel cadeiras);
        CadeiraModel ListaPorId(int id);
        bool Deletar(int id);
    }
}
