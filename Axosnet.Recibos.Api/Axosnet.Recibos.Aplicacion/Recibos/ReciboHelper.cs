using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using Axosnet.Recibos.Dominio.Validadores;
using Axosnet.Recibos.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Aplicacion.Recibos
{
    public class ReciboHelper : IReciboHelper
    {
        private readonly RecibosContext _context;
        private readonly ReciboValidador _validador;


        public ReciboHelper(RecibosContext context)
        {
            _context = context;
            _validador = new ReciboValidador();
        }


        public async Task<Respuesta<List<Recibo>>> ObtenerRecibos()
        {
            var respuesta = new Respuesta<List<Recibo>>();

            respuesta.datos = await _context.Recibos.Where(x => x.Activo).ToListAsync();
            respuesta.mensaje = "Recibos obtenidos correctamente";

            return respuesta;
        }

        public async Task<Respuesta<Recibo>> ObtenerRecibo(int idRecibo)
        {
            var respuesta = new Respuesta<Recibo>();

            var recibo = await _context.Recibos
                .Where(x => x.Id == idRecibo && x.Activo)
                .FirstOrDefaultAsync();

            if (recibo == null)
                throw new ErrorExcepcion(HttpStatusCode.NotFound, "Recibo no encontrado");

            respuesta.datos = recibo;
            respuesta.mensaje = "Recibo encontrado";

            return respuesta;
        }

        public async Task<Respuesta<Recibo>> Agregar(Recibo recibo)
        {
            var respuesta = new Respuesta<Recibo>();

            if (!_validador.Validate(recibo).IsValid)
                throw new ErrorExcepcion(HttpStatusCode.BadRequest, "Favor de validar los campos");

            try
            {
                recibo.Activo = true;

                await _context.AddAsync(recibo);
                await _context.SaveChangesAsync();

                respuesta.mensaje = "Recibo agregado correctamente";
                respuesta.datos = recibo;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al agregar recibo // {ex.Message}");
            }


            return respuesta;
        }

        public async Task<Respuesta<Recibo>> Actualizar(Recibo recibo)
        {
            var respuesta = new Respuesta<Recibo>();

            if (!_validador.Validate(recibo).IsValid)
                throw new ErrorExcepcion(HttpStatusCode.BadRequest, "Favor de validar los campos");

            try
            {
                var existeRecibo = await _context.Recibos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == recibo.Id);

                if (existeRecibo == null)
                    throw new ErrorExcepcion(HttpStatusCode.NotFound, "No existe el recibo que desea actualizar");

                _context.Entry(recibo).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                respuesta.mensaje = "Recibo actualizado correctamente";
                respuesta.datos = recibo;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al actualizar recibo // {ex.Message}");
            }


            return respuesta;
        }

        public async Task<Respuesta<Recibo>> Eliminar(int idRecibo)
        {
            var respuesta = new Respuesta<Recibo>();

            var existeRecibo = await _context.Recibos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == idRecibo && x.Activo);

            if (existeRecibo == null)
                throw new ErrorExcepcion(HttpStatusCode.NotFound, "No existe el recibo que desea eliminar");

            try
            {
                existeRecibo.Activo = false;
                _context.Entry(existeRecibo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                respuesta.mensaje = "Recibo eliminado correctamente";
                respuesta.datos = existeRecibo;
            }
            catch (Exception ex)
            {
                throw new ErrorExcepcion(HttpStatusCode.InternalServerError, $"Error al eliminar usuario // {ex.Message}");
            }


            return respuesta;
        }        
    }
}
