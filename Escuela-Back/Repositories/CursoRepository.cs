using Escuela_Back.Interfaces;
using Escuela_Back.Models;

namespace Escuela_Back.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private const string File = "cursos_data.json";
        private readonly IJsonDataService _json;

        public CursoRepository(IJsonDataService json) => _json = json;

        public async Task<List<Curso>> GetAllAsync()
            => await _json.LoadAsync<Curso>(File);

        public async Task<Curso?> GetByIdAsync(Guid id)
        {
            var data = await GetAllAsync();
            return data.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Curso> AddAsync(Curso curso)
        {
            var data = await GetAllAsync();

            curso.Id = Guid.NewGuid();
            data.Add(curso);

            await _json.SaveAsync(File, data);
            return curso;
        }

        public async Task<Curso?> UpdateAsync(Guid id, Curso curso)
        {
            var data = await GetAllAsync();
            var existing = data.FirstOrDefault(c => c.Id == id);

            if (existing == null)
                return null;

            existing.Nombre = curso.Nombre;
            existing.Color = curso.Color;
            existing.Icono = curso.Icono;

            await _json.SaveAsync(File, data);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await GetAllAsync();
            var curso = data.FirstOrDefault(c => c.Id == id);

            if (curso == null)
                return false;

            data.Remove(curso);
            await _json.SaveAsync(File, data);
            return true;
        }

        public async Task<bool> VincularAsignatura(Guid cursoId, Guid asignaturaId)
        {
            var data = await GetAllAsync();
            var curso = data.FirstOrDefault(c => c.Id == cursoId);

            if (curso == null)
                return false;

            if (!curso.Asignaturas.Contains(asignaturaId))
                curso.Asignaturas.Add(asignaturaId);

            await _json.SaveAsync(File, data);
            return true;
        }

        public async Task<bool> DesvincularAsignatura(Guid cursoId, Guid asignaturaId)
        {
            var data = await GetAllAsync();
            var curso = data.FirstOrDefault(c => c.Id == cursoId);

            if (curso == null)
                return false;

            curso.Asignaturas.Remove(asignaturaId);
            await _json.SaveAsync(File, data);
            return true;
        }

        public async Task<bool> VincularEstudiante(Guid cursoId, Guid estudianteId)
        {
            var data = await GetAllAsync();
            var curso = data.FirstOrDefault(c => c.Id == cursoId);

            if (curso == null)
                return false;

            if (!curso.Estudiantes.Contains(estudianteId))
                curso.Estudiantes.Add(estudianteId);

            await _json.SaveAsync(File, data);
            return true;
        }

        public async Task<bool> DesvincularEstudiante(Guid cursoId, Guid estudianteId)
        {
            var data = await GetAllAsync();
            var curso = data.FirstOrDefault(c => c.Id == cursoId);

            if (curso == null)
                return false;

            curso.Estudiantes.Remove(estudianteId);
            await _json.SaveAsync(File, data);
            return true;
        }

        public async Task<int> GetCursosByEstudiante(Guid estudianteId)
        {
            var data = await GetAllAsync();
            return data.Count(c => c.Estudiantes.Contains(estudianteId));
        }

        public async Task<int> GetCursosByAsignatura(Guid asignaturaId)
        {
            var data = await GetAllAsync();
            return data.Count(c => c.Asignaturas.Contains(asignaturaId));
        }
    }
}
