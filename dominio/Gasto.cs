using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoPesos { get; set; }
        public string Moneda { get; set; }
        public Categoria idCategoria { get; set; }
        public string Descripcion { get; set; }
        public MedioPago IdMedioDePago { get; set; }
        public Usuario Usuario { get; set; }
        public Hogar Hogar { get; set; }
        public decimal MontoUSD { get; set; }
        public decimal Cotizacion { get; set; }
        public bool Estado { get; set; }



    }
}
