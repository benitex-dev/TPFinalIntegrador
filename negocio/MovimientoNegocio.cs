using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MovimientoNegocio
    {

        //public void CargarMovimientosDelMes()
        //{
        //   List<Movimiento> lista = ObtenerMovimientosDelMes();

        // rptMovimientos.DataSource = lista;
        // rptMovimientos.DataBind();
        //}

        public List<Movimiento> ListarMovimientosDelMes(int idUsuario)
        {
            return ListarMovimientosPorMes(idUsuario, DateTime.Now.Month, DateTime.Now.Year);
        }

        public List<Movimiento> ListarMovimientosPorMes(int idUsuario, int mes, int anio)
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            IngresoNegocio ingresoNegocio = new IngresoNegocio();
            GastoNegocio gastoNegocio = new GastoNegocio();
            CuotaNegocio cuotaNegocio = new CuotaNegocio();

            List<Ingreso> ingresos = ingresoNegocio.ListarPorUsuarioPorMes(idUsuario, mes, anio);
            List<Gasto> gastos = gastoNegocio.ListarPorUsuarioPorMes(idUsuario, mes, anio);
            List<Cuota> cuotas = cuotaNegocio.ListarPorUsuarioPorMes(idUsuario, mes, anio);

            foreach (Ingreso ingreso in ingresos)
            {
                Movimiento mov = new Movimiento();
                mov.Fecha = ingreso.Fecha;
                mov.Descripcion = ingreso.Descripcion;
                mov.Categoria = ingreso.Categoria.Nombre;
                mov.Tipo = "Ingreso";
                mov.Monto = ingreso.Monto;
                mov.Estado = ingreso.Estado ? "Activo" : "Eliminado";

                movimientos.Add(mov);
            }

            foreach (Gasto gasto in gastos)
            {
                if (!gasto.EsEnCuotas)
                {
                    Movimiento mov = new Movimiento();
                    mov.Fecha = gasto.Fecha;
                    mov.Descripcion = gasto.Descripcion;
                    mov.Categoria = gasto.Categoria.Nombre;
                    mov.Tipo = "Gasto";
                    mov.Monto = gasto.MontoPesos;
                    mov.Estado = gasto.Estado ? "Activo" : "Eliminado";

                    movimientos.Add(mov);
                }
            }

            foreach (Cuota cuota in cuotas)
            {
                Movimiento mov = new Movimiento();
                mov.Fecha = cuota.Vencimiento;
                mov.Descripcion = cuota.Gasto.Descripcion + " - Cuota " + cuota.NumeroCuota + "/" + cuota.TotalCuotas;
                mov.Categoria = cuota.Gasto.Categoria.Nombre;
                mov.Tipo = "Gasto";
                mov.Monto = cuota.Monto;
                mov.Estado = cuota.Estado.ToString();

                movimientos.Add(mov);
            }

            return movimientos.OrderByDescending(x => x.Fecha).ToList();
        }
    }
}

