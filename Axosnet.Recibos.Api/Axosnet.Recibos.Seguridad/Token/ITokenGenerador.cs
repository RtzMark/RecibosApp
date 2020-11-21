using Axosnet.Recibos.Dominio.Model;
using System;

namespace Axosnet.Recibos.Seguridad.Token
{
    public interface ITokenGenerador
    {
        string CrearToken(string email, string nombre, Guid id);

        DatosRespuestaUsuario ObtenerDatosToken(string token);
    }
}
