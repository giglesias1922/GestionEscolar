namespace Escuela_Back.Interfaces
{
    public interface IJsonDataService
    {
        Task<List<T>> LoadAsync<T>(string fileName);
        Task SaveAsync<T>(string fileName, List<T> data);
    }
}
