using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class MetaResumen
    {
        public int IdMeta { get; set; }
        public string Nombre { get; set; }
        public decimal MontoObjetivo { get; set; }
        public DateTime? FechaObjetivo { get; set; }
        public decimal MontoActual { get; set; }
        public int Porcentaje { get; set; }
    }
}
