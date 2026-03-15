using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Hogar
    {
        public int IdHogar { get; set; }
        public string Nombre { get; set; }
        public Usuario Usuario { get; set; }
        public bool Estado { get; set; }
    }
}