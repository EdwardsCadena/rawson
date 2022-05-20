using System.ComponentModel.DataAnnotations;

namespace Prueba.Models
{
    public class People
    {
        [Key]
        public int Idpersons { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public DateTime FechNacimiento { get; set; }
    }
}
