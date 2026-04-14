using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
   

    public class GastoPorIntegrante
    {
        public string NombreIntegrante { get; set; }
        public List<GastoCategoria> Categorias { get; set; }
        public decimal Total { get { return Categorias.Sum(c => c.Total); } }

        public GastoPorIntegrante()
        {
            Categorias = new List<GastoCategoria>();
        }
    }
}
