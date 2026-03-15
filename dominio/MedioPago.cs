using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    
    public class MedioPago
    {
        public int idMedioPago {  get; set; }
        //TipoPago tipo {  get; set; } Analizar funcionamiento de este atributo ! 
        public string descripcion { get; set; }
        public Usuario Usuario { get; set; }
        public Hogar Hogar { get; set; }
        public DateTime diaCierre { get; set; }
        public DateTime diaVencimiento { get; set; }
        public bool estado { get; set; }
    }
}
