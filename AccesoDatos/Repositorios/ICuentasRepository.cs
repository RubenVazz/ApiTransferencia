using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public interface ICuentasRepository
    {
        Task<IEnumerable<Cuentas>> GetAllCuentas();
        Task<Cuentas> GetCuentaDetalle(string idCuenta);
        Task<bool> InsertCuenta(Cuentas cuentas);
        Task<bool> UpdateCuenta(Cuentas cuentas);
        Task<bool> DeleteCuenta(Cuentas cuentas);
    }
}
