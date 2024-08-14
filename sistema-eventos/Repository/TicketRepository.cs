using Microsoft.EntityFrameworkCore;
using sistema_eventos.Db;
using sistema_eventos.Helper;
using sistema_eventos.Models;
using sistema_eventos.ViewModel;

namespace sistema_eventos.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly BancoContext _bancoContext;
        private readonly ISessionCliente _sessionCliente;
        public TicketRepository(BancoContext bancoContext, ISessionCliente sessionCliente)
        {
            _bancoContext = bancoContext;
            _sessionCliente = sessionCliente;
        }
        public TicketsModel Adicionar(TicketsModel tickets)
        {
            _bancoContext.Tickets.Add(tickets);
            _bancoContext.SaveChanges();
            return tickets;
        }


        public async Task<List<UserCadeiraEventoViewModel>> GetTickets()
        {
            var usuarioLogado = _sessionCliente.BuscarSession();
            if (usuarioLogado == null)
            {
                return new List<UserCadeiraEventoViewModel>();
            }
            return await _bancoContext.Tickets
                .Where(x => x.user_id == usuarioLogado.id)
                .Select(c => new UserCadeiraEventoViewModel
                {
                    TickerId = c.id,
                    EventoNome = c.CadeiraEvento.eventos.nome,
                    EventoData = c.CadeiraEvento.eventos.data_hora,
                    EventoStatus = c.CadeiraEvento.eventos.status
                })
                .ToListAsync();
        }

        public async Task<List<TicketDetalhesViewModel>> GetTicketsAdmin()
        {
            return await _bancoContext.Tickets
                .Select(c => new TicketDetalhesViewModel
                {
                    TickerId = c.id,
                    EventoNome = c.CadeiraEvento.eventos.nome,
                    EmailCliente = c.User.email,
                    NomeCliente = c.User.name
                })
                .ToListAsync();
        }

        public async Task<TicketDetalhesViewModel> TicketsDetalhesAdminAsync(int id)
        {
            return await _bancoContext.Tickets
                .Where(c => c.id == id)
                .Select(c => new TicketDetalhesViewModel
                {
                    TickerId = c.id,
                    EventoNome = c.CadeiraEvento.eventos.nome,
                    EventoData = c.CadeiraEvento.eventos.data_hora,
                    statusEvento = c.CadeiraEvento.eventos.status,
                    EventoPreco = c.CadeiraEvento.eventos.preco,
                    FilaCadeira = c.CadeiraEvento.cadeiras.fila,
                    Cadeira = c.CadeiraEvento.cadeiras.cadeira,
                    TicketDataCompra = c.data_compra,
                    TicketCodigo = c.tickets_codigo,
                    NomeCliente = c.User.name,
                    EmailCliente = c.User.email,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserCadeiraEventoViewModel> TicketsDetalhesAsync(int id)
        {
            return await _bancoContext.Tickets
                .Where(c => c.id == id)
                .Select(c => new UserCadeiraEventoViewModel
                {
                    TickerId = c.id,
                    EventoNome = c.CadeiraEvento.eventos.nome,
                    EventoData = c.CadeiraEvento.eventos.data_hora,
                    EventoStatus = c.CadeiraEvento.eventos.status,
                    EventoPreco = c.CadeiraEvento.eventos.preco,
                    CadeiraFila = c.CadeiraEvento.cadeiras.fila,
                    CadeiraNumero = c.CadeiraEvento.cadeiras.cadeira,
                    DataCompra = c.data_compra,
                    TicketCodigo = c.tickets_codigo
                })
                .FirstOrDefaultAsync();
        }
    }
}
