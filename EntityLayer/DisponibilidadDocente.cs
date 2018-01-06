using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class DisponibilidadDocente
    {
        public int id { get; set; }
        public Docente docente { get; set; }
        public Periodo periodo { get; set; }
        public byte maximoHorasSemana { get; set; }
        public List<DetalleDisponibilidadDocente> listaDisponibilidad { get; set; }
    }
}
