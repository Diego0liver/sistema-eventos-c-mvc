using sistema_eventos.Db;
using sistema_eventos.Enum;
using sistema_eventos.Models;

namespace sistema_eventos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BancoContext _bancoContext;
        public UserRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UserModel Adicionar(UserModel user)
        {
            _bancoContext.Users.Add(user);
            _bancoContext.SaveChanges();
            return (user);
        }

        public UserModel BuscarLogin(string email)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.email == email);
        }
        public UserModel ListaPorId(int id)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.id == id);
        }
        public bool Deletar(int id)
        {
            UserModel deletarUser = ListaPorId(id);
            if (deletarUser == null) throw new System.Exception("Algo deu errado");
            _bancoContext.Users.Remove(deletarUser);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<UserModel> GetUsers()
        {
            return _bancoContext.Users.ToList();
        }

        public UserModel Editar(UserModel user)
        {
            UserModel atualizarUsuario = ListaPorId(user.id);
            if (atualizarUsuario == null) throw new System.Exception("Algo deu errado");
            atualizarUsuario.name = user.name;
            atualizarUsuario.email = user.email;
            atualizarUsuario.tipo_user = user.tipo_user;
            if (!string.IsNullOrEmpty(user.password))
            {
                atualizarUsuario.password = user.password;
            }

            _bancoContext.Users.Update(atualizarUsuario);
            _bancoContext.SaveChanges();
            return atualizarUsuario;
        }

        public UserModel BuscarLoginAdmin(string email, UserTipo userTipo)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.email == email && x.tipo_user == userTipo);
        }
    }
}
