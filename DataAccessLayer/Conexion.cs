using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Conexion
    {
        private static readonly Conexion instancia = new Conexion();
        public static Conexion Instancia
        {
            get { return Conexion.instancia; }
        }

        public SqlConnection obtenerConexion() {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=.; Initial Catalog=UpnHorarios; User ID=sa; Password=123456";
            return cn;
        }
    }
}
