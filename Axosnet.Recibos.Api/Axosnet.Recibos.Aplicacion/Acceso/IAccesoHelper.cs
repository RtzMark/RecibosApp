using Axosnet.Recibos.Dominio;

namespace Axosnet.Recibos.Aplicacion.Acceso
{
    public interface IAccesoHelper
    {
        Respuesta<string> Login(string email, string clave);
    }
}
