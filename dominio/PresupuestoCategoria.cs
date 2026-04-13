using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class PresupuestoCategoria
    {
        public int IdPresupuesto { get; set; }
        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal MontoPresupuestado { get; set; }
        public decimal GastoReal { get; set; } // se calcula al listar, no se guarda en DB
    }
}
