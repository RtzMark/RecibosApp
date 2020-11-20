using Axosnet.Recibos.Api;
using Axosnet.Recibos.Aplicacion.Usuarios;
using Axosnet.Recibos.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Axosnet.Recibos.Test.Configuracion
{
    public class BasePruebas
    {
        protected RecibosContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<RecibosContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new RecibosContext(opciones);
            return dbContext;
        }

        protected WebApplicationFactory<Startup> ConstruirWebApplicationFactory(string nombreBD,
            bool ignorarSeguridad = true)
        {
            var factory = new WebApplicationFactory<Startup>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptorDBContext = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<RecibosContext>));

                    if (descriptorDBContext != null)
                    {
                        services.Remove(descriptorDBContext);
                    }

                    services.AddTransient<IUsuarioHelper, UsuarioHelper>();

                    services.AddDbContext<RecibosContext>(options =>
                    options.UseInMemoryDatabase(nombreBD));

                    if (ignorarSeguridad)
                    {
                        services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();

                        services.AddControllers(options =>
                        {
                            options.Filters.Add(new UsuarioFalsoAutenticacion());
                        });
                    }
                });
            });

            return factory;
        }
    }
}
