using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaLogin.Models
{
    public class Viaje
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "El Campo Fecha es obligatorio")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El Campo Origen es obligatorio")]
        public string Origen { get; set; }
        [Required(ErrorMessage = "El Campo Destino es obligatorio")]
        public string Destino { get; set; }
        [Required(ErrorMessage = "El Campo Cliente es obligatorio")]
        public string Cliente { get; set; }
        public string Producto { get; set; }
      

        public Guid IdConductor { get; set; }

        [ForeignKey("IdConductor")]

        public virtual Conductor? Conductor { get; set; }
    }
}
