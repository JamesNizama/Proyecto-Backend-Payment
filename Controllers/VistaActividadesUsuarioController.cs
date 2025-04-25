using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaActividadesUsuarioController : ControllerBase
    {
        private readonly VistaActividadUsuarioData _VistaActividades;

        public VistaActividadesUsuarioController(VistaActividadUsuarioData data)
        {
            _VistaActividades = data;
        }

        // GET: api/empresa
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _VistaActividades.ListaVistaActividadUsuarioData();
            return Ok(lista);
        }
    }
}
