using Escuela_Back.Models;

namespace Escuela_Back.Interfaces
{
    public interface IAsignaturaService
    {
        Task<List<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(Guid id);
        Task<Asignatura> AddAsync(Asignatura model);
        Task<Asignatura> UpdateAsync(Guid id, Asignatura model);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> PuedeEliminarAsync(Guid asignaturaId);
    }
}
