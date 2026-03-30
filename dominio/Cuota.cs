using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum EstadoCuota
    {
        Pendiente = 1,
        Pagada = 2,
        Vencida = 3
    }
    public class Cuota
    {
        public int IdCuota { get; set; }

        public Gasto Gasto { get; set; }

        public int NumeroCuota { get; set; }

        public int TotalCuotas { get; set; } 

        public decimal Monto { get; set; }

        public DateTime Vencimiento { get; set; }

        public EstadoCuota Estado { get; set; }

        public string DescripcionCuota
        {
            get
            {
                return $"{Gasto.Descripcion} - Cuota {NumeroCuota}/{TotalCuotas}";
            }
        }
    }
}