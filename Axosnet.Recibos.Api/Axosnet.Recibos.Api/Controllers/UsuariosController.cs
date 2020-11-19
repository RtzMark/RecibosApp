using Axosnet.Recibos.Aplicacion.Usuarios;
using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioHelper _usuario;


        public UsuariosController(IUsuarioHelper usuario)
        {
            _usuario = usuario;
        }


        [HttpGet]
        public async Task<ActionResult<Respuesta<List<Usuario>>>> Get()
        {
            return await _usuario.ObtenerUsuarios();
        }

        [HttpGet("{id}", Name = "obtenerUsuario")]
        public async Task<ActionResult<Respuesta<Usuario>>> Get(Guid id)
        {
            return await _usuario.ObtenerUsuario(id);
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta<Usuario>>> Post([FromBody] Usuario usuario)
        {
            return await _usuario.Agregar(usuario);
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta<Usuario>>> Put([FromBody] Usuario usuario)
        {
            return await _usuario.Actualizar(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta<Usuario>>> Delete(Guid id)
        {
            return await _usuario.Eliminar(id);
        }
    }
}
