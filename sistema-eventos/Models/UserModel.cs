using sistema_eventos.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_eventos.Models
{
    [Table("users")]
    public class UserModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string? token { get; set; }
        public UserTipo tipo_user { get; set; }
        public ICollection<TicketsModel> Tickets { get; set; }
        public bool SenhaValida(string senha)
        {
            return password == senha;
        }
    }
}
