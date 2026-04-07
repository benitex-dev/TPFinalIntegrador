using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PresupuestoCategoriaNegocio
    {
        public void Guardar(PresupuestoCategoria presupuesto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Verifico si ya existe para ese mes/año/categoría
                datos.setConsulta("SELECT IdPresupuesto FROM PRESUPUESTO_CATEGORIA " +
                                  "WHERE IdCategoria = @idCategoria AND IdUsuario = @idUsuario " +
                                  "AND Mes = @mes AND Anio = @anio");
                datos.setParametro("@idCategoria", presupuesto.Categoria.IdCategoria);
                datos.setParametro("@idUsuario", presupuesto.Usuario.IdUsuario);
                datos.setParametro("@mes", presupuesto.Mes);
                datos.setParametro("@anio", presupuesto.Anio);
                datos.ejecutarLectura();

                bool existe = datos.Lector.Read();
                int idExistente = existe ? (int)datos.Lector["IdPresupuesto"] : 0;
                datos.cerrarConexion();

                if (existe)
                {
                    datos = new AccesoDatos();
                    datos.setConsulta("UPDATE PRESUPUESTO_CATEGORIA SET MontoPresupuestado = @monto " +
                                      "WHERE IdPresupuesto = @id");
                    datos.setParametro("@monto", presupuesto.MontoPresupuestado);
                    datos.setParametro("@id", idExistente);
                    datos.ejecutarAccion();
                }
                else
                {
                    datos = new AccesoDatos();
                    datos.setConsulta("INSERT INTO PRESUPUESTO_CATEGORIA (IdCategoria, IdUsuario, Mes, Anio, MontoPresupuestado) " +
                                      "VALUES (@idCategoria, @idUsuario, @mes, @anio, @monto)");
                    datos.setParametro("@idCategoria", presupuesto.Categoria.IdCategoria);
                    datos.setParametro("@idUsuario", presupuesto.Usuario.IdUsuario);
                    datos.setParametro("@mes", presupuesto.Mes);
                    datos.setParametro("@anio", presupuesto.Anio);
                    datos.setParametro("@monto", presupuesto.MontoPresupuestado);
                    datos.ejecutarAccion();
                }
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
        public List<PresupuestoCategoria> ListarPorUsuarioYMes(int idUsuario, int mes, int anio)
        {
            List<PresupuestoCategoria> lista = new List<PresupuestoCategoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Trae todas las categorías de gasto del usuario
                // y hace LEFT JOIN con presupuesto para ese mes
                // y calcula el gasto real de ese mes por categoría
                datos.setConsulta(@"
                      SELECT
                          C.IdCategoria,
                          C.Nombre AS NombreCategoria,
                          ISNULL(P.IdPresupuesto, 0) AS IdPresupuesto,
                          ISNULL(P.MontoPresupuestado, 0) AS MontoPresupuestado,
                          ISNULL(SUM(G.MontoPesos), 0) AS GastoReal
                      FROM CATEGORIA C
                      LEFT JOIN PRESUPUESTO_CATEGORIA P
                          ON P.IdCategoria = C.IdCategoria
                          AND P.IdUsuario = @idUsuario
                          AND P.Mes = @mes
                          AND P.Anio = @anio
                      LEFT JOIN GASTO G
                          ON G.IdCategoria = C.IdCategoria
                          AND G.IdUsuario = @idUsuario
                          AND G.Estado = 1
                          AND MONTH(G.Fecha) = @mes
                          AND YEAR(G.Fecha) = @anio
                      WHERE C.IdUsuario = @idUsuario
                          AND C.Tipo = 2
                          AND C.Estado = 1
                      GROUP BY C.IdCategoria, C.Nombre, P.IdPresupuesto, P.MontoPresupuestado");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@mes", mes);
                datos.setParametro("@anio", anio);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    PresupuestoCategoria item = new PresupuestoCategoria();

                    item.IdPresupuesto = (int)datos.Lector["IdPresupuesto"];
                    item.Mes = mes;
                    item.Anio = anio;
                    item.MontoPresupuestado = (decimal)datos.Lector["MontoPresupuestado"];
                    item.GastoReal = (decimal)datos.Lector["GastoReal"];

                    item.Categoria = new Categoria();
                    item.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    item.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    item.Usuario = new Usuario();
                    item.Usuario.IdUsuario = idUsuario;

                    lista.Add(item);
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
        }
}
