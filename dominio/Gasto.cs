using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{

    public enum Moneda
    {
        ARS = 1,
        USD = 2,
        EUR = 3,
        BRL = 4
    }
    public class Gasto
    {
        public int IdGasto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoPesos { get; set; }
        public int NumeroCuota { get; set; }
        public Moneda Moneda { get; set; }
        public Categoria Categoria { get; set; }
        public string Descripcion { get; set; }
        public MedioPago MedioDePago { get; set; }
        public Usuario Usuario { get; set; }
        //public Hogar Hogar { get; set; }
        public decimal MontoUSD { get; set; }
        public decimal Cotizacion { get; set; }
        public bool Estado { get; set; }
        //CUOTAS
        public bool EsEnCuotas { get; set; }
        public int CantidadCuotas { get; set; }
        public decimal MontoCuota { get; set; }
        public Hogar Hogar { get; set; }



    }
}
