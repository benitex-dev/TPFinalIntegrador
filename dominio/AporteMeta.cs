using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class AporteMeta
    {public int IdAporte { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaAporte { get; set; }
        public MetaAhorro Meta { get; set; }
    }
}
