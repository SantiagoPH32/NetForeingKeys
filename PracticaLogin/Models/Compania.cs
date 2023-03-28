using System.ComponentModel.DataAnnotations;

namespace PracticaLogin.Models
{
    public class Compania
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "El Campo NIT es obligatorio")]
        public int NIT { get; set; }
        [Required(ErrorMessage = "El Campo Nombre es obligatorio")]
        public String Nombre { get; set; }

        public virtual ICollection<Conductor>? Conductores { get; set; }
    }
}
