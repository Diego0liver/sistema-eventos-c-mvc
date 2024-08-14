using sistema_eventos.Enum;

namespace sistema_eventos.ViewModel
{
    public class UserCadeiraEventoViewModel
    {
        public string EventoNome { get; set; }
        public DateTime EventoData { get; set; }
        public decimal EventoPreco { get; set; }
        public Status EventoStatus { get; set; }
        public string CadeiraFila { get; set; }
        public int CadeiraNumero { get; set; }
        public int TickerId { get; set; }
        public string TicketCodigo { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
