using Escuela_Back.Interfaces;
using Escuela_Back.Models;

namespace Escuela_Back.Services
{
    public class AsignaturaService : IAsignaturaService
    {
        private readonly IAsignaturaRepository _repo;
        private readonly ICursoRepository _cursos;
        public AsignaturaService(IAsignaturaRepository repo, ICursoRepository cursos)
        {
            _repo = repo;
            _cursos = cursos;
        }

        public Task<List<Asignatura>> GetAllAsync() =>
            _repo.GetAllAsync();

        public Task<Asignatura?> GetByIdAsync(Guid id) =>
            _repo.GetByIdAsync(id);

        public async Task<Asignatura> AddAsync(Asignatura model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre) || model.Nombre.Length < 3)
                throw new InvalidOperationException("El nombre debe tener al menos 3 caracteres.");

            return await _repo.AddAsync(model);
        }

        public async Task<Asignatura> UpdateAsync(Guid id, Asignatura model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                throw new InvalidOperationException("El nombre es obligatorio.");

            var updated = await _repo.UpdateAsync(id, model);

            if (updated == null)
                throw new KeyNotFoundException("Asignatura no encontrada.");

            return updated;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {

            if (!await PuedeEliminarAsync(id))
                throw new InvalidOperationException("No se puede eliminar la asignatura porque está asignado al menos a un curso.");


            var ok = await _repo.DeleteAsync(id);

            if (!ok)
                throw new KeyNotFoundException("El asignatura no existe.");

            return true;
        }

        
        public async Task<bool> PuedeEliminarAsync(Guid asignaturaId)
        {
            int cantidad = await _cursos.GetCursosByAsignatura(asignaturaId);
            return cantidad == 0;
        }       
    }
}
