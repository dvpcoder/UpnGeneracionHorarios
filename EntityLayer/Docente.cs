using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Docente
    {
        public int id { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string descripcionEspecialidad { get; set; }
        public string categoria { get; set; }
        public byte edad { get; set; }
    }
}
