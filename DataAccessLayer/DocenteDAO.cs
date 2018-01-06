using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DocenteDAO
    {
        private static readonly DocenteDAO instancia = new DocenteDAO();
        public static DocenteDAO Instancia
        {
            get { return DocenteDAO.instancia; }
        }

        public List<Docente> obtenerListadoDocentes() {

            SqlCommand cmd = null;
            List<Docente> lista = new List<Docente>();
            Docente obj;

            try
            {
                SqlConnection cn = Conexion.Instancia.obtenerConexion();
                cmd = new SqlCommand("spr_obtenerListadoDocentes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    obj = new Docente();
                    obj.id = (int) reader["id"];
                    obj.apellidoPaterno = reader["apellidoPaterno"] as String;
                    obj.apellidoMaterno = reader["apellidoMaterno"] as String;
                    obj.nombres = reader["nombres"] as String;
                    obj.descripcionEspecialidad = reader["descripcionEspecialidad"] as String;
                    obj.categoria = reader["categoria"] as String;
                    obj.edad = reader["edad"] as byte? ?? default(byte);
                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }
    }
}
