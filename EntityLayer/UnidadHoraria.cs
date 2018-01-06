using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class UnidadHoraria
    {
        public int id { get; set; }
        public Franja franja { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
    }
}
