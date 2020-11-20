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
    public class RecibosUnitTest : BasePruebas
    {
        private static readonly string dbNombre = "testRecibos";
        private static readonly string url = "/api/Recibos";


        [TestMethod]
        public async Task Get()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);
            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);

            respuesta.EnsureSuccessStatusCode();

            var recibos = JsonConvert.DeserializeObject<Respuesta<List<Recibo>>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(0, recibos.datos.Count);
        }

        [TestMethod]
        public async Task Post()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);
            var contexto = ConstruirContext(dbNombre);

            var recibo = new Recibo
            {
                Proveedor = "Proveedor",
                Monto = 100,
                Moneda = "mxn",
                Fecha = DateTime.Now,
                Comentario = "Sin comentarios",
                Activo = true
            };

            var cliente = factory.CreateClient();
            var contentData = JsonConvert.SerializeObject(recibo);

            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var respuesta = await cliente.PostAsync(url, new StringContent(contentData, Encoding.UTF8, "application/json"));

            respuesta.EnsureSuccessStatusCode();

            var respuestaPeticion = JsonConvert.DeserializeObject<Respuesta<Recibo>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(false, respuestaPeticion.error);
        }

        [TestMethod]
        public async Task Put()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);
            var contexto = ConstruirContext(dbNombre);

            var recibo = new Recibo
            {
                Proveedor = "Proveedor",
                Monto = 100,
                Moneda = "mxn",
                Fecha = DateTime.Now,
                Comentario = "Sin comentarios",
                Activo = true
            };

            var cliente = factory.CreateClient();
            var contentData = JsonConvert.SerializeObject(recibo);

            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var respuesta = await cliente.PutAsync(url, new StringContent(contentData, Encoding.UTF8, "application/json"));
            var respuestaPeticion = JsonConvert.DeserializeObject<Respuesta<Recibo>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(true, respuestaPeticion.error);
        }

        [TestMethod]
        public async Task Delete()
        {
            var factory = ConstruirWebApplicationFactory(dbNombre);
            var contexto = ConstruirContext(dbNombre);
            var cliente = factory.CreateClient();

            var respuesta = await cliente.DeleteAsync($"{url}/1");
            var respuestaPeticion = JsonConvert.DeserializeObject<Respuesta<Recibo>>(await respuesta.Content.ReadAsStringAsync());

            Assert.AreEqual(true, respuestaPeticion.error);
        }
    }
}
