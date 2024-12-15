using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Caso_2.Models
{
    public class Inscripcion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public virtual Evento Evento { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required]
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;
        public bool Asistio { get; set; } = false;
    }
}
