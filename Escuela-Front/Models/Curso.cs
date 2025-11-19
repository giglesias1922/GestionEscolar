using System.ComponentModel.DataAnnotations;

namespace Escuela_Front.Models
{
    public class Curso
    {
        public Guid Id { get; set; }


        [Required]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        public string Nombre { get; set; } = string.Empty;


        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Color debe ser hexadecimal, por ejemplo #FF0000")]
        public string Color { get; set; } = "#FF0000";



        public string Icono { get; set; } = "📘";


        // Relación por IDs simples para no complicar el ejemplo
        public List<Guid> Asignaturas { get; set; } = new();
        public List<Guid> Estudiantes { get; set; } = new();
    }
}
