using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class MetaAhorro
    {
        public int IdMeta { get; set; }
        public string Nombre { get; set; }
        public decimal MontoObjetivo { get; set; }
        public DateTime FechaObjetivo { get; set; }
        public Usuario Usuario { get; set; }
        public Hogar Hogar { get; set; }
        public bool Estado { get; set; }
    }
}
