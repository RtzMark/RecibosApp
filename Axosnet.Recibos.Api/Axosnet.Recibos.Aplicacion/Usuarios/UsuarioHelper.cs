using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using Axosnet.Recibos.Dominio.Validadores;
using Axosnet.Recibos.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Aplicacion.Usuarios
{
    public class UsuarioHelper : IUsuarioHelper
    {
        private readonly RecibosContext _context;
        private readonly UsuarioValidador _validador;


        public UsuarioHelper(RecibosContext context)
        {
            _context = context;
            _validador = new UsuarioValidador();
        }


        public async Task<Respuesta<List<Usuario>>> ObtenerUsuarios()
        {
            var respuesta = new Respuesta<List<Usuario>>();

            respuesta.datos = await _context.Usuarios.ToListAsync();
            respuesta.mensaje = "Usuarios obtenidos correctamente";

            return respuesta;
        }

        public async Task<Respuesta<Usuario>> ObtenerUsuario(Guid idUsuario)
        {
            var respuesta = new Respuesta<Usuario>();

            var usuario = await _context.Usuarios
                .Where(x => x.Id == idUsuario && x.Activo)
                .Select(x => new Usuario { Id = x.Id, Email = x.Email, Nombre = x.Nombre })
                .FirstOrDefaultAsync();

            if (usuario == null)
                throw new ErrorExcepcion(HttpStatusCode.NotFound, "Usuario no encontrado");

            respuesta.datos = usuario;
            respuesta.mensaje = "Usuarios encontrado";

            return respuesta;
        }

        public async Task<Respuesta<Usuario>> Agregar(Usuario usuario)
        {
            var respuesta = new Respuesta<Usuario>();

            if (!_validador.Validate(usuario).IsValid)
                throw new ErrorExcepcion(HttpStatusCode.BadRequest, "Favor de validar los campos");

            try
            {
                var existeUsuario = await _context.Usuarios.FindAsync(usuario.Email);

                if (existeUsuario != null)
                {
                    respuesta.error = true;
                    respuesta.mensaje = "Ya existe email";
                }

                await _context.AddAsync(usuario);

                respuesta.mensaje = "Usuario agregado correctamente";
                respuesta.datos = existeUsuario;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al agregar usuario // {ex.Message}");
            }


            return respuesta;
        }

        public async Task<Respuesta<Usuario>> Actualizar(Usuario usuario)
        {
            var respuesta = new Respuesta<Usuario>();

            if (!_validador.Validate(usuario).IsValid)
                throw new ErrorExcepcion(HttpStatusCode.BadRequest, "Favor de validar los campos");

            try
            {
                var existeUsuario = await _context.Usuarios.FindAsync(usuario.Id);

                if (existeUsuario == null)
                    throw new ErrorExcepcion(HttpStatusCode.NotFound, "No existe el usuario que desea actualizar");

                var existeUsuarioEmail = await _context.Usuarios.FindAsync(usuario.Email);

                if (existeUsuarioEmail != null)
                {
                    respuesta.error = true;
                    respuesta.mensaje = "Ya existe email";
                }

                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                respuesta.mensaje = "Usuario actualizado correctamente";
                respuesta.datos = existeUsuario;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al actualizar usuario // {ex.Message}");
            }


            return respuesta;
        }

        public async Task<Respuesta<Usuario>> Eliminar(int idUsuario)
        {
            var respuesta = new Respuesta<Usuario>();

            try
            {
                var existeUsuario = await _context.Usuarios.FindAsync(idUsuario);

                if (existeUsuario == null)
                    throw new ErrorExcepcion(HttpStatusCode.NotFound, "No existe el usuario que desea eliminar");

                existeUsuario.Activo = false;
                _context.Entry(existeUsuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                respuesta.mensaje = "Usuario eliminado correctamente";
                respuesta.datos = existeUsuario;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al eliminar usuario // {ex.Message}");
            }


            return respuesta;
        }
    }
}
