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
            public string Nombre { get; set; }
            //public Hogar Hogar { get; set; } //categoria compartida
            public Usuario Usuario { get; set; } //categoria personal
            public TipoCategoria Tipo { get; set; }
            public bool Estado { get; set; }
            public Hogar Hogar { get; set; }
    }

    

}