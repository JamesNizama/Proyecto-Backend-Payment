using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaData _areaData;

        public AreaController(AreaData areaData)
        {
            _areaData = areaData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _areaData.ListaArea();
            return Ok(lista);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var area = await _areaData.ListarAreaPorID(id);
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            var area = await _areaData.ListarAreaPorNombre(nombre);
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Area area)
        {
            var result = await _areaData.AgregarArea(area);
            return result ? Ok(new { mensaje = "Area registrada" }) : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Area area)
        {
            var result = await _areaData.ActualizarArea(area);
            return result ? Ok(new { mensaje = "Area actualizada" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(byte id)
        {
            var result = await _areaData.EliminarArea(id);
            return result ? Ok(new { mensaje = "Area eliminada" }) : NotFound();
        }
    }
}
