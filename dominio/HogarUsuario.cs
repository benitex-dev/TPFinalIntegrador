using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum Rol
    {
        Admin,
        Miembro,
        Lector,
    }
    public class HogarUsuario
    {
        public int IdMiembro { get; set; }
        public Hogar Hogar { get; set; }
        public Usuario Usuario { get; set; }
        public Rol Rol { get; set; }
        public bool Estado { get; set; }
    }
}
