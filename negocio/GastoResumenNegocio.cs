using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace negocio
{
    public class GastoResumenNegocio
    {
        public List<GastoResumen> ObtenerGastosDelMes(int idUsuario)
        {
            List<GastoResumen> listaGastos = new List<GastoResumen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                SELECT 
                    Movimientos.Categoria, 
                    SUM(Movimientos.Monto) AS MontoTotal
                FROM 
                (
                    -- BLOQUE 1: Gastos normales de 1 pago
                    SELECT 
                        c.Nombre AS Categoria, 
                        g.MontoPesos AS Monto
                    FROM Gasto g
                    INNER JOIN Categoria c ON g.IdCategoria = c.IdCategoria
                    WHERE g.IdUsuario = @idUsuario
                      AND g.Estado = 1
                      AND c.Tipo = 2 -- Solo gastos
                      AND ISNULL(g.EsEnCuotas, 0) = 0
                      AND MONTH(g.Fecha) = MONTH(GETDATE()) 
                      AND YEAR(g.Fecha) = YEAR(GETDATE())

                    UNION ALL

                    -- BLOQUE 2: Cuotas del mes
                    SELECT 
                        c.Nombre AS Categoria, 
                        cu.Monto AS Monto
                    FROM Cuota cu
                    INNER JOIN Gasto g ON g.IdGasto = cu.IdGasto
                    INNER JOIN Categoria c ON g.IdCategoria = c.IdCategoria
                    WHERE g.IdUsuario = @idUsuario
                      AND g.Estado = 1
                      AND c.Tipo = 2 -- Solo gastos
                      AND ISNULL(g.EsEnCuotas, 0) = 1
                      AND MONTH(cu.Vencimiento) = MONTH(GETDATE()) 
                      AND YEAR(cu.Vencimiento) = YEAR(GETDATE())
                ) AS Movimientos
                GROUP BY Movimientos.Categoria
                HAVING SUM(Movimientos.Monto) > 0
            ");
                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    GastoResumen aux = new GastoResumen();

                    aux.Categoria = (string)datos.Lector["Categoria"];
                    aux.Monto = Convert.ToDecimal(datos.Lector["MontoTotal"]);

                    listaGastos.Add(aux);
                }

                return listaGastos;
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
