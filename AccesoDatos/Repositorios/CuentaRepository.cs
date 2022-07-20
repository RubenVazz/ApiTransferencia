using Dapper;
using Model;
using Npgsql;

namespace AccesoDatos.Repositorios
{
    public class CuentaRepository : ICuentasRepository
    {
        private ConfiguracionBD _connectionString;
        public CuentaRepository(ConfiguracionBD connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteCuenta(Cuentas cuentas)
        {
            var db = dbConnection();

            var sql = @"
                            DELETE FROM public.cuenta 
                            WHERE id_cuenta=@Id_Cuenta";

            var result = await db.ExecuteAsync(sql, new { cuentas.Id_Cuenta });
            if (result < 1)
            {
                throw new Exception($"Cuenta{cuentas} no existe");
            }
            return result > 0;
        }

        public async Task<IEnumerable<Cuentas>> GetAllCuentas()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * FROM public.cuenta ";

            return await db.QueryAsync<Cuentas>(sql, new { });
        }

        public async Task<Cuentas> GetCuentaDetalle(string IdCuenta)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT * FROM public.cuenta
                            WHERE id_cuenta=@Id_Cuenta ";

            return await db.QueryFirstOrDefaultAsync<Cuentas>(sql, new {Id_Cuenta = IdCuenta });
        }

        public async Task<bool> InsertCuenta(Cuentas cuentas)
        {
            var db = dbConnection();

            var sql = @"
                            INSERT INTO public.""cuenta""(id_cuenta,num_cta,moneda,cedula,saldo,cod_banco)
                            VALUES(@Id_Cuenta,@num_cta,@Moneda,@Cedula,@Saldo,@cod_banco) ";

            var result = await db.ExecuteAsync(sql, new
            {
                cuentas.Id_Cuenta,
                cuentas.num_cta,
                cuentas.Moneda,
                cuentas.Cedula,
                cuentas.Saldo,
                cuentas.cod_banco
            });

            return result > 0;
        }

        public async Task<bool> UpdateCuenta(Cuentas cuentas)
        {
            var db = dbConnection();

            var sql = @"
                            UPDATE public.""cuenta""
                            SET id_cuenta = @Id_Cuenta,
                                num_cta = @num_cta,
                                moneda = @Moneda,   
                                cedula = @Cedula,
                                saldo = @Saldo,
                                cod_banco =@cod_banco  
                            WHERE id_cuenta=@id_cuenta";

            var result = await db.ExecuteAsync(sql, new
            {
                cuentas.Id_Cuenta,
                cuentas.num_cta,
                cuentas.Moneda,
                cuentas.Cedula,
                cuentas.Saldo,
                cuentas.cod_banco
            });

            return result > 0;
        }
    }
}
