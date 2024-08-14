using Newtonsoft.Json;
using sistema_eventos.Models;

namespace sistema_eventos.Helper
{
    public class SessionAdmin : ISessionAdmin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionAdmin(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserModel BuscarSessionAdmin()
        {
            string sessionBuscar = _httpContextAccessor.HttpContext.Session.GetString("sessionAdmin");
            if (string.IsNullOrEmpty(sessionBuscar)) return null;
            return JsonConvert.DeserializeObject<UserModel>(sessionBuscar);
        }

        public void CriarSessionAdmin(UserModel userModel)
        {
            string valorSessionAdmin = JsonConvert.SerializeObject(userModel);
            _httpContextAccessor.HttpContext.Session.SetString("sessionAdmin", valorSessionAdmin);
        }

        public void RemoverSessionAdmin()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sessionAdmin");
        }
    }
}
