using Axosnet.Recibos.Aplicacion.Acceso;
using Axosnet.Recibos.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Axosnet.Recibos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly IAccesoHelper _accesoHelper;


        public AccesoController(IAccesoHelper accesoHelper)
        {
            _accesoHelper = accesoHelper;
        }


        [HttpPost("Login")]
        public ActionResult<Respuesta<string>> Login([FromBody] string email, [FromBody] string clave)
        {
            return _accesoHelper.Login(email, clave);
        }
    }
}
