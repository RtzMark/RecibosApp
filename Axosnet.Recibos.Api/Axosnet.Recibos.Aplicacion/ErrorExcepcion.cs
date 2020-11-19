using System;
using System.Net;

namespace Axosnet.Recibos.Aplicacion
{
    public class ErrorExcepcion : Exception
    {
        public HttpStatusCode Codigo { get; }
        public string Error { get; }
        public ErrorExcepcion(HttpStatusCode codigo, string error)
        {
            Codigo = codigo;
            Error = error;
        }
    }
}
