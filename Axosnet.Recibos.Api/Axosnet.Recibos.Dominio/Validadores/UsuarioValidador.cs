using Axosnet.Recibos.Dominio.Entidad;
using FluentValidation;

namespace Axosnet.Recibos.Dominio.Validadores
{
    public class UsuarioValidador : AbstractValidator<Usuario>
    {
        public UsuarioValidador()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Clave).NotNull().NotEmpty();
            RuleFor(x => x.Nombre).NotNull().NotEmpty();
        }
    }
}
