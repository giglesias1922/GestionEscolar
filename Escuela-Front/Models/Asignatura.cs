using System.ComponentModel.DataAnnotations;

namespace Escuela_Front.Models
{
    public class Asignatura
    {
        public Guid Id { get; set; }


        [Required]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        public string Nombre { get; set; } = string.Empty;


        [MaxLength(200, ErrorMessage = "La descripción debe tener máximo 200 caracteres")]
        public string? Descripcion { get; set; }
    }
}
