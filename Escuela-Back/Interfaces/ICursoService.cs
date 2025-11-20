using Escuela_Back.Models;

namespace Escuela_Back.Interfaces
{
    public interface ICursoService
    {
        Task<List<Curso>> GetAllAsync();
        Task<Curso?> GetByIdAsync(Guid id);
        Task<Curso> CreateAsync(Curso model);
        Task<Curso?> UpdateAsync(Guid id, Curso model);
        Task<bool> DeleteAsync(Guid id);

        Task<bool> VincularAsignatura(Guid cursoId, Guid asignaturaId);
        Task<bool> DesvincularAsignatura(Guid cursoId, Guid asignaturaId);
        Task<bool> VincularEstudiante(Guid cursoId, Guid estudianteId);
        Task<bool> DesvincularEstudiante(Guid cursoId, Guid estudianteId);
    }

}
