using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Model;

namespace Axosnet.Recibos.Aplicacion.Acceso
{
    public interface IAccesoHelper
    {
        Respuesta<DatosRespuestaLogin> Login(string email, string clave);
    }
}
