using Escuela_Back.Interfaces;
using Escuela_Back.Models;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _repo;
    private readonly ICursoLogic _logic;

    public CursoService(ICursoRepository repo, ICursoLogic logic)
    {
        _repo = repo;
        _logic = logic;
    }

    public async Task<List<Curso>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Curso?> GetByIdAsync(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<Curso> CreateAsync(Curso model)
    {
        if (!_logic.ValidarNombre(model.Nombre))
            throw new ArgumentException("El nombre debe tener al menos 3 caracteres.");

        if (!_logic.ValidarColor(model.Color))
            throw new ArgumentException("Formato de color inválido.");

        return await _repo.AddAsync(model);
    }

    public async Task<Curso?> UpdateAsync(Guid id, Curso model)
    {
        if (!_logic.ValidarNombre(model.Nombre))
            throw new ArgumentException("Nombre inválido.");

        if (!_logic.ValidarColor(model.Color))
            throw new ArgumentException("Color inválido.");

        return await _repo.UpdateAsync(id, model);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repo.DeleteAsync(id);
    }

    public async Task<bool> VincularAsignatura(Guid cursoId, Guid asignaturaId)
    {
        return await _repo.VincularAsignatura(cursoId, asignaturaId);
    }

    public async Task<bool> DesvincularAsignatura(Guid cursoId, Guid asignaturaId)
    {
        return await _repo.DesvincularAsignatura(cursoId, asignaturaId);
    }

    public async Task<bool> VincularEstudiante(Guid cursoId, Guid estudianteId)
    {
        return await _repo.VincularEstudiante(cursoId, estudianteId);
    }

    public async Task<bool> DesvincularEstudiante(Guid cursoId, Guid estudianteId)
    {
        return await _repo.DesvincularEstudiante(cursoId, estudianteId);
    }
}
