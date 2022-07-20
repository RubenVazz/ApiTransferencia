using Model;

namespace AccesoDatos.Repositorios
{
    public interface ITransaccionRepository
    {
        Task<IEnumerable<Transaccion>> GetTransaccion();
        Task<bool> InsertTransaccion(TransaccionRequest transaccion);
        Task<string> UpdateEstado(Transaccion tr);
        Task<Cliente> GetTransferEstado(string idtransferencia);
        Task<IEnumerable<Transaccion>> GetHistorialTransferenciaOrigen(string num_cta);
        Task<IEnumerable<Transaccion>> GetHistorialTransferenciaDestino(string num_cta);




    }
}
