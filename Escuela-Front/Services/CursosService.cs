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


        // Vinculaciones
        public Task<HttpResponseMessage> AddAsignatura(int cursoId, int asignaturaId)
        => _http.PostAsync($"api/cursos/{cursoId}/asignaturas/{asignaturaId}", null);


        public Task<HttpResponseMessage> RemoveAsignatura(int cursoId, int asignaturaId)
        => _http.DeleteAsync($"api/cursos/{cursoId}/asignaturas/{asignaturaId}");


        public Task<HttpResponseMessage> AddEstudiante(int cursoId, int estudianteId)
        => _http.PostAsync($"api/cursos/{cursoId}/estudiantes/{estudianteId}", null);


        public Task<HttpResponseMessage> RemoveEstudiante(int cursoId, int estudianteId)
        => _http.DeleteAsync($"api/cursos/{cursoId}/estudiantes/{estudianteId}");
    }
}
