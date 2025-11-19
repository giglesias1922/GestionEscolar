using Escuela_Front.Models;
using System.Net.Http.Json;

namespace Escuela_Front.Services
{
    public class AsignaturasService
    {
        private readonly HttpClient _http;


        public AsignaturasService(HttpClient http)
        {
            _http = http;
        }


        public Task<List<Asignatura>?> GetAllAsync() => _http.GetFromJsonAsync<List<Asignatura>>("api/asignaturas");
        public Task<Asignatura?> GetAsync(Guid id) => _http.GetFromJsonAsync<Asignatura>($"api/asignaturas/{id}");
        public async Task<bool> CreateAsync(Asignatura dto)
        {
            var r = await _http.PostAsJsonAsync("api/asignaturas", dto);
            return r.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAsync(Guid id, Asignatura dto)
        {
            var r = await _http.PutAsJsonAsync($"api/asignaturas/{id}", dto);
            return r.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/asignaturas/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
            else
            {
                return true;
            }
        }
    }
}
