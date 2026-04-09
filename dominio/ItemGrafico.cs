using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ItemGrafico
    {
        public string Nombre { get; set; }
        public decimal Monto { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Offset { get; set; }
        public string ColorHex { get; set; }

        public string PorcentajeStr => Porcentaje.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
        public string OffsetStr => Offset.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
    }
}
