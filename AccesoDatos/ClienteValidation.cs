using FluentValidation;
using Model;

namespace AccesoDatos

{
    public class ClienteValidation: AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(Cliente => Cliente.Nombre_Apellido).NotNull().NotEmpty().MinimumLength(5).MaximumLength(50);
            string[] condiciones = new string[]
            {
                "ci", "cedula", "cédula",
                "Ci", "Cedula", "Cédula",
                "CI", "CEDULA", "CÉDULA",
                "ruc", "RUC", "R.U.C.",
                "pasaporte", "Pasaporte","PASAPORTE",

            };
            RuleFor(Cliente => Cliente.Tipo_Doc).NotNull().NotEmpty().Must(value => condiciones.Contains(value)).WithMessage("Documento no valido");
            RuleFor(Cliente => Cliente.Cedula).NotNull();
        }


    }
}
