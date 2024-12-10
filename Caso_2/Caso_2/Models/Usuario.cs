using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Caso_2.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [Required]
        public string Rol { get; set; }
    }
}
