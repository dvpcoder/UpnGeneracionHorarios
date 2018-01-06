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
    public class DisponibilidadDocenteDAO
    {
        private static readonly DisponibilidadDocenteDAO instancia = new DisponibilidadDocenteDAO();
        public static DisponibilidadDocenteDAO Instancia
        {
            get { return DisponibilidadDocenteDAO.instancia; }
        }

        public DisponibilidadDocente obtenerDisponibilidadDocente(int idDocente, int idPeriodo)
        {

            SqlCommand cmd = null;
            DisponibilidadDocente obj = null;

            try
            {
                SqlConnection cn = Conexion.Instancia.obtenerConexion();
                cmd = new SqlCommand("spr_obtenerDisponibilidadDocente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDPERIODO", idPeriodo);
                cmd.Parameters.AddWithValue("@IDDOCENTE", idDocente);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    obj = new DisponibilidadDocente();
                    obj.id = (int)reader["id"];
                    obj.maximoHorasSemana = reader["MaximoHorasSemana"] as byte? ?? default(byte);
                    obj.periodo = new Periodo()
                    {
                        id = idPeriodo
                    };
                    obj.docente = new Docente()
                    {
                        id = idDocente
                    };
                    obj.listaDisponibilidad = DisponibilidadDocenteDAO.Instancia.obtenerDetalleDisponibilidadDocente(obj.id);
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
            return obj;
        }

        public List<DetalleDisponibilidadDocente> obtenerDetalleDisponibilidadDocente(int idDisponibilidadDocente)
        {
            SqlCommand cmd = null;
            List<DetalleDisponibilidadDocente> lista = new List<DetalleDisponibilidadDocente>();
            DetalleDisponibilidadDocente obj;

            try
            {
                SqlConnection cn = Conexion.Instancia.obtenerConexion();
                cmd = new SqlCommand("spr_obtenerDetalleDisponibilidadDocente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDDISPONIBILIDADDOCENTE", idDisponibilidadDocente);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    obj = new DetalleDisponibilidadDocente();
                    obj.id = (int)reader["id"];
                    obj.slotHorario = new SlotHorario()
                    {
                        id = (int)reader["IdSlotHorario"]
                    };
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

        public int registrarDisponibilidadDocente(DisponibilidadDocente disponibilidad)
        {
            SqlCommand cmd = null;
            int insercion = 0;

            try
            {
                SqlConnection cn = Conexion.Instancia.obtenerConexion();
                cn.Open();

                cmd = cn.CreateCommand();
                SqlTransaction transaccion = cn.BeginTransaction();
                cmd.Connection = cn;
                cmd.Transaction = transaccion;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "spr_registrarDisponibilidadDocente";
                cmd.Parameters.AddWithValue("@IDPERIODO", disponibilidad.periodo.id);
                cmd.Parameters.AddWithValue("@IDDOCENTE", disponibilidad.docente.id);
                cmd.Parameters.AddWithValue("@MAXIMOHORASSEMANA", disponibilidad.maximoHorasSemana);

                disponibilidad.id = (int) cmd.ExecuteScalar();
                cmd.Parameters.Clear();

                if(disponibilidad.id > 0)
                {
                    foreach(DetalleDisponibilidadDocente detalle in disponibilidad.listaDisponibilidad)
                    {
                        cmd.CommandText = "spr_registrarDetalleDisponibilidadDocente";
                        cmd.Parameters.AddWithValue("@IDDISPONIBILIDADDOCENTE", disponibilidad.id);
                        cmd.Parameters.AddWithValue("@IDSLOTHORARIO", detalle.slotHorario.id);
                        insercion = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                
                if(insercion > 0)
                {
                    transaccion.Commit();
                }else
                {
                    transaccion.Rollback();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return insercion;
        }

        public int editarDisponibilidadDocente(DisponibilidadDocente disponibilidad)
        {
            SqlCommand cmd = null;
            int insercion = 0;

            try
            {
                SqlConnection cn = Conexion.Instancia.obtenerConexion();
                cn.Open();

                cmd = cn.CreateCommand();
                SqlTransaction transaccion = cn.BeginTransaction();
                cmd.Connection = cn;
                cmd.Transaction = transaccion;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "spr_editarDisponibilidadDocente";
                cmd.Parameters.AddWithValue("@ID", disponibilidad.id);
                cmd.Parameters.AddWithValue("@IDPERIODO", disponibilidad.periodo.id);
                cmd.Parameters.AddWithValue("@IDDOCENTE", disponibilidad.docente.id);
                cmd.Parameters.AddWithValue("@MAXIMOHORASSEMANA", disponibilidad.maximoHorasSemana);

                insercion = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                if(insercion > 0)
                {
                    cmd.CommandText = "spr_limpiarDetalleDisponibilidadDocente";
                    cmd.Parameters.AddWithValue("@IDDISPONIBILIDADDODCENTE", disponibilidad.id);
                    insercion = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if(insercion > 0)
                    {
                        foreach (DetalleDisponibilidadDocente detalle in disponibilidad.listaDisponibilidad)
                        {
                            cmd.CommandText = "spr_registrarDetalleDisponibilidadDocente";
                            cmd.Parameters.AddWithValue("@IDDISPONIBILIDADDOCENTE", disponibilidad.id);
                            cmd.Parameters.AddWithValue("@IDSLOTHORARIO", detalle.slotHorario.id);
                            insercion = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }                        
                    }
                }

                if (insercion > 0)
                {
                    transaccion.Commit();
                }
                else
                {
                    transaccion.Rollback();
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
            return insercion;
        }
    }
}
    