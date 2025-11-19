using Escuela_Front.Models;
using System.Net.Http.Json;

namespace Escuela_Front.Services
{
    
    public class EstudiantesService
    {
        private readonly HttpClient _http;


        public EstudiantesService(HttpClient http)
        {
            _http = http;
        }


        public Task<List<Estudiante>?> GetAllAsync() => _http.GetFromJsonAsync<List<Estudiante>>("api/estudiantes");
        public Task<Estudiante?> GetAsync(Guid id) => _http.GetFromJsonAsync<Estudiante>($"api/estudiantes/{id}");
        public async Task<bool> CreateAsync(Estudiante dto)
        {
            var r = await _http.PostAsJsonAsync("api/estudiantes", dto);
            return r.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAsync(Guid id, Estudiante dto)
        {
            var r = await _http.PutAsJsonAsync($"api/estudiantes/{id}", dto);
            return r.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var r = await _http.DeleteAsync($"api/estudiantes/{id}");
            return r.IsSuccessStatusCode;
        }
    }
}
