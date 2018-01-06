using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class SlotHorario
    {
        public int id { get; set; }
        public UnidadHoraria unidadHoraria { get; set; }
        public DiaSemana diaSemana { get; set; }
    }
}
