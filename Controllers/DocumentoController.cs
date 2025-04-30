using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly DocumentoData _documentoData;

        public DocumentoController(DocumentoData documentoData)
        {
            _documentoData = documentoData;
        }

        // GET: api/empresa
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _documentoData.ListaDocumento();
            return Ok(lista);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var documento = await _documentoData.ListarDocumentoPorID(id);
            if (documento == null)
                return NotFound();
            return Ok(documento);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<IActionResult> ObtenerPorTitulo(string titulo)
        {
            var documento = await _documentoData.ListarDocumentoPorTitulo(titulo);
            if (documento == null)
                return NotFound();
            return Ok(documento);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Documento documento)
        {
            var result = await _documentoData.AgregarDocumento(documento);
            return result ? Ok(new { mensaje = "Documento registrado" }) : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Documento documento)
        {
            var result = await _documentoData.ActualizarDocumento(documento);
            return result ? Ok(new { mensaje = "Documento actualizada" }) : NotFound();
        }

        // DELETE: api/empresa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(byte id)
        {
            var result = await _documentoData.EliminarDocumento(id);
            return result ? Ok(new { mensaje = "Documento eliminado" }) : NotFound();
        }
    }
}
