using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    class Deuda
    {
        public int IdDeuda { get; set; }
        public Usuario Usuario { get; set; }
        public string NombreDeudor { get; set; }
        public string EmailDeudor { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoTotal { get; set; }
        public int Cuotas { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool Estado { get; set; }
    }
}
