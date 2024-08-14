using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_eventos.Models
{
    [Table("cadeiras_eventos")]
    public class CadeiraEventoModel
    {
        [Key]
        public int id { get; set; }
        public int cadeira_id { get; set; }
        public int evento_id { get; set; }
        public CadeiraModel cadeiras { get; set; }
        public EventosModel eventos { get; set; }
        public ICollection<TicketsModel> Tickets { get; set; }
    }
}
