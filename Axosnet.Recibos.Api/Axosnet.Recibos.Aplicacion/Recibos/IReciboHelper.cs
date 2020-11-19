using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using System;
using System.Collections.Generic;

namespace Axosnet.Recibos.Aplicacion.Recibos
{
    public interface IReciboHelper
    {
        Respuesta<IEnumerable<Recibo>> ObtenerRecibos();
        Respuesta<Recibo> ObtenerRecibo(Guid idRecibo);
        Respuesta<Recibo> Agregar(Recibo recibo);
        Respuesta<string> Actualizar(Recibo recibo);
        Respuesta<string> Eliminar(Guid idRecibo);
    }
}
