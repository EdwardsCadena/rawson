using System.ComponentModel.DataAnnotations;

namespace Prueba.Models
{
    public class InfoPeople
    {
        [Key]
        public int Id { get; set; }
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direcion { get; set; } = null!;
        public People people { get; set; } = null!;
    }
}
