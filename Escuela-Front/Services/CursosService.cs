using Escuela_Front.Models;
using System.Net.Http.Json;

namespace Escuela_Front.Services
{
    public class CursosService
    {
        private readonly HttpClient _http;


        public CursosService(HttpClient http)
        {
            _http = http;
        }


        public Task<List<Curso>?> GetAllAsync() => _http.GetFromJsonAsync<List<Curso>>("api/cursos");
        public Task<Curso?> GetAsync(Guid id) => _http.GetFromJsonAsync<Curso>($"api/cursos/{id}");
        public async Task<bool> CreateAsync(Curso dto)
        {
            var r = await _http.PostAsJsonAsync("api/cursos", dto);
            return r.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAsync(Guid id, Curso dto)
        {
            var r = await _http.PutAsJsonAsync($"api/cursos/{id}", dto);
            return r.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var r = await _http.DeleteAsync($"api/cursos/{id}");
            return r.IsSuccessStatusCode;
        }

        public async Task<bool> AddAsignatura(Guid cursoId, Guid asignaturaId)
        {
            var response = await _http.PostAsync($"api/cursos/{cursoId}/asignaturas/{asignaturaId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsignatura(Guid cursoId, Guid asignaturaId)
        {
            var response = await _http.DeleteAsync($"api/cursos/{cursoId}/asignaturas/{asignaturaId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddEstudiante(Guid cursoId, Guid estudianteId)
        {
            var response = await _http.PostAsync($"api/cursos/{cursoId}/estudiantes/{estudianteId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveEstudiante(Guid cursoId, Guid estudianteId)
        {
            var response = await _http.DeleteAsync($"api/cursos/{cursoId}/estudiantes/{estudianteId}");
            return response.IsSuccessStatusCode;
        }
    }
}
