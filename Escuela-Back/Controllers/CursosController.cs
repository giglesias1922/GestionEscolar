using Escuela_Back.Controllers;
using Escuela_Back.Interfaces;
using Escuela_Back.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _service;
    private readonly ILogger<CursosController> _logger;

    public CursosController(ICursoService service, ILogger<CursosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var curso = await _service.GetByIdAsync(id);
        return curso == null ? NotFound() : Ok(curso);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Curso model)
    {
        try
        {
            var created = await _service.CreateAsync(model);
            return Ok(created);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error al obtener las asignaturas.");
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al crear el curso.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, Curso model)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, model);
            return updated == null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error al obtener las asignaturas.");
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al actualizar el curso.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al eliminar el curso.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }


    [HttpPost("{id}/asignaturas/{asignaturaId}")]
    public async Task<IActionResult> VincularAsignatura(Guid id, Guid asignaturaId)
    {
        try
        {
            return await _service.VincularAsignatura(id, asignaturaId)? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al vincular la asignatura.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }


    [HttpDelete("{id}/asignaturas/{asignaturaId}")]
    public async Task<IActionResult> DesvincularAsignatura(Guid id, Guid asignaturaId)
    {
        try
        {
            return await _service.DesvincularAsignatura(id, asignaturaId) ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al desvincular la asignatura.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }


    [HttpPost("{id}/estudiantes/{estudianteId}")]
    public async Task<IActionResult> VincularEstudiante(Guid id, Guid estudianteId)
    {
        try
        {
            return await _service.VincularEstudiante(id, estudianteId) ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al vincular el estudiante.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    [HttpDelete("{id}/estudiantes/{estudianteId}")]
    public async Task<IActionResult> DesvincularEstudiante(Guid id, Guid estudianteId)
    {
        try
        {
            return await _service.DesvincularEstudiante(id, estudianteId) ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al desvincular el estudiante.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }
}
