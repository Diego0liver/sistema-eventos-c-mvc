using sistema_eventos.Db;
using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public class CadeiraEventoRepository : ICadeiraEventoRepository
    {
        private readonly BancoContext _bancoContext;
        public CadeiraEventoRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public CadeiraEventoModel Adicionar(CadeiraEventoModel cadeirasEventos)
        {
            _bancoContext.CadeiraEvento.Add(cadeirasEventos);
            _bancoContext.SaveChanges();
            return cadeirasEventos;
        }
    }
}
