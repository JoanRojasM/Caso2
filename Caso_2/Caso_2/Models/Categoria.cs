using System.ComponentModel.DataAnnotations;

namespace Caso_2.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50, ErrorMessage = "El usuario de registro no puede superar los 50 caracteres.")]
        public string UsuarioRegistro { get; set; }
        public virtual IEnumerable<Evento>? Eventos { get; set; } = new List<Evento>();

    }
}
