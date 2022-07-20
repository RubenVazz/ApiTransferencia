namespace Model
{
    public class Cuentas
    {
        public string Id_Cuenta { get; set; }
        public string? num_cta { get; set; }
        public string? Moneda { get; set; }
        public string? Cedula { get; set; }
        public decimal Saldo { get; set; }
        public string cod_banco { get; set; }
    }
}