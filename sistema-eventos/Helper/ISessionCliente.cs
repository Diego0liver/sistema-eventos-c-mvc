using sistema_eventos.Models;

namespace sistema_eventos.Helper
{
    public interface ISessionCliente
    {
        void CriarSessionUsuario(UserModel userModel);
        void RemoverSessionUsuario();
        UserModel BuscarSession();
    }
}
