using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaLogin.Models
{
    public class Conductor
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "El Campo Documento es obligatorio")]
        public int Documento { get; set; }
        [Required(ErrorMessage = "El Campo Nombre es obligatorio")]
        public String NombreCompleto { get; set; }

        public String Correo { get; set; }
        [Required(ErrorMessage = "El Campo Celular es obligatorio")]
        public int Celular { get; set; }


        //llave foranea
        public Guid IdCompania { get; set; }
        [ForeignKey("IdCompania")]
        public virtual Compania? Compania { get; set; }

        public virtual ICollection<Vehiculo>? Vehiculos { get; set; }
        public virtual ICollection<Viaje>? Viajes { get; set; }
    }
}
