using Escuela_Back.Models;

namespace Escuela_Back.Interfaces
{
    public interface ICursoRepository
    {
        Task<List<Curso>> GetAllAsync();
        Task<Curso?> GetByIdAsync(Guid id);
        Task<Curso> AddAsync(Curso curso);
        Task<Curso?> UpdateAsync(Guid id, Curso curso);
        Task<bool> DeleteAsync(Guid id);

        Task<bool> VincularAsignatura(Guid cursoId, Guid asignaturaId);
        Task<bool> DesvincularAsignatura(Guid cursoId, Guid asignaturaId);

        Task<bool> VincularEstudiante(Guid cursoId, Guid estudianteId);
        Task<bool> DesvincularEstudiante(Guid cursoId, Guid estudianteId);

        Task<int> GetCursosByEstudiante(Guid estudianteId);
        Task<int> GetCursosByAsignatura(Guid asignaturaId);
    }
}
