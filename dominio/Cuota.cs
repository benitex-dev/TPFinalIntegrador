using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    class Cuota
    {
        public int IdCuota { get; set; }
        public Gasto IdGasto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoPesos { get; set; }
        public string Moneda { get; set; }
        public decimal MontoUSD { get; set; }
        public decimal Cotizacion { get; set; }
        public string Descripcion { get; set; }
        public Categoria IdCategoria { get; set; }
        public MedioPago IdMedioPago { get; set; }
        public Usuario IdUsuario { get; set; }
        public Hogar IdHogar { get; set; }
        public bool Estado { get; set; }
    }
}
