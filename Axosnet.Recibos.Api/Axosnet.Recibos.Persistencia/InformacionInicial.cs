using Axosnet.Recibos.Dominio.Entidad;
using Axosnet.Recibos.Seguridad.Crypt;
using Microsoft.EntityFrameworkCore;
using System;

namespace Axosnet.Recibos.Persistencia
{
    public class InformacionInicial
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasData(
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Admin",
                    Email = "admin@axosnet.com",
                    Clave = Cifrado.EncryptSHA256("admin"),
                    Activo = true
                });
        }
    }
}
