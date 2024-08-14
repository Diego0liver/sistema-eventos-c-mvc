using Newtonsoft.Json;
using sistema_eventos.Models;

namespace sistema_eventos.Helper
{
    public class SessionCliente : ISessionCliente
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionCliente(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserModel BuscarSession()
        {
            string sessionBuscar = _httpContextAccessor.HttpContext.Session.GetString("sessionCliente");
            if (string.IsNullOrEmpty(sessionBuscar)) return null;
            return JsonConvert.DeserializeObject<UserModel>(sessionBuscar);
        }

        public void CriarSessionUsuario(UserModel userModel)
        {
            string valorSession = JsonConvert.SerializeObject(userModel);
            _httpContextAccessor.HttpContext.Session.SetString("sessionCliente", valorSession);
        }

        public void RemoverSessionUsuario()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sessionCliente");
        }
    }
}
