using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Model;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Aplicacion.UsuarioSesion
{
    public interface IUsuarioSesionHelper
    {
        Task<Respuesta<DatosRespuestaUsuario>> RespuestaUsuario(string token);
    }
}
