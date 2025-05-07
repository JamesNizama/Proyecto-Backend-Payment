using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsProyectoController : ControllerBase
    {

        private readonly ItemsProyectoData _itemsProyectoData;

        public ItemsProyectoController(ItemsProyectoData itemsProyectoData)
        {
            _itemsProyectoData = itemsProyectoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _itemsProyectoData.ListaItemsProyecto();
            return Ok(lista);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var items = await _itemsProyectoData.ListaItemsProyecto(id);
            if (items == null || items.Count == 0)
                return NotFound();
            return Ok(items);
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ItemProyecto itemProyecto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _itemsProyectoData.AgregarItemProyecto(itemProyecto);

            if (result > 0)
            {
                return CreatedAtAction(nameof(Lista), new { id = result }, new { mensaje = "ItemsProyecto registrado con éxito", id_proyecto = result });
            }
            else
            {
                return BadRequest(new { mensaje = "Error al registrar el ItemsProyecto." });
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> ActualizarItems([FromBody] Usuarios usuarios)
        //{
        //    var result = await _itemsProyectoData.UpdateItemsProyectoId(usuarios);
        //    return result ? Ok(new { mensaje = "Items actualizada" }) : NotFound();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarItemsProyecto(byte id)
        {
            var result = await _itemsProyectoData.EliminarItemsProyecto(id);
            return result ? Ok(new { mensaje = "ItemsProyecto eliminado" }) : NotFound();
        }

    }
}
