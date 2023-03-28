using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaLogin.Models
{
    public class Vehiculo
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "El Campo Placa es obligatorio")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "El Campo Marca es obligatorio")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "El Campo Modelo es obligatorio")]
        public string Modelo { get; set; }

        public Guid IdConductor { get; set; }

        [ForeignKey("IdConductor")]

        public virtual Conductor? Conductor { get; set; }
    }
}
