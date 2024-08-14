using sistema_eventos.Enum;

namespace sistema_eventos.ViewModel
{
    public class TicketDetalhesViewModel
    {
        public int TickerId { get; set; }
        public string TicketCodigo { get; set; }
        public DateTime TicketDataCompra { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente  { get; set; }
        public int Cadeira { get; set; }
        public string FilaCadeira { get; set; }
        public string EventoNome { get; set; }
        public DateTime EventoData { get; set; }
        public decimal EventoPreco { get; set; }
        public Status statusEvento { get; set; }
    }
}
