using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cuota
    {
        public int IdCuota { get; set; }
        public Gasto Gasto { get; set; }
        public int NumeroCuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime Vencimiento { get; set; }
        public bool Estado { get; set; }
    }
}
