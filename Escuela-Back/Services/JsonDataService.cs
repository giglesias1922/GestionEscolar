using Escuela_Back.Interfaces;
using System.Text.Json;

namespace Escuela_Back.Services
{
    public class JsonDataService : IJsonDataService
    {
        private readonly string _root;

        public JsonDataService(IWebHostEnvironment env)
        {
            _root = Path.Combine(env.ContentRootPath, "Data");
        }

        public async Task<List<T>> LoadAsync<T>(string fileName)
        {
            var path = Path.Combine(_root, fileName);

            if (!File.Exists(path))
                return new List<T>();

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public async Task SaveAsync<T>(string fileName, List<T> data)
        {
            var path = Path.Combine(_root, fileName);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(path, json);
        }
    }
}
