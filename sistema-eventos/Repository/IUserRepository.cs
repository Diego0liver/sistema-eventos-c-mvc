using sistema_eventos.Enum;
using sistema_eventos.Models;
using sistema_eventos.ViewModel;

namespace sistema_eventos.Repository
{
    public interface IUserRepository
    {
        UserModel BuscarLogin(string email);
        UserModel BuscarLoginAdmin(string email, UserTipo userTipo);
        List<UserModel> GetUsers();
        UserModel Adicionar(UserModel user);
        UserModel Editar(UserModel user);
        UserModel ListaPorId(int id);
        bool Deletar(int id);
    }
}
