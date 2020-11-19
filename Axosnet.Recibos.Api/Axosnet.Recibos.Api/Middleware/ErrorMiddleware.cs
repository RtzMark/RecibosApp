using Axosnet.Recibos.Aplicacion;
using Axosnet.Recibos.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Api.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;


        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ErrorMiddlewareAsync(context, ex, _logger);
            }
        }

        private async Task ErrorMiddlewareAsync(HttpContext context, Exception ex, ILogger<ErrorMiddleware> logger)
        {
            var respuesta = new Respuesta<string>() { error = true, datos = "" };

            switch (ex)
            {
                case ErrorExcepcion me:
                    logger.LogError(ex, "Error de Proceso");
                    respuesta.mensaje = me.Error;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    respuesta.mensaje = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            var resultados = JsonConvert.SerializeObject(respuesta);
            await context.Response.WriteAsync(resultados);
        }
    }
}
