using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Ingreso
    {
        int IdIngreso { get; set; }
        string Descripcion { get; set; }
        DateTime Fecha { get; set; }
        Double Monto { get; set; }
        Categoria Categoria { get; set; }
        Usuario Usuario { get; set; }
        Hogar Hogar { get; set; }
        bool Estado { get; set; }

    }
}
