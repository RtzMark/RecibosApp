using Axosnet.Recibos.Dominio.Entidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Axosnet.Recibos.Persistencia
{
    public class RecibosContext : DbContext
    {
        public RecibosContext(DbContextOptions options) 
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InformacionInicial.SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Recibo> Recibos { get; set; }
    }
}
