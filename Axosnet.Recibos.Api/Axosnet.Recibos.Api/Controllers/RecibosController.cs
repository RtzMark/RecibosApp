using Axosnet.Recibos.Aplicacion.Recibos;
using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecibosController : ControllerBase
    {
        private readonly IReciboHelper _recibo;


        public RecibosController(IReciboHelper recibo)
        {
            _recibo = recibo;
        }


        [HttpGet]
        public async Task<ActionResult<Respuesta<List<Recibo>>>> Get()
        {
            return await _recibo.ObtenerRecibos();
        }

        [HttpGet("{id}", Name = "obtenerRecibos")]
        public async Task<ActionResult<Respuesta<Recibo>>> Get(int id)
        {
            return await _recibo.ObtenerRecibo(id);
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta<Recibo>>> Post([FromBody] Recibo recibo)
        {
            return await _recibo.Agregar(recibo);
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta<Recibo>>> Put([FromBody] Recibo recibo)
        {
            return await _recibo.Actualizar(recibo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta<Recibo>>> Delete(int id)
        {
            return await _recibo.Eliminar(id);
        }
    }
}
