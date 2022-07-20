using Dapper;
using Model;
using Npgsql;

namespace AccesoDatos.Repositorios
{
    public class BancoRepository : IBancoRepository
    {
        private ConfiguracionBD _connectionString;
        public BancoRepository(ConfiguracionBD connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteBanco(Banco bancos)
        {
            var db = dbConnection();

            var sql = @"
                            DELETE FROM public.banco 
                            WHERE cod_banco=@cod_banco";

            var result = await db.ExecuteAsync(sql, new { cod_banco = bancos.cod_banco });
            return result > 0;
        }

        public async Task<IEnumerable<Banco>> GetAllBancos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * FROM public.banco ";

            return await db.QueryAsync<Banco>(sql, new { });
        }

        public async Task<Banco> GetBancoDetalle(string cod_banco)
        {
            var db = dbConnection();

            var sql = @"
                             SELECT * FROM public.banco
                            WHERE cod_banco=@cod_banco";

            return await db.QueryFirstOrDefaultAsync<Banco>(sql, new { CodigoBanco = cod_banco });
        }

        public async Task<bool> InsertBanco(Banco banco)
        {
            var db = dbConnection();

            var sql = @"
                            INSERT INTO public.""banco""(cod_banco,nombre_banco,direccion)
                            VALUES(@cod_banco, @Nombre_Banco ,@Direccion)";



            var result = await db.ExecuteAsync(sql, new { banco.cod_banco, banco.nombre_banco, banco.direccion });

            return result > 0;
        }

        public async Task<bool> UpdateBanco(Banco bancos)
        {
            var db = dbConnection();

            var sql = @"
                            UPDATE public.""banco""
                            SET cod_banco = @Cod_Banco,
                                nombre_banco = @Nombre_Banco,
                                 direccion= @Direccion
                            WHERE cod_banco=@cod_banco";

            var result = await db.ExecuteAsync(sql, new { bancos.cod_banco, bancos.nombre_banco, bancos.direccion });

            return result > 0;
        }
    }
}
