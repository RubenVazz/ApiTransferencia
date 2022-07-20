using Model;
using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc;

namespace AccesoDatos.Repositorios
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private ConfiguracionBD _connectionString;
        public TransaccionRepository(ConfiguracionBD connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Transaccion>> GetHistorialTransferenciaOrigen(string num_cta)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT * FROM public.transferencias
                            WHERE num_cta=@NumeroCuenta ";

            var response = await db.QueryAsync<Transaccion>(sql, new { NumeroCuenta = num_cta });

            return response;
        }

        public async Task<IEnumerable<Transaccion>> GetHistorialTransferenciaDestino(string num_cta)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT * FROM public.transferencias
                            WHERE num_cta=@NumeroCuentaDestino ";
            var response = await db.QueryAsync<Transaccion>(sql, new { NumeroCuentaDestino = num_cta });

            return response;

        }

        public async Task<IEnumerable<Transaccion>> GetTransaccion()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * FROM public.transferencias ";

            return await db.QueryAsync<Transaccion>(sql, new { });
        }

        public async Task<Cliente> GetTransferEstado(string idtransferencia)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT estado FROM public.transferencias
                            WHERE id_transaccion=@IdTransaccion";

            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { IdTransaccion = idtransferencia });
        }

        public async Task<bool> InsertTransaccion(TransaccionRequest transaccion)
        {
            var db = dbConnection();

            var sql = @"
                            INSERT INTO public.transferencias(id_transaccion,num_cta,
                            cedula,fecha,monto,estado)
                            VALUES(@IdTransaccion, @NumeroCuenta ,@Cedula,@Fecha,@Monto,@Estado)";



            var result = await db.ExecuteAsync(sql, new { transaccion.IdTransaccion,
                transaccion.NumeroCuenta,transaccion.Cedula,
                transaccion.Fecha,transaccion.Monto,transaccion.Estado});
            
            if (result > 0)
            {
                string UpdateSaldoCuentaOrigen = @"UPDATE cuenta 
                                                        SET saldo = saldo - @Monto 
                                                         WHERE num_cta = @NumeroCuenta";

                await db.ExecuteAsync(UpdateSaldoCuentaOrigen, new { transaccion.Monto, transaccion.NumeroCuenta });

                string UpdateSaldoCuentaDestino = @"UPDATE cuenta
                                                          SET saldo = saldo + @Monto 
                                                          WHERE num_cta = @NumeroCuentaDestino";

                await db.ExecuteAsync(UpdateSaldoCuentaDestino, new { transaccion.Monto, transaccion.NumeroCuentaDestino });
            }

            return result > 0;
        }
        public async Task<string> UpdateEstado(Transaccion tr)
        {
            var db = dbConnection();

            var sql = @"
                            UPDATE public.transferencias  
                            SET estado = @estado
                            WHERE id_transaccion = @IdTransaccion";
            return await db.QueryFirstOrDefaultAsync<string>(sql, new {tr.IdTransaccion, tr.Estado });

        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        
    }
}
