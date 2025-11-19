using Escuela_Back.Interfaces;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Escuela_Test
{
    public class MockJsonDataService : IJsonDataService
    {
        private readonly ConcurrentDictionary<string, string> _storage = new();

        public Task<List<T>> LoadAsync<T>(string fileName)
        {
            if (_storage.TryGetValue(fileName, out var json))
                return Task.FromResult(JsonSerializer.Deserialize<List<T>>(json)!);

            return Task.FromResult(new List<T>());
        }

        public Task SaveAsync<T>(string fileName, List<T> data)
        {
            _storage[fileName] = JsonSerializer.Serialize(data);
            return Task.CompletedTask;
        }

        public void Seed<T>(string fileName, List<T> data)
        {
            _storage[fileName] = JsonSerializer.Serialize(data);
        }
    }
}
