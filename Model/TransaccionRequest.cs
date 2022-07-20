namespace Model
{
    public class TransaccionRequest
    {
        public string IdTransaccion { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroCuentaDestino { get; set; } 
        public string Cedula { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string BancoOrigen { get; set; }
        public string BancoDestino { get; set; }
        public decimal Saldo { get; set; } 
    }
}
