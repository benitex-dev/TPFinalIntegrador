using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Ingreso
    {
        public int IdIngreso { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }
        public Hogar Hogar { get; set; }
        bool Estado { get; set; }

    }
}
