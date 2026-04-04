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
        //LISTAR POR DEUDA
        public List<CuotaDeuda> ListarPorDeuda(int idDeuda)
        {
            List<CuotaDeuda> lista = new List<CuotaDeuda>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT IdCuotaDeuda, IdDeuda, NumeroCuota, Monto,
                      FechaVencimiento, FechaPago, Estado
                      FROM CUOTA_DEUDA
                      WHERE IdDeuda = @idDeuda
                      ORDER BY NumeroCuota");

                datos.setParametro("@idDeuda", idDeuda);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    CuotaDeuda cuota = new CuotaDeuda();

                    cuota.IdCuotaDeuda = (int)datos.Lector["IdCuotaDeuda"];
                    cuota.Deuda = new Deuda();
                    cuota.Deuda.IdDeuda = (int)datos.Lector["IdDeuda"];
                    cuota.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.FechaVencimiento = (DateTime)datos.Lector["FechaVencimiento"];
                    cuota.FechaPago = datos.Lector["FechaPago"] is DBNull ? (DateTime?)null : (DateTime)datos.Lector["FechaPago"];
                    cuota.Estado = (EstadoCuota)(int)datos.Lector["Estado"];

                    lista.Add(cuota);
                }

                return lista;
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
        // MARCAR COMO PAGADA
        public void MarcarPagada(int idCuotaDeuda)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idCuotaDeuda <= 0)
                    throw new Exception("Id de cuota inválido.");

                datos.setConsulta(@"UPDATE CUOTA_DEUDA
                      SET Estado = @estado, FechaPago = @fechaPago
                      WHERE IdCuotaDeuda = @idCuotaDeuda");

                datos.setParametro("@idCuotaDeuda", idCuotaDeuda);
                datos.setParametro("@estado", (int)EstadoCuota.Pagada);
                datos.setParametro("@fechaPago", DateTime.Today);

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


     }
}
