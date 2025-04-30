using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaData _empresaData;

        public EmpresaController(EmpresaData empresaData)
        {
            _empresaData = empresaData;
        }

        // GET: api/empresa
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _empresaData.ListaEmpresa();
            return Ok(lista);
        }

        // GET: api/empresa/id/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var empresa = await _empresaData.ListarEmpresaPorID(id);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        // GET: api/empresa/nombre/empresaX
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            var empresa = await _empresaData.ListarEmpresaPorNombre(nombre);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        // POST: api/empresa
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Empresa empresa)
        {
            var result = await _empresaData.AgregarEmpresa(empresa);
            return result ? Ok(new { mensaje = "Empresa registrada" }) : BadRequest();
        }

        // PUT: api/empresa
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Empresa empresa)
        {
            var result = await _empresaData.ActualizarEmpresa(empresa);
            return result ? Ok(new { mensaje = "Empresa actualizada" }) : NotFound();
        }

        // DELETE: api/empresa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(byte id)
        {
            var result = await _empresaData.EliminarEmpresa(id);
            return result ? Ok(new { mensaje = "Empresa eliminada" }) : NotFound();
        }

    }
}
