using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccesoDatos
{
    public class ConfiguracionBD
    {

        public ConfiguracionBD(string connectionString) => ConnectionString = connectionString;
        public string ConnectionString { get; set; }
    }
}
