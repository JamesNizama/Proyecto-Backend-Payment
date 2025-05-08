using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly ProyectoData _proyectoData;

        public ProyectoController(ProyectoData proyectoData)
        {
            _proyectoData = proyectoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _proyectoData.ListaProyecto();
            return Ok(lista); 
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proyecto proyecto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var result = await _proyectoData.AgregarProyecto(proyecto);

            if (result > 0)
            {
                return CreatedAtAction(nameof(Lista), new { id = result }, new { mensaje = "Proyecto registrado con éxito", id_proyecto = result });
            }
            else
            {
                return BadRequest(new { mensaje = "Error al registrar el proyecto." });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Proyecto proyecto)
        {
            var result = await _proyectoData.ActualizarProyecto(proyecto);
            return result ? Ok(new { mensaje = "Proyecto actualizado" }) : NotFound();
        }

    }
}
