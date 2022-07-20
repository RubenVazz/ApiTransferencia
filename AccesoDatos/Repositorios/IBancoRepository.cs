using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public interface IBancoRepository
    {
        Task<IEnumerable<Banco>> GetAllBancos();
        Task<Banco> GetBancoDetalle(string cod_banco);
        Task<bool> InsertBanco(Banco bancos);
        Task<bool> UpdateBanco(Banco bancos);
        Task<bool> DeleteBanco(Banco bancos);
    }
}
