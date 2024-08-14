using Microsoft.AspNetCore.Mvc;
using sistema_eventos.Helper;
using sistema_eventos.Models;
using sistema_eventos.Repository;

namespace sistema_eventos.Controllers
{
    public class CadeiraController : Controller
    {
        private readonly ICadeiraRepository _cadeiraRepository;
        private readonly ISessionAdmin _sessionAdmin;
        public CadeiraController(ICadeiraRepository cadeiraRepository, ISessionAdmin sessionAdmin)
        {
            _cadeiraRepository = cadeiraRepository;
            _sessionAdmin = sessionAdmin;
        }
        public IActionResult Index()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            var cadeiras = _cadeiraRepository.GetCadeiras().ToList();
            return View(cadeiras);
        }
        public IActionResult NovaCadeira()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            return View();
        }
        [HttpPost]
        public IActionResult NovaCadeira(CadeiraModel cadeiras)
        {
            try
            {
                _cadeiraRepository.Adicionar(cadeiras);
                TempData["success"] = "Cadeira adicionado com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Error ao adicionar cadeira. {err.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Excluir(int id)
        {
            try
            {
                _cadeiraRepository.Deletar(id);
                TempData["success"] = "Cadeira deletado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Erro ao deletar cadeira {err.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
