using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DataBridge
    {
        private static readonly DataBridge instancia = new DataBridge();
        public static DataBridge Instancia
        {
            get { return DataBridge.instancia; }
        }

        public List<Docente> obtenerListadoDocentes()
        {
            return DocenteDAO.Instancia.obtenerListadoDocentes();
        }

        public DisponibilidadDocente obtenerDisponibilidadDocente(int idDocente, int idPeriodo)
        {
            return DisponibilidadDocenteDAO.Instancia.obtenerDisponibilidadDocente(idDocente, idPeriodo);
        }

        public int guardarDisponibilidadDocente(DisponibilidadDocente disponibilidad)
        {
            if(disponibilidad.id == 0)
            {
                return DisponibilidadDocenteDAO.Instancia.registrarDisponibilidadDocente(disponibilidad);
            }
            else
            {
                return DisponibilidadDocenteDAO.Instancia.editarDisponibilidadDocente(disponibilidad);
            }
        }
    }
}
