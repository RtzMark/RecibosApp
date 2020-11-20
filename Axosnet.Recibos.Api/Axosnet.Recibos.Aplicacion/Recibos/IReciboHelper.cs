using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Aplicacion.Recibos
{
    public interface IReciboHelper
    {
        Task<Respuesta<List<Recibo>>> ObtenerRecibos();
        Task<Respuesta<Recibo>> ObtenerRecibo(int idRecibo);
        Task<Respuesta<Recibo>> Agregar(Recibo recibo);
        Task<Respuesta<Recibo>> Actualizar(Recibo recibo);
        Task<Respuesta<Recibo>> Eliminar(int idRecibo);
    }
}
