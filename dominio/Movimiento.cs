using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Movimiento
    {
        //Se utiliza como clase intermedia para mostrar el grid principal 
        public int idReferencia {  get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }   // Ingreso o Gasto
        public decimal Monto { get; set; }
        public string Estado { get; set; }

        public string MontoMostrado
        {
            get
            {
                if (Tipo == "Gasto")
                    return "- $ " + Monto.ToString("N2");
                else
                    return "+ $ " + Monto.ToString("N2");
            }
        }
    }
}
