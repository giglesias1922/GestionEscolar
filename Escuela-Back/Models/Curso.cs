namespace Escuela_Back.Models
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;

        public List<Guid> Asignaturas { get; set; } = new();
        public List<Guid> Estudiantes { get; set; } = new();
    }
}
