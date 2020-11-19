using Axosnet.Recibos.Dominio.Entidad;
using FluentValidation;

namespace Axosnet.Recibos.Dominio.Validadores
{
    public class ReciboValidador : AbstractValidator<Recibo>
    {
        public ReciboValidador()
        {
            RuleFor(x => x.Proveedor).NotNull().NotEmpty();
            RuleFor(x => x.Monto).NotNull().NotEmpty();
            RuleFor(x => x.Fecha).NotNull().NotEmpty();
            RuleFor(x => x.Moneda).NotNull().NotEmpty();
            RuleFor(x => x.Comentario).Length(0, 250);
        }
    }
}
