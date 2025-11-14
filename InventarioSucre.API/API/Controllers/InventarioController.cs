using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase, IInventarioController
    {
        private readonly IInventarioFlujo _inventarioFlujo;
        private readonly ILogger<InventarioController> _logger;

        public InventarioController(IInventarioFlujo inventarioFlujo, ILogger<InventarioController> logger)
        {
            _inventarioFlujo = inventarioFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] InventarioRequest inventario)
        {
            var resultado = await _inventarioFlujo.Agregar(inventario);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] InventarioRequest inventario)
        {
            if (!await VerificarInventarioExiste(Id))
                return NotFound("El inventario no existe");

            var resultado = await _inventarioFlujo.Editar(Id, inventario);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarInventarioExiste(Id))
                return NotFound("El inventario no existe");

            var resultado = await _inventarioFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _inventarioFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _inventarioFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound("El inventario no existe");

            return Ok(resultado);
        }

        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarInventarioExiste(Guid Id)
        {
            var resultadoInventarioExiste = await _inventarioFlujo.Obtener(Id);
            return resultadoInventarioExiste != null;
        }

        #endregion Helpers
    }
}