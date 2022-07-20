using FluentValidation;
using Model;
using System.Threading.Tasks;

namespace AccesoDatos
{
     public class CuentaValidator : AbstractValidator<Cuentas>
    {
        public CuentaValidator()
        {
            RuleFor(Cuentas => Cuentas.num_cta).NotNull().NotEmpty();
            RuleFor(Cuentas => Cuentas.Moneda).NotNull().NotNull().MaximumLength(3).MinimumLength(3);
            RuleFor(Cuentas => Cuentas.Cedula).NotNull().NotEmpty();
            RuleFor(Cuentas => Cuentas.Saldo).NotNull().NotEmpty();
            RuleFor(Cuentas => Cuentas.cod_banco).NotNull().NotEmpty().MinimumLength(8).MaximumLength(8);

        }
    }
}
