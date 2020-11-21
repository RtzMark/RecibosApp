using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Model;
using Axosnet.Recibos.Seguridad.Token;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Aplicacion.UsuarioSesion
{
    public class UsuarioSesionHelper : IUsuarioSesionHelper
    {
        private readonly ITokenGenerador _tokenGenerador;


        public UsuarioSesionHelper(ITokenGenerador tokenGenerador)
        {
            _tokenGenerador = tokenGenerador;
        }


        public async Task<Respuesta<DatosRespuestaUsuario>> RespuestaUsuario(string token)
        {
            var respuesta = new Respuesta<DatosRespuestaUsuario>();

            try
            {
                String[] tokenInfo = token.Split(' ');

                var datosRespuestaUsuario = await Task.Run(() =>
                {
                    return _tokenGenerador.ObtenerDatosToken(tokenInfo[1]);
                });

                respuesta.mensaje = "Datos obtenidos correctamente";

                if (String.IsNullOrEmpty(datosRespuestaUsuario.Email) || String.IsNullOrEmpty(datosRespuestaUsuario.Nombre))
                {
                    respuesta.error = true;
                    respuesta.mensaje = "El usuario no tiene datos para mostrar";
                }

                respuesta.datos = datosRespuestaUsuario;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al obtener datos de usuario // {ex.Message}");
            }

            return respuesta;
        }
    }
}
