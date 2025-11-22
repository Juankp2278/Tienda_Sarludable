using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace Sistema_De_Gestion
{
    public class ConexionBD
    {
        private readonly string cadenaConexion = "server=localhost;user=root;password=;database=sarludables_db";

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}
