using Model;
using FluentValidation;

namespace AccesoDatos

{
    public class TransaccionValidator:AbstractValidator<TransaccionRequest>
    {
        public TransaccionValidator()
        {
            RuleFor(TransaccionRequest => TransaccionRequest.BancoOrigen).NotNull().NotEmpty().Length(8);
            RuleFor(TransaccionRequest => TransaccionRequest.BancoDestino).NotNull().NotEmpty().Length(8);
            string[] condiciones = new string[]
            {
                "Aprobado", "aprobado", "APROBADO",
                "En Proceso", "en proceso", "EN PROCESO",
                "Rechazada", "rechazada", "RECHAZADA",

            };
            RuleFor(TransaccionRequest => TransaccionRequest.Estado).NotNull().NotEmpty().Must(value => condiciones.Contains(value)).WithMessage("Estado no valido");
        }
    }
}
