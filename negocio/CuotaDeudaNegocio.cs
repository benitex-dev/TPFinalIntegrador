using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CuotaDeudaNegocio
    {

        // ALTA
        public void AgregarCuota(CuotaDeuda nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nueva == null)
                    throw new Exception("La cuota no puede ser nula.");

                if (nueva.Deuda == null || nueva.Deuda.IdDeuda <= 0)
                    throw new Exception("La cuota debe estar asociada a una deuda.");

                if (nueva.NumeroCuota <= 0)
                    throw new Exception("El número de cuota debe ser mayor a cero.");

                if (nueva.Monto <= 0)
                    throw new Exception("El monto de la cuota debe ser mayor a cero.");

                if (nueva.FechaVencimiento == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha de vencimiento válida.");

                datos.setConsulta(@"INSERT INTO CUOTA_DEUDA
                      (IdDeuda, NumeroCuota, Monto, FechaVencimiento, FechaPago, Estado)
                      VALUES (@idDeuda, @numeroCuota, @monto, @fechaVencimiento, @fechaPago, @estado)");

                datos.setParametro("@idDeuda", nueva.Deuda.IdDeuda);
                datos.setParametro("@numeroCuota", nueva.NumeroCuota);
                datos.setParametro("@monto", nueva.Monto);
                datos.setParametro("@fechaVencimiento", nueva.FechaVencimiento);
                datos.setParametro("@fechaPago", (object)nueva.FechaPago ?? DBNull.Value);
                datos.setParametro("@estado", (int)nueva.Estado);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        // LISTAR POR DEUDA
        //public List<CuotaDeuda> ListarPorDeuda(int idDeuda)
        //{ 
        
       // }
        // MARCAR COMO PAGADA
        public void MarcarPagada(int idCuotaDeuda)
        { 
        
        
        }


     }
}
