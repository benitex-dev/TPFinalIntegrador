using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class HogarUsuario
    {
        public int IdMiembro { get; set; }
        public Hogar Hogar { get; set; }
        public Usuario Usuario { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }
    }
}
