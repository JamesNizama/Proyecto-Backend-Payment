using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly ActividadData _actividadData;

        public ActividadController(ActividadData actividadData)
        {
            _actividadData = actividadData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _actividadData.ListaActividad();
            return Ok(lista);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var actividad = await _actividadData.ListarActividadPorID(id);
            if (actividad == null)
                return NotFound();
            return Ok(actividad);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<IActionResult> ObtenerPorTitulo(string titulo)
        {
            var actividad = await _actividadData.ListarActividadPorTitulo(titulo);
            if (actividad == null)
                return NotFound();
            return Ok(actividad);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Actividad actividad)
        {
            var result = await _actividadData.AgregarActividad(actividad);
            return result ? Ok(new { mensaje = "Empresa registrada" }) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Actividad actividad)
        {
            var result = await _actividadData.ActualizarActividad(actividad);
            return result ? Ok(new { mensaje = "Actividad actualizada" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(byte id)
        {
            var result = await _actividadData.EliminarActividad(id);
            return result ? Ok(new { mensaje = "Actividad eliminada" }) : NotFound();
        }
    }
}
