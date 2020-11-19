using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Aplicacion.Usuarios
{
    public interface IUsuarioHelper
    {
        Task<Respuesta<List<Usuario>>> ObtenerUsuarios();
        Task<Respuesta<Usuario>> ObtenerUsuario(Guid idUsuario);
        Task<Respuesta<Usuario>> Agregar(Usuario usuario);
        Task<Respuesta<Usuario>> Actualizar(Usuario usuario);
        Task<Respuesta<Usuario>> Eliminar(int idUsuario);
    }
}
