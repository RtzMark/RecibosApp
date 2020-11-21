using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Model;
using Axosnet.Recibos.Persistencia;
using Axosnet.Recibos.Seguridad.Crypt;
using Axosnet.Recibos.Seguridad.Token;
using System;
using System.Linq;

namespace Axosnet.Recibos.Aplicacion.Acceso
{
    public class AccesoHelper : IAccesoHelper
    {
        private readonly RecibosContext _context;
        private readonly ITokenGenerador _tokenGenerador;


        public AccesoHelper(RecibosContext context, ITokenGenerador tokenGenerador)
        {
            _context = context;
            _tokenGenerador = tokenGenerador;
        }


        public Respuesta<DatosRespuestaLogin> Login(string email, string clave)
        {
            var respuesta = new Respuesta<DatosRespuestaLogin>();

            clave = Cifrado.EncryptSHA256(clave);

            var usuario = _context.Usuarios.Where(x => x.Email == email && x.Clave == clave).FirstOrDefault();

            if (usuario.Id == null || usuario.Id == new Guid())
            {
                respuesta.mensaje = "Email o contraseña incorrectos";
                respuesta.error = true;
                return respuesta;
            }

            respuesta.datos = new DatosRespuestaLogin
            {
                Token = _tokenGenerador.CrearToken(usuario.Email, usuario.Nombre, usuario.Id),
                Usuario = new DatosRespuestaUsuario
                {
                    Email = usuario.Email,
                    Nombre = usuario.Nombre
                }
            };
            respuesta.mensaje = "Usuario logeado correctamente";

            return respuesta;
        }
    }
}
