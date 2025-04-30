using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaActividadesPendientesController : ControllerBase
    {
        private readonly VistaActividadPendienteData _VistaActividades;

        public VistaActividadesPendientesController(VistaActividadPendienteData data)
        {
            _VistaActividades = data;
        }

        // GET: api/empresa
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _VistaActividades.ListaVistaActividadPendienteData();
            return Ok(lista);
        }
    }
}
