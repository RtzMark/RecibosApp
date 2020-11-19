using System;

namespace Axosnet.Recibos.Dominio.Entidad
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
    }
}
