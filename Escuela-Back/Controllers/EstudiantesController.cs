using Escuela_Back.Interfaces;
using Escuela_Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Escuela_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _service;
        private readonly ILogger<EstudiantesController> _logger;

        public EstudiantesController(IEstudianteService service, ILogger<EstudiantesController> logger)
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
            var e = await _service.GetByIdAsync(id);
            return e == null ? NotFound() : Ok(e);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Estudiante model)
        {
            try
            {
                var created = await _service.AddAsync(model);
                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el estudiante.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Estudiante model)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, model);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el estudiante.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException) 

            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el estudiante.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }


    }
}
