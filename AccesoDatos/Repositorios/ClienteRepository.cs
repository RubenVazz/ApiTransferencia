using Dapper;
using Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        private ConfiguracionBD _connectionString;
        public ClienteRepository(ConfiguracionBD connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteCliente(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"
                            DELETE  FROM public.cliente 
                            WHERE Cedula=@CEDULA";

            var result = await db.ExecuteAsync(sql, new { cedula = cliente.Cedula });
            return result > 0;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * FROM public.cliente ";

            return await db.QueryAsync<Cliente>(sql, new { });
        }

        public async Task<Cliente> GetClienteDetalle(string cedula)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT * FROM public.cliente
                            WHERE Cedula=@CEDULA";

            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { CEDULA = cedula });

        }

        public async Task<bool> InsertCliente(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"
                            INSERT INTO public.""cliente""(Cedula ,Tipo_Doc, Nombre_Apellido)
                            VALUES(@CEDULA, @TIPO_DOC ,@NOMBRE_APELLIDO) ";

            var result = await db.ExecuteAsync(sql, new { cliente.Cedula, cliente.Tipo_Doc, cliente.Nombre_Apellido });

            return result > 0;
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"
                            UPDATE public.""cliente""
                            SET Cedula= @CEDULA,
                                 Tipo_Doc= @TIPO_DOC,
                                 Nombre_Apellido=@NOMBRE_APELLIDO
                            WHERE Cedula=@CEDULA";

            var result = await db.ExecuteAsync(sql, new { cliente.Cedula, cliente.Tipo_Doc, cliente.Nombre_Apellido });

            return result > 0;
        }
    }
}
