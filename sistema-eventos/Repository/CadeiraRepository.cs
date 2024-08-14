using sistema_eventos.Db;
using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public class CadeiraRepository : ICadeiraRepository
    {
        private readonly BancoContext _bancoContext;
        public CadeiraRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public CadeiraModel ListaPorId(int id)
        {
            return _bancoContext.Cadeiras.FirstOrDefault(x => x.id == id);
        }
        public bool Deletar(int id)
        {
            CadeiraModel deletarCadeira = ListaPorId(id);
            if (deletarCadeira == null) throw new System.Exception("Algo deu errado");
            _bancoContext.Cadeiras.Remove(deletarCadeira);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<CadeiraModel> GetCadeiras()
        {
            return _bancoContext.Cadeiras.ToList();
        }
        
        CadeiraModel ICadeiraRepository.Adicionar(CadeiraModel cadeiras)
        {
            _bancoContext.Cadeiras.Add(cadeiras);
            _bancoContext.SaveChanges();
            return (cadeiras);
        }
    }
}
