using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Axosnet.Recibos.Test.Configuracion
{
    public class UsuarioFalsoAutenticacion : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "admin@axosnet.com"),
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.NameIdentifier, "044C5CE8-14A9-4260-9C4A-B4F7FCE5D86F"),
                }, "prueba"));

            await next();
        }
    }
}
