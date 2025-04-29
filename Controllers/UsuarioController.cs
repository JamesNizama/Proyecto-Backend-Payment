using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioData _UsuarioData;

        public UsuarioController(UsuarioData data)
        {
            _UsuarioData = data;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _UsuarioData.ListaUsuario();
            return Ok(lista);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuarios = await _UsuarioData.ListarUsuarioPorID(id);
            if (usuarios == null)
                return NotFound();
            return Ok(usuarios);
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombre)
        {
            var usuarios = await _UsuarioData.ListarUsuarioPorNombre(nombre);
            if (usuarios == null)
                return NotFound();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Usuarios usuarios)
        {
            var result = await _UsuarioData.AgregarUsuario(usuarios);
            return result ? Ok(new { mensaje = "Usuario registrada" }) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Usuarios usuarios)
        {
            var result = await _UsuarioData.ActualizarUsuario(usuarios);
            return result ? Ok(new { mensaje = "Usuario actualizada" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(byte id)
        {
            var result = await _UsuarioData.EliminarUsuarios(id);
            return result ? Ok(new { mensaje = "Usuario eliminado" }) : NotFound();
        }
    }
}
