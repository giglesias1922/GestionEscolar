using Escuela_Back.Models;

namespace Escuela_Back.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<List<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(Guid id);
        Task<Estudiante> AddAsync(Estudiante estudiante);
        Task<Estudiante?> UpdateAsync(Guid id, Estudiante estudiante);
        Task<bool> DeleteAsync(Guid id);
    }
}
