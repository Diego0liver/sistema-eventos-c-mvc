using Microsoft.AspNetCore.Mvc;
using sistema_eventos.Helper;
using sistema_eventos.Models;
using sistema_eventos.Repository;

namespace sistema_eventos.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _usersRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionCliente _sessionCliente;
        private readonly ISessionAdmin _sessionAdmin;
        public UserController(IUserRepository usersRepository, ITicketRepository ticketRepository, ISessionCliente sessionCliente, ISessionAdmin sessionAdmin)
        {
            _usersRepository = usersRepository;
            _ticketRepository = ticketRepository;
            _sessionCliente = sessionCliente;
            _sessionAdmin = sessionAdmin;
        }
        public IActionResult CadastroCliente()
        {
            return View();
        }
        public IActionResult LoginCliente()
        {
            if(_sessionCliente.BuscarSession() != null ) return RedirectToAction("MinhasCompras");
            return View();
        }
        [HttpPost]
        public IActionResult LogarCliente(LoginModel userModel)
        {
            try
            {
                UserModel cliente = _usersRepository.BuscarLogin(userModel.email);
                if (cliente != null)
                {
                    if (cliente.SenhaValida(userModel.password))
                    {
                        _sessionCliente.CriarSessionUsuario(cliente);
                        return RedirectToAction("MinhasCompras");
                    }

                    TempData["error"] = $"E-mail ou senha invalida";
                }

                TempData["error"] = $"Usuario nao encontrado";
                return RedirectToAction("LoginCliente");
            }
            catch (Exception err)
            {
                TempData["error"] = $"E-mail ou senha invalida. {err.Message}";
                return RedirectToAction("LoginCliente");
            }
        }
        public IActionResult SairCliente()
        {
            _sessionCliente.RemoverSessionUsuario();
            return RedirectToAction("LoginCliente");
        }

        public async Task<IActionResult> MinhasCompras()
        {
            if (_sessionCliente.BuscarSession() == null) return RedirectToAction("LoginCliente");
            var minhasCompras = await _ticketRepository.GetTickets();

            if (minhasCompras == null)
            {
                return NotFound();
            }
           
            return View(minhasCompras);
        }
        public async Task<IActionResult> DetalhesCompra(int id)
        {
            if (_sessionCliente.BuscarSession() == null) return RedirectToAction("LoginCliente");
            var userDetails = await _ticketRepository.TicketsDetalhesAsync(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }
        [HttpPost]
        public IActionResult NovoCliente(UserModel users)
        {
            try
            {
                UserModel cliente = _usersRepository.BuscarLogin(users.email);
                if (cliente == null)
                {
                    _usersRepository.Adicionar(users);
                    TempData["success"] = "Cadastro afetuado com sucesso.";
                    return RedirectToAction("LoginCliente");
                }
                TempData["error"] = "Esse e-mail ja existe";
                return RedirectToAction("CadastroCliente");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Error no cadastro. {err.Message}";
                return RedirectToAction("CadastroCliente");
            }
        }

        //////////////PAINEL ADMIN DO USER////////////
        public IActionResult UserListAdmin()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            var users = _usersRepository.GetUsers().ToList();
            return View(users);
        }
        public IActionResult NovoUser()
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            return View();
        }
        public IActionResult UsuarioId(int id)
        {
            if (_sessionAdmin.BuscarSessionAdmin() == null) return RedirectToAction("LoginAdmin", "Admin");
            UserModel usuarioId = _usersRepository.ListaPorId(id);
            return View(usuarioId);
        }
        [HttpPost]
        public IActionResult NovoUser(UserModel users)
        {
            try
            {
                _usersRepository.Adicionar(users);
                TempData["success"] = "Usuario adicionado com sucesso.";
                return RedirectToAction("UserListAdmin");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Error ao adicionar usuario. {err.Message}";
                return RedirectToAction("UserListAdmin");
            }
        }
        [HttpPost]
        public IActionResult Excluir(int id)
        {
            try
            {
                _usersRepository.Deletar(id);
                TempData["success"] = "Usuario deletado com sucesso";
                return RedirectToAction("UserListAdmin");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Erro ao deletar usuario {err.Message}";
                return RedirectToAction("UserListAdmin");
            }
        }
        [HttpPost]
        public IActionResult EditarUsuario(UserModel users)
        {
            try
            {
                _usersRepository.Editar(users);
                TempData["success"] = "Usuario atualizado com sucesso.";
                return RedirectToAction("UserListAdmin", "User");
            }
            catch (Exception err)
            {
                TempData["error"] = $"Error ao atualizar usuario. {err.Message}";
                return RedirectToAction("UserListAdmin", "User");
            }
        }
    }
}
