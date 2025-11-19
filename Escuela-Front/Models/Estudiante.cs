using System.ComponentModel.DataAnnotations;

namespace Escuela_Front.Models
{
    public class Estudiante
    {
        public Guid Id { get; set; }


        [Required]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;


        [Range(5, 18, ErrorMessage = "La edad debe estar entre 5 y 18 años")]
        public int Edad { get; set; }
    }
}
