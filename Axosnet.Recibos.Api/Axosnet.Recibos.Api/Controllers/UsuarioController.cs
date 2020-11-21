using Axosnet.Recibos.Aplicacion.UsuarioSesion;
using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioSesionHelper _usuarioSesionHelper;


        public UsuarioController(IUsuarioSesionHelper usuarioSesionHelper)
        {
            _usuarioSesionHelper = usuarioSesionHelper;
        }


        [HttpGet]
        public async Task<ActionResult<Respuesta<DatosRespuestaUsuario>>> Get()
        {
            var token = Request.Headers[HeaderNames.Authorization];

            return await _usuarioSesionHelper.RespuestaUsuario(token.ToString());
        }

    }
}
