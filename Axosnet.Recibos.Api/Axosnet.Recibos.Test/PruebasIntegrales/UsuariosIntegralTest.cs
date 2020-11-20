using Axosnet.Recibos.Dominio;
using Axosnet.Recibos.Dominio.Entidad;
using Axosnet.Recibos.Test.Configuracion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Test.PruebasUnitarias
{
    [TestClass]
    public class UsuariosIntegralTest : BasePruebas
    {
        private static readonly string dbNombre = "testRecibos";
        private static readonly string url = "/api/Usuarios";


        [TestMethod]
        public async Task Get()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);

            var contexto = ConstruirContext(dbNombre);
            contexto.Usuarios.Add(
                new Usuario
                {
                    Id = new Guid("F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801"),
                    Email = "usuario@axosnet.com",
                    Nombre = "Marco",
                    Clave = "Prueba123",
                    Activo = true
                });
            await contexto.SaveChangesAsync();

            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);

            respuesta.EnsureSuccessStatusCode();

            var usuarios = JsonConvert.DeserializeObject<Respuesta<List<Usuario>>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(1, usuarios.datos.Count);
        }

        [TestMethod]
        public async Task GetUsuario()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);

            var contexto = ConstruirContext(dbNombre);
            var usuario = new Usuario
            {
                Id = new Guid("F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801"),
                Email = "usuario@axosnet.com",
                Nombre = "Marco",
                Clave = "Prueba123",
                Activo = true
            };

            contexto.Usuarios.Add(usuario);
            await contexto.SaveChangesAsync();

            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync($"{url}/F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801");

            respuesta.EnsureSuccessStatusCode();

            var usuarios = JsonConvert.DeserializeObject<Respuesta<Usuario>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(usuario.Id, usuarios.datos.Id);
        }

        [TestMethod]
        public async Task Put()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);

            var contexto = ConstruirContext(dbNombre);
            contexto.Usuarios.Add(
                new Usuario
                {
                    Id = new Guid("F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801"),
                    Email = "usuario@axosnet.com",
                    Nombre = "Marco",
                    Clave = "Prueba123",
                    Activo = true
                });
            await contexto.SaveChangesAsync();

            var usuarioActualizar = new Usuario
            {
                Id = new Guid("F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801"),
                Email = "usuario@axosnet.com",
                Nombre = "Marco Retiz",
                Clave = "Prueba123"
            };

            var cliente = factory.CreateClient();
            var contentData = JsonConvert.SerializeObject(usuarioActualizar);

            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var respuesta = await cliente.PutAsync(url, new StringContent(contentData, Encoding.UTF8, "application/json"));
            var respuestaPeticion = JsonConvert.DeserializeObject<Respuesta<Usuario>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(false, respuestaPeticion.error);
        }

        [TestMethod]
        public async Task Delete()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);
            var contexto = ConstruirContext(dbNombre);
            contexto.Usuarios.Add(
                new Usuario
                {
                    Id = new Guid("F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801"),
                    Email = "usuario@axosnet.com",
                    Nombre = "Marco",
                    Clave = "Prueba123",
                    Activo = true
                });
            await contexto.SaveChangesAsync();

            var cliente = factory.CreateClient();
            var respuesta = await cliente.DeleteAsync($"{url}/F5ADB2A7-A0AF-4D9F-B626-18DE5A3F1801");
            var respuestaPeticion = JsonConvert.DeserializeObject<Respuesta<Usuario>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(false, respuestaPeticion.error);
        }
    }
}
