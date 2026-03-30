using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class GastoNegocio
    {
        public List<Gasto> ListarPorUsuario(int idUsuario)
        {
            List<Gasto> lista = new List<Gasto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT G.IdGasto, G.Fecha, G.MontoPesos, G.Moneda, G.IdCategoria, G.Descripcion, " +
                                  "G.IdMedioPago, G.IdUsuario, G.MontoUSD, G.Cotizacion, G.Estado, " +
                                  "C.Nombre AS NombreCategoria, MP.Descripcion AS NombreMedioPago " +
                                  "FROM GASTO G " +
                                  "INNER JOIN CATEGORIA C ON G.IdCategoria = C.IdCategoria " +
                                  "INNER JOIN MEDIOPAGO MP ON G.IdMedioPago = MP.IdMedioPago " +
                                  "WHERE G.IdUsuario = @idUsuario AND G.Estado = 1");

                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Gasto gasto = new Gasto();

                    gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    gasto.Fecha = (DateTime)datos.Lector["Fecha"];
                    gasto.MontoPesos = (decimal)datos.Lector["MontoPesos"];
                    gasto.Moneda = (Moneda)(int)datos.Lector["Moneda"];
                    gasto.Descripcion = (string)datos.Lector["Descripcion"];
                    gasto.Estado = (bool)datos.Lector["Estado"];

                    gasto.Usuario = new Usuario();
                    gasto.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    gasto.Categoria = new Categoria();
                    gasto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    gasto.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    gasto.MedioDePago = new MedioPago();
                    gasto.MedioDePago.IdMedioPago = (int)datos.Lector["IdMedioPago"];
                    gasto.MedioDePago.Descripcion = (string)datos.Lector["NombreMedioPago"];

                    if (datos.Lector["MontoUSD"] != DBNull.Value)
                        gasto.MontoUSD = (decimal)datos.Lector["MontoUSD"];

                    if (datos.Lector["Cotizacion"] != DBNull.Value)
                        gasto.Cotizacion = (decimal)datos.Lector["Cotizacion"];

                    lista.Add(gasto);
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

        public void AgregarGasto(Gasto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nuevo == null)
                    throw new Exception("El gasto no puede ser nulo.");

                if (string.IsNullOrWhiteSpace(nuevo.Descripcion))
                    throw new Exception("La descripción del gasto es obligatoria.");

                if (nuevo.MontoPesos <= 0)
                    throw new Exception("El monto en pesos debe ser mayor a cero.");

                if (nuevo.Categoria == null || nuevo.Categoria.IdCategoria <= 0)
                    throw new Exception("Debe seleccionar una categoría.");

                if (nuevo.MedioDePago == null || nuevo.MedioDePago.IdMedioPago <= 0)
                    throw new Exception("Debe seleccionar un medio de pago.");

                if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0)
                    throw new Exception("El gasto debe estar asociado a un usuario.");

                if (nuevo.Moneda != Moneda.ARS)
                {
                    if (nuevo.MontoUSD <= 0)
                        throw new Exception("Debe ingresar el monto en moneda extranjera.");

                    if (nuevo.Cotizacion <= 0)
                        throw new Exception("Debe ingresar una cotización válida.");
                }

                datos.setConsulta("INSERT INTO GASTO (Fecha, MontoPesos, Moneda, IdCategoria, Descripcion, IdMedioPago, IdUsuario, MontoUSD, Cotizacion, Estado) " +
                                  "VALUES (@fecha, @montoPesos, @moneda, @idCategoria, @descripcion, @idMedioPago, @idUsuario, @montoUSD, @cotizacion, @estado)");

                datos.setParametro("@fecha", nuevo.Fecha);
                datos.setParametro("@montoPesos", nuevo.MontoPesos);
                datos.setParametro("@moneda", (int)nuevo.Moneda);
                datos.setParametro("@idCategoria", nuevo.Categoria.IdCategoria);
                datos.setParametro("@descripcion", nuevo.Descripcion.Trim());
                datos.setParametro("@idMedioPago", nuevo.MedioDePago.IdMedioPago);
                datos.setParametro("@idUsuario", nuevo.Usuario.IdUsuario);

                if (nuevo.Moneda == Moneda.ARS)
                {
                    datos.setParametro("@montoUSD", DBNull.Value);
                    datos.setParametro("@cotizacion", DBNull.Value);
                }
                else
                {
                    datos.setParametro("@montoUSD", nuevo.MontoUSD);
                    datos.setParametro("@cotizacion", nuevo.Cotizacion);
                }

                datos.setParametro("@estado", nuevo.Estado);

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

        public void EliminarGasto(int idGasto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE GASTO SET Estado = 0 WHERE IdGasto = @idGasto");
                datos.setParametro("@idGasto", idGasto);

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

        public decimal TotalGastosMesActual(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT ISNULL(SUM(MontoPesos), 0) AS Total " +
                                  "FROM GASTO " +
                                  "WHERE IdUsuario = @idUsuario " +
                                  "AND Estado = 1 " +
                                  "AND MONTH(Fecha) = MONTH(GETDATE()) " +
                                  "AND YEAR(Fecha) = YEAR(GETDATE())");

                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (decimal)datos.Lector["Total"];

                return 0;
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

        public decimal TotalGastosMesActualHogar(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT ISNULL(SUM(MontoPesos), 0) AS Total " +
                                  "FROM GASTO " +
                                  "WHERE IdHogar = @idHogar " +
                                  "AND Estado = 1 " +
                                  "AND MONTH(Fecha) = MONTH(GETDATE()) " +
                                  "AND YEAR(Fecha) = YEAR(GETDATE())");

                datos.setParametro("@idHogar", idHogar);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (decimal)datos.Lector["Total"];

                return 0;
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

        public List<Gasto> ListarPorUsuarioMesActual(int idUsuario)
        {
            List<Gasto> lista = new List<Gasto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT G.IdGasto, G.Fecha, G.MontoPesos, G.Moneda, G.IdCategoria, G.Descripcion, " +
                                  "G.IdMedioPago, G.IdUsuario, G.MontoUSD, G.Cotizacion, G.Estado, " +
                                  "C.Nombre AS NombreCategoria, MP.Descripcion AS NombreMedioPago " +
                                  "FROM GASTO G " +
                                  "INNER JOIN CATEGORIA C ON G.IdCategoria = C.IdCategoria " +
                                  "INNER JOIN MEDIOPAGO MP ON G.IdMedioPago = MP.IdMedioPago " +
                                  "WHERE G.IdUsuario = @idUsuario " +
                                  "AND G.Estado = 1 " +
                                  "AND MONTH(G.Fecha) = MONTH(GETDATE()) " +
                                  "AND YEAR(G.Fecha) = YEAR(GETDATE())");

                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Gasto gasto = new Gasto();

                    gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    gasto.Fecha = (DateTime)datos.Lector["Fecha"];
                    gasto.MontoPesos = (decimal)datos.Lector["MontoPesos"];
                    gasto.Moneda = (Moneda)(int)datos.Lector["Moneda"];
                    gasto.Descripcion = (string)datos.Lector["Descripcion"];
                    gasto.Estado = (bool)datos.Lector["Estado"];

                    gasto.Usuario = new Usuario();
                    gasto.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    gasto.Categoria = new Categoria();
                    gasto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    gasto.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    gasto.MedioDePago = new MedioPago();
                    gasto.MedioDePago.IdMedioPago = (int)datos.Lector["IdMedioPago"];
                    gasto.MedioDePago.Descripcion = (string)datos.Lector["NombreMedioPago"];

                    if (datos.Lector["MontoUSD"] != DBNull.Value)
                        gasto.MontoUSD = (decimal)datos.Lector["MontoUSD"];

                    if (datos.Lector["Cotizacion"] != DBNull.Value)
                        gasto.Cotizacion = (decimal)datos.Lector["Cotizacion"];

                    lista.Add(gasto);
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

        public List<Gasto> ListarPorHogarMesActual(int idHogar)
        {
            List<Gasto> lista = new List<Gasto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT G.IdGasto, G.Fecha, G.MontoPesos, G.Moneda, G.IdCategoria, G.Descripcion, " +
                                  "G.IdMedioPago, G.IdUsuario, G.IdHogar, G.MontoUSD, G.Cotizacion, G.Estado, " +
                                  "C.Nombre AS NombreCategoria, MP.Descripcion AS NombreMedioPago " +
                                  "FROM GASTO G " +
                                  "INNER JOIN CATEGORIA C ON G.IdCategoria = C.IdCategoria " +
                                  "INNER JOIN MEDIOPAGO MP ON G.IdMedioPago = MP.IdMedioPago " +
                                  "WHERE G.IdHogar = @idHogar " +
                                  "AND G.Estado = 1 " +
                                  "AND MONTH(G.Fecha) = MONTH(GETDATE()) " +
                                  "AND YEAR(G.Fecha) = YEAR(GETDATE())");

                datos.setParametro("@idHogar", idHogar);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Gasto gasto = new Gasto();

                    gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    gasto.Fecha = (DateTime)datos.Lector["Fecha"];
                    gasto.MontoPesos = (decimal)datos.Lector["MontoPesos"];
                    gasto.Moneda = (Moneda)(int)datos.Lector["Moneda"];
                    gasto.Descripcion = (string)datos.Lector["Descripcion"];
                    gasto.Estado = (bool)datos.Lector["Estado"];

                    gasto.Hogar = new Hogar();
                    gasto.Hogar.IdHogar = (int)datos.Lector["IdUsuario"];

                    gasto.Categoria = new Categoria();
                    gasto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    gasto.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    gasto.MedioDePago = new MedioPago();
                    gasto.MedioDePago.IdMedioPago = (int)datos.Lector["IdMedioPago"];
                    gasto.MedioDePago.Descripcion = (string)datos.Lector["NombreMedioPago"];

                    if (datos.Lector["MontoUSD"] != DBNull.Value)
                        gasto.MontoUSD = (decimal)datos.Lector["MontoUSD"];

                    if (datos.Lector["Cotizacion"] != DBNull.Value)
                        gasto.Cotizacion = (decimal)datos.Lector["Cotizacion"];

                    lista.Add(gasto);
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
        public List<Gasto> ListarPorUsuarioPorMes(int idUsuario, int mes, int anio)
        {
            List<Gasto> lista = new List<Gasto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT G.IdGasto, G.Descripcion, G.Fecha, G.MontoPesos, G.MontoUSD, G.Cotizacion, G.Moneda, " +
                    "G.IdCategoria, G.IdMedioPago, G.IdUsuario, G.Estado, C.Nombre AS NombreCategoria, M.Descripcion AS MedioPagoDescripcion " +
                    "FROM GASTO G " +
                    "INNER JOIN CATEGORIA C ON G.IdCategoria = C.IdCategoria " +
                    "LEFT JOIN MEDIOPAGO M ON G.IdMedioPago = M.IdMedioPago " +
                    "WHERE G.IdUsuario = @idUsuario AND G.Estado = 1 AND MONTH(G.Fecha) = @mes AND YEAR(G.Fecha) = @anio");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@mes", mes);
                datos.setParametro("@anio", anio);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Gasto gasto = new Gasto();

                    gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    gasto.Descripcion = (string)datos.Lector["Descripcion"];
                    gasto.Fecha = (DateTime)datos.Lector["Fecha"];
                    gasto.MontoPesos = datos.Lector["MontoPesos"] != DBNull.Value ? (decimal)datos.Lector["MontoPesos"] : 0m;

                    if (datos.Lector["MontoUSD"] != DBNull.Value)
                        gasto.MontoUSD = (decimal)datos.Lector["MontoUSD"];

                    if (datos.Lector["Cotizacion"] != DBNull.Value)
                        gasto.Cotizacion = (decimal)datos.Lector["Cotizacion"];

                    gasto.Moneda = (Moneda)Convert.ToInt32(datos.Lector["Moneda"]);
                    gasto.Estado = (bool)datos.Lector["Estado"];

                    gasto.Usuario = new Usuario();
                    gasto.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    gasto.Categoria = new Categoria();
                    gasto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    gasto.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    gasto.MedioDePago = new MedioPago();
                    if (datos.Lector["IdMedioPago"] != DBNull.Value)
                        gasto.MedioDePago.IdMedioPago = (int)datos.Lector["IdMedioPago"];
                    gasto.MedioDePago.Descripcion = datos.Lector["MedioPagoDescripcion"] != DBNull.Value
                        ? (string)datos.Lector["MedioPagoDescripcion"] : null;

                    lista.Add(gasto);
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

