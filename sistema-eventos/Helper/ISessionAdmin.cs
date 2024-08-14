using sistema_eventos.Models;

namespace sistema_eventos.Helper
{
    public interface ISessionAdmin
    {
        void CriarSessionAdmin(UserModel userModel);
        void RemoverSessionAdmin();
        UserModel BuscarSessionAdmin();
    }
}
