using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MetaResumenNegocio
    {
        public List<MetaResumen> obtenerMetasPorUsuario(int idUsuario)
        {
            // 1. Creamos la lista vacía al principio
            List<MetaResumen> listaMetas = new List<MetaResumen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT 
            m.IdMeta, 
            m.Nombre, 
            m.MontoObjetivo, 
            m.FechaObjetivo, 
            ISNULL(SUM(a.Monto), 0) AS MontoActual, 
            CAST((ISNULL(SUM(a.Monto), 0) / m.MontoObjetivo) * 100 AS INT) AS Porcentaje 
            FROM META_AHORRO m 
            LEFT JOIN APORTE_META a ON m.IdMeta = a.IdMeta 
            WHERE m.IdUsuario = @idUsuario AND m.Estado = 1 
            GROUP BY m.IdMeta, m.Nombre, m.MontoObjetivo, m.FechaObjetivo");

                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    MetaResumen aux = new MetaResumen();

                    aux.IdMeta = (int)datos.Lector["IdMeta"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.MontoObjetivo = (decimal)datos.Lector["MontoObjetivo"];
                    aux.MontoActual = (decimal)datos.Lector["MontoActual"];
                    aux.Porcentaje = (int)datos.Lector["Porcentaje"];

                    if (!(datos.Lector["FechaObjetivo"] is DBNull))
                    {
                        aux.FechaObjetivo = (DateTime)datos.Lector["FechaObjetivo"];
                    }
                    listaMetas.Add(aux);
                }
                return listaMetas;
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
