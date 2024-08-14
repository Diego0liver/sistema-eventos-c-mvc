using sistema_eventos.Models;
using sistema_eventos.ViewModel;

namespace sistema_eventos.Repository
{
    public interface ITicketRepository
    {
        TicketsModel Adicionar(TicketsModel tickets);
        Task<List<UserCadeiraEventoViewModel>> GetTickets();
        Task<List<TicketDetalhesViewModel>> GetTicketsAdmin();
        Task<UserCadeiraEventoViewModel> TicketsDetalhesAsync(int id);
        Task<TicketDetalhesViewModel> TicketsDetalhesAdminAsync(int id);
    }
}
