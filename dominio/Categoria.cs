using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum TipoCategoria
    {
        Ingreso = 1,
        Gasto = 2
    }

    public class Categoria
    {
        
            public int IdCategoria { get; set; }
            public Hogar Hogar { get; set; }
            public Usuario Usuario { get; set; }
            public TipoCategoria tipo { get; set; }
            public bool Estado { get; set; }
    }

    

}