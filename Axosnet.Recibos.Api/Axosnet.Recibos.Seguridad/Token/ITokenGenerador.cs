using System;

namespace Axosnet.Recibos.Seguridad.Token
{
    public interface ITokenGenerador
    {
        string CrearToken(string email, Guid id);
    }
}
