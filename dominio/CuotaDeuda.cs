using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class CuotaDeuda
    {
        public int IdCuotaDeuda { get; set; }
        public Deuda Deuda { get; set; }
        public int NumeroCuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public EstadoCuota Estado { get; set; }
    }
}
