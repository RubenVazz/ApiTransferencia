using FluentValidation;
using Model;

namespace AccesoDatos
{
    public class BancoValidator : AbstractValidator<Banco>
    {
        public BancoValidator()
        {
            RuleFor(Banco => Banco.cod_banco).NotNull().NotEmpty().Length(8);
            RuleFor(Banco => Banco.nombre_banco).NotNull().NotEmpty();
            RuleFor(Banco => Banco.direccion).NotNull().NotEmpty();

        }


    }
}
