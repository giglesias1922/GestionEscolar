using Escuela_Back.Interfaces;
using Escuela_Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace Escuela_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsignaturasController : ControllerBase
    {
        private readonly IAsignaturaService _service;
        private readonly ILogger<AsignaturasController> _logger;

        public AsignaturasController(IAsignaturaService service, ILogger<AsignaturasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Asignatura model)
        {
            try
            {
                var created = await _service.AddAsync(model);
                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Asignatura model)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, model);
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la asignatura.");
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
                _logger.LogError(ex, "Error al eliminar la asignatura.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}