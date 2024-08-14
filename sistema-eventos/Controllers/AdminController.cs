using Microsoft.AspNetCore.Mvc;
using sistema_eventos.Helper;
using sistema_eventos.Models;
using sistema_eventos.Repository;

namespace sistema_eventos.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _usersRepository;
        private readonly ISessionAdmin _sessionAdmin;
        public AdminController(IUserRepository usersRepository, ISessionAdmin sessionAdmin)
        {
            _usersRepository = usersRepository;
            _sessionAdmin = sessionAdmin;
        }
        public IActionResult Index()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin");
            return View();
        }
        public IActionResult LoginAdmin()
        {
            if (_sessionAdmin.BuscarSessionAdmin() != null) return RedirectToAction("Index");
            return View();
        }
        public IActionResult SairAdmin()
        {
            _sessionAdmin.RemoverSessionAdmin();
            return RedirectToAction("LoginAdmin");
        }
        [HttpPost]
        public IActionResult LogarAdmin(LoginModel userModel)
        {
            try
            {
                UserModel admin = _usersRepository.BuscarLoginAdmin(userModel.email, Enum.UserTipo.Admin);
                if (admin != null)
                {
                    if (admin.SenhaValida(userModel.password))
                    {
                        _sessionAdmin.CriarSessionAdmin(admin);
                        return RedirectToAction("Index");
                    }

                    TempData["error"] = $"E-mail ou senha invalida";
                }

                TempData["error"] = $"Usuario nao encontrado";
                return RedirectToAction("LoginAdmin");
            }
            catch (Exception err)
            {
                TempData["error"] = $"E-mail ou senha invalida. {err.Message}";
                return RedirectToAction("LoginAdmin");
            }
        }
    }
}
