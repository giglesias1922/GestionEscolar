namespace Escuela_Back.Models
{
    public class Asignatura
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
