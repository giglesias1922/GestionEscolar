using Escuela_Back.Interfaces;
using Escuela_Back.Models;

namespace Escuela_Back.Repositories
{
    public class AsignaturaRepository : IAsignaturaRepository
    {
        private const string File = "asignaturas_data.json";
        private readonly IJsonDataService _json;

        public AsignaturaRepository(IJsonDataService json)
        {
            _json = json;
        }

        public async Task<List<Asignatura>> GetAllAsync() =>
            await _json.LoadAsync<Asignatura>(File);

        public async Task<Asignatura?> GetByIdAsync(Guid id)
        {
            var data = await GetAllAsync();
            return data.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Asignatura> AddAsync(Asignatura asignatura)
        {
            var data = await GetAllAsync();

            asignatura.Id = Guid.NewGuid();
            data.Add(asignatura);

            await _json.SaveAsync(File, data);
            return asignatura;
        }

        public async Task<Asignatura?> UpdateAsync(Guid id, Asignatura asignatura)
        {
            var data = await GetAllAsync();
            var existing = data.FirstOrDefault(x => x.Id == id);

            if (existing == null) return null;

            existing.Nombre = asignatura.Nombre;
            existing.Descripcion = asignatura.Descripcion;

            await _json.SaveAsync(File, data);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await GetAllAsync();
            var item = data.FirstOrDefault(x => x.Id == id);

            if (item == null) return false;

            data.Remove(item);
            await _json.SaveAsync(File, data);
            return true;
        }
    }
}
