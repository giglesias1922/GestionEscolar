namespace Escuela_Back.Models
{
    public class Estudiante
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int Edad { get; set; }
    }
}
