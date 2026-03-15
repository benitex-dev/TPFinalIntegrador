using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dominio
{
    public enum TipoPago
    {
        Efectivo = 1,
        Debito = 2,
        Credito = 3,
        Transferencia = 4,
        BilleteraVirtual = 5
    }
    public class MedioPago
    {
        public int IdMedioPago {  get; set; }
        public TipoPago Tipo {  get; set; } 
        public string Descripcion { get; set; }
        public Usuario Usuario { get; set; }
        public Hogar Hogar { get; set; }
        public int DiaCierre { get; set; } //No ponemos DATE TIME porque va a pedir un año por el formato y no serìa correcto 
        public int DiaVencimiento { get; set; }
        public bool Estado { get; set; }
    }
}
