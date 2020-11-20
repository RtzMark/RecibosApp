using Axosnet.Recibos.Api.Middleware;
using Axosnet.Recibos.Aplicacion.Acceso;
using Axosnet.Recibos.Aplicacion.Recibos;
using Axosnet.Recibos.Aplicacion.Usuarios;
using Axosnet.Recibos.Persistencia;
using Axosnet.Recibos.Seguridad.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Axosnet.Recibos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RecibosContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<ITokenGenerador, TokenGenerador>();

            services.AddTransient<IAccesoHelper, AccesoHelper>();
            services.AddTransient<IUsuarioHelper, UsuarioHelper>();
            services.AddTransient<IReciboHelper, ReciboHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ErrorMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
