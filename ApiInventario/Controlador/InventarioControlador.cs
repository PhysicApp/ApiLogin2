using ApiInventario.Modelo;
using ApiInventario.Servicio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiInventario.Controlador
{
    [Route("apiInventario/[controller]")]
    [ApiController]
    public class InventarioControlador : ControllerBase
    {
        private readonly IInventarioServicio _inventarioServicio;

        public InventarioControlador(IInventarioServicio inventarioServicio)
        {
            _inventarioServicio = inventarioServicio;
        }

        [HttpGet("ConsultarInventario")]
        public async Task<IActionResult> ConsultarInventario()
        {
            var resultado = await _inventarioServicio.ConsultarInventario();
            return Ok(resultado);
        }

        [HttpGet("ConsultarInventarioId/{idInventario}")]
        public async Task<IActionResult> ConsultarInventarioId(int idInventario)
        {
            var resultado = await _inventarioServicio.ConsultarInventarioPorId(idInventario);
            return Ok(resultado);
        }

        [HttpPost("AgregarProducto")]
        public async Task<IActionResult> AgregarProducto(Inventario inventario)
        {
            var resultado = await _inventarioServicio.CrearInventario(inventario);
            return Ok(resultado);
        }

        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(Inventario inventario)
        {
            var resultado = await _inventarioServicio.ActualizarInventario(inventario);
            return Ok(resultado);
        }

        [HttpDelete("EliminarProducto/{idInventario}")]
        public async Task<IActionResult> EliminarProducto(int idInventario)
        {
            var resultado = await _inventarioServicio.EliminarInventario(idInventario);
            return Ok(resultado);
        }
    }
}
