using Escuela_Back.Models;

namespace Escuela_Back.Interfaces
{
    public interface IAsignaturaRepository
    {
        Task<List<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(Guid id);
        Task<Asignatura> AddAsync(Asignatura asignatura);
        Task<Asignatura?> UpdateAsync(Guid id, Asignatura asignatura);
        Task<bool> DeleteAsync(Guid id);
    }
}
