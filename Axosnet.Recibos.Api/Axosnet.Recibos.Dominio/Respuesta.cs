namespace Axosnet.Recibos.Dominio
{
    /// <summary>
    /// Se utiliza como respuesta en todas las peticiones realizadas a la api.
    /// </summary>
    /// <typeparam name="T">Informacion que se regresara al usuario (Listas, int, texto, etc)</typeparam>
    public class Respuesta<T>
    {
        public bool error { get; set; }
        public string mensaje { get; set; }
        public T datos { get; set; }


        public Respuesta()
        {
            error = false;
        }
    }
}
