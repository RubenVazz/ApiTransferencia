using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Transaccion
    {
        public string IdTransaccion { get; set; }
        public string Num_Cta { get; set; }
        public string Cedula { get; set; }  
        public DateTime Fecha  { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
    }
}
