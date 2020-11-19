using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using System;
using System.Collections.Generic;

namespace Axosnet.Recibos.Aplicacion.Usuarios
{
    public interface IUsuarioHelper
    {
        Respuesta<IEnumerable<Usuario>> ObtenerUsuarios();
        Respuesta<Usuario> ObtenerUsuario(Guid idUsuario);
        Respuesta<Usuario> Agregar(Usuario usuario);
        Respuesta<string> Actualizar(Usuario usuario);
        Respuesta<string> Eliminar(int idUsuario);
    }
}
