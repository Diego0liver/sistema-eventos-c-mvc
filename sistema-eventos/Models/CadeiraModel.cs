using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_eventos.Models
{
    [Table("cadeiras")]
    public class CadeiraModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Esta campo e obrigatorio")]
        public string fila { get; set; }

        [Required(ErrorMessage = "Esta campo e obrigatorio")]
        public int cadeira { get; set; }
        public ICollection<CadeiraEventoModel> CadeiraEvento { get; set; }
    }
}
