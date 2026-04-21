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

        public List<Movimiento> ListarMovimientosDelMes(bool esModoHogar,int idReferencia)
        {
            return ListarMovimientos(esModoHogar,idReferencia, DateTime.Now.Month, DateTime.Now.Year);
        }

        public List<Movimiento> ListarMovimientos(bool esModoHogar, int idReferencia, int mes, int anio)
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            IngresoNegocio ingresoNegocio = new IngresoNegocio();
            GastoNegocio gastoNegocio = new GastoNegocio();
            CuotaNegocio cuotaNegocio = new CuotaNegocio(); // Agregamos el negocio de cuotas

            List<Ingreso> ingresos;
            List<Gasto> gastos;
            List<Cuota> cuotas;

            if (esModoHogar)
            {
                ingresos = ingresoNegocio.ListarPorHogarMesActual(idReferencia);
                gastos = gastoNegocio.ListarPorHogarMesActual(idReferencia);
                // Nota: Asegurate de tener este método creado en tu CuotaNegocio
                //cuotas = cuotaNegocio.ListarPorHogarMesActual(idReferencia);
            }
            else
            {
                ingresos = ingresoNegocio.ListarPorUsuarioPorMes(idReferencia, mes, anio);
                gastos = gastoNegocio.ListarPorUsuarioPorMes(idReferencia, mes, anio);
            }
                cuotas = cuotaNegocio.ListarPorUsuarioPorMes(idReferencia, mes, anio);
            //CAMBIAR

            foreach (Ingreso ingreso in ingresos)
            {
                Movimiento mov = new Movimiento();
                mov.idReferencia = ingreso.IdIngreso;
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
                    mov.idReferencia = gasto.IdGasto;
                    mov.Fecha = gasto.Fecha;
                    mov.Descripcion = gasto.Descripcion;
                    mov.Categoria = gasto.Categoria.Nombre;
                    mov.Tipo = "Gasto";
                    mov.Monto = gasto.MontoPesos;
                    mov.MedioPago = gasto.MedioDePago.Descripcion;
                    mov.Estado = gasto.Estado ? "Activo" : "Eliminado";

                    movimientos.Add(mov);
                }
            }

            // 4. MAPEO DE CUOTAS
            foreach (Cuota cuota in cuotas)
            {
                Movimiento mov = new Movimiento();
                mov.idReferencia = cuota.Gasto.IdGasto;
                mov.Fecha = cuota.Vencimiento;
                mov.Descripcion = cuota.Gasto.Descripcion + " (cuota " + cuota.NumeroCuota + "/" + cuota.TotalCuotas + ")";
                mov.Categoria = cuota.Gasto.Categoria.Nombre;
                mov.Tipo = "Gasto";
                mov.Monto = cuota.Monto;
                mov.Estado = cuota.Estado.ToString();

                movimientos.Add(mov);
            }

            // 5. DEVOLVER ORDENADO
            return movimientos.OrderByDescending(x => x.Fecha).ToList();
        }
    }
}

