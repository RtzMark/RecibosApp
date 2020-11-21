using Axosnet.Recibos.Api.Middleware;
using Axosnet.Recibos.Aplicacion.Acceso;
using Axosnet.Recibos.Aplicacion.Recibos;
using Axosnet.Recibos.Aplicacion.Usuarios;
using Axosnet.Recibos.Aplicacion.UsuarioSesion;
using Axosnet.Recibos.Persistencia;
using Axosnet.Recibos.Seguridad.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddCors(o => o.AddPolicy("corsApp", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<RecibosContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cnzxDTt2ZHsR5tJh4BFRktnS6SdpFqhdwnkJc6atfBbZqxpmZe"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.Configure<TokenConfiguracion>(Configuration.GetSection("jwt"));

            services.AddScoped<ITokenGenerador, TokenGenerador>();

            services.AddTransient<IAccesoHelper, AccesoHelper>();
            services.AddTransient<IUsuarioSesionHelper, UsuarioSesionHelper>();
            services.AddTransient<IUsuarioHelper, UsuarioHelper>();
            services.AddTransient<IReciboHelper, ReciboHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("corsApp");
            app.UseMiddleware<ErrorMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

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
