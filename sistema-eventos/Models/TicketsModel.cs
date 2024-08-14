using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_eventos.Models
{
    [Table("tickets")]
    public class TicketsModel
    {
        [Key]
        public int id { get; set; }
        public string tickets_codigo { get; set; }
        public int user_id { get; set; }
        public int cadeiras_eventos_id { get; set; }
        public DateTime data_compra { get; set; }
        public CadeiraEventoModel CadeiraEvento { get; set; }
        public UserModel User { get; set; }
    }
}
