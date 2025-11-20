using Escuela_Back.Interfaces;

namespace Escuela_Back.Services
{
    public class CursoLogic : ICursoLogic
    {
        public bool ValidarColor(string color)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(color, "^#[0-9A-F]{6}$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        public bool ValidarNombre(string nombre)
        {
            return !string.IsNullOrWhiteSpace(nombre) && nombre.Length >= 3;
        }
    }
}
