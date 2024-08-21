using sistema_eventos.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_eventos.Models
{
    [Table("eventos")]
    public class EventosModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal preco { get; set; }
        [Required]
        public DateTime data_hora { get; set; }
        public Status status { get; set; }
        public string? banner { get; set; }

        public ICollection<CadeiraEventoModel> CadeiraEvento { get; set; }
    }
}
