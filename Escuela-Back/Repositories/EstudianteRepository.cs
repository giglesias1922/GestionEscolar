using Escuela_Back.Interfaces;
using Escuela_Back.Models;

namespace Escuela_Back.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private const string File = "estudiantes_data.json";
        private readonly IJsonDataService _json;

        public EstudianteRepository(IJsonDataService json) => _json = json;

        public async Task<List<Estudiante>> GetAllAsync() => await _json.LoadAsync<Estudiante>(File);

        public async Task<Estudiante?> GetByIdAsync(Guid id)
        {
            var data = await GetAllAsync();
            return data.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Estudiante> AddAsync(Estudiante estudiante)
        {
            var data = await GetAllAsync();

            estudiante.Id = Guid.NewGuid();

            data.Add(estudiante);

            await _json.SaveAsync(File, data);

            return estudiante;
        }

        public async Task<Estudiante?> UpdateAsync(Guid id, Estudiante estudiante)
        {
            var data = await GetAllAsync();
            var existing = data.FirstOrDefault(e => e.Id == id);

            if (existing == null) return null;

            existing.NombreCompleto = estudiante.NombreCompleto;
            existing.Edad = estudiante.Edad;

            await _json.SaveAsync(File, data);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await GetAllAsync();
            var item = data.FirstOrDefault(e => e.Id == id);

            if (item == null) return false;

            data.Remove(item);
            await _json.SaveAsync(File, data);
            return true;
        }
    }
}
