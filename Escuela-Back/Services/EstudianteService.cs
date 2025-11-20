using Escuela_Back.Interfaces;
using Escuela_Back.Models;

namespace Escuela_Back.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _repo;
        private readonly ICursoRepository _cursos;

        public EstudianteService(IEstudianteRepository repo, ICursoRepository cursos)
        {
            _repo = repo;
            _cursos = cursos;
        }

        public Task<List<Estudiante>> GetAllAsync() =>
            _repo.GetAllAsync();

        public Task<Estudiante?> GetByIdAsync(Guid id) =>
            _repo.GetByIdAsync(id);

        public async Task<Estudiante> AddAsync(Estudiante estudiante)
        {
            if (string.IsNullOrWhiteSpace(estudiante.NombreCompleto) || estudiante.NombreCompleto.Length < 3)
                throw new ArgumentException("El nombre debe tener al menos 3 caracteres.");

            if (estudiante.Edad < 5 || estudiante.Edad > 18)
                throw new ArgumentException("La edad debe estar entre 5 y 18 años.");

            return await _repo.AddAsync(estudiante);
        }

        public async Task<Estudiante?> UpdateAsync(Guid id, Estudiante estudiante)
        {
            if (string.IsNullOrWhiteSpace(estudiante.NombreCompleto) || estudiante.NombreCompleto.Length < 3)
                throw new ArgumentException("El nombre debe tener al menos 3 caracteres.");

            if (estudiante.Edad < 5 || estudiante.Edad > 18)
                throw new ArgumentException("La edad debe estar entre 5 y 18 años.");

            var updated = await _repo.UpdateAsync(id, estudiante);

            return updated;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (!await PuedeEliminarAsync(id))
                throw new InvalidOperationException("No se puede eliminar el estudiante porque está asignado al menos a un curso.");

            var ok = await _repo.DeleteAsync(id);

            if (!ok)
                throw new KeyNotFoundException("El estudiante no existe.");

            return true;
        }

        public async Task<bool> PuedeEliminarAsync(Guid estudianteId)
        {
            int cantidad = await _cursos.GetCursosByEstudiante(estudianteId);
            return cantidad == 0;
        }
    }
}
