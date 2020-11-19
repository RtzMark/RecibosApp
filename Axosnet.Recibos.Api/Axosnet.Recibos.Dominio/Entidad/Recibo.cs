using System;

namespace Axosnet.Recibos.Dominio.Entidad
{
    public class Recibo
    {
        public int Id { get; set; }
        public string Proveedor { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
    }
}
