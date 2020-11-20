using Axosnet.Recibos.Aplicacion.Acceso;
using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Axosnet.Recibos.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly IAccesoHelper _accesoHelper;


        public AccesoController(IAccesoHelper accesoHelper)
        {
            _accesoHelper = accesoHelper;
        }


        [HttpPost("Login")]
        public ActionResult<Respuesta<string>> Login([FromBody] DatosLogin datos)
        {
            return _accesoHelper.Login(datos.Email, datos.Clave);
        }
    }
}
