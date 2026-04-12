using dominio;
using System;
using System.Collections.Generic;
using negocio; 

namespace negocio
{
    public class IngresoNegocio
    {
        public List<Ingreso> ListarPorUsuario(int idUsuario)
        {
            List<Ingreso> lista = new List<Ingreso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT I.IdIngreso, I.Descripcion, I.Fecha, I.Monto, I.IdCategoria, I.IdUsuario, I.Estado, C.Nombre AS NombreCategoria " +
                                  "FROM INGRESO I " +
                                  "INNER JOIN CATEGORIA C ON I.IdCategoria = C.IdCategoria " +
                                  "WHERE I.IdUsuario = @idUsuario AND I.Estado = 1");

                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Ingreso ingreso = new Ingreso();

                    ingreso.IdIngreso = (int)datos.Lector["IdIngreso"];
                    ingreso.Descripcion = (string)datos.Lector["Descripcion"];
                    ingreso.Fecha = (DateTime)datos.Lector["Fecha"];
                    ingreso.Monto = (decimal)datos.Lector["Monto"];
                    ingreso.Estado = (bool)datos.Lector["Estado"];

                    ingreso.Usuario = new Usuario();
                    ingreso.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    ingreso.Categoria = new Categoria();
                    ingreso.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    ingreso.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    lista.Add(ingreso);
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

        public void AgregarIngreso(Ingreso nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nuevo == null)
                    throw new Exception("El ingreso no puede ser nulo.");

                if (string.IsNullOrWhiteSpace(nuevo.Descripcion))
                    throw new Exception("La descripción del ingreso es obligatoria.");

                if (nuevo.Monto <= 0)
                    throw new Exception("El monto debe ser mayor a cero.");

                if (nuevo.Categoria == null || nuevo.Categoria.IdCategoria <= 0)
                    throw new Exception("Debe seleccionar una categoría.");

                if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0)
                    throw new Exception("El ingreso debe estar asociado a un usuario.");

                datos.setConsulta("INSERT INTO INGRESO (Descripcion, Fecha, Monto, IdCategoria, IdUsuario, Estado) " +
                                  "VALUES (@descripcion, @fecha, @monto, @idCategoria, @idUsuario, @estado)");

                datos.setParametro("@descripcion", nuevo.Descripcion.Trim());
                datos.setParametro("@fecha", nuevo.Fecha);
                datos.setParametro("@monto", nuevo.Monto);
                datos.setParametro("@idCategoria", nuevo.Categoria.IdCategoria);
                datos.setParametro("@idUsuario", nuevo.Usuario.IdUsuario);
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

        public void EliminarIngreso(int idIngreso)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE INGRESO SET Estado = 0 WHERE IdIngreso = @idIngreso");
                datos.setParametro("@idIngreso", idIngreso);
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

        public List<Ingreso> ListarPorUsuarioMesActual(int idUsuario,int mes,int anio)
        {
            List<Ingreso> lista = new List<Ingreso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT I.IdIngreso, I.Descripcion, I.Fecha, I.Monto, I.IdCategoria, I.IdUsuario, I.Estado, C.Nombre AS NombreCategoria " +
                                  "FROM INGRESO I " +
                                  "INNER JOIN CATEGORIA C ON I.IdCategoria = C.IdCategoria " +
                                  "WHERE I.IdUsuario = @idUsuario " +
                                  "AND I.Estado = 1 " +
                                  "AND MONTH(I.Fecha) = @mes " +
                                  "AND YEAR(I.Fecha) = @anio");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@mes", mes);
                datos.setParametro("@anio", anio);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Ingreso ingreso = new Ingreso();

                    ingreso.IdIngreso = (int)datos.Lector["IdIngreso"];
                    ingreso.Descripcion = (string)datos.Lector["Descripcion"];
                    ingreso.Fecha = (DateTime)datos.Lector["Fecha"];
                    ingreso.Monto = (decimal)datos.Lector["Monto"];
                    ingreso.Estado = (bool)datos.Lector["Estado"];

                    ingreso.Usuario = new Usuario();
                    ingreso.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    ingreso.Categoria = new Categoria();
                    ingreso.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    ingreso.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    lista.Add(ingreso);
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

        public List<Ingreso> ListarPorHogarMesActual(int idHogar,int mes, int anio)
        {
            List<Ingreso> lista = new List<Ingreso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT I.IdIngreso, I.Descripcion, I.idHogar, I.Fecha, I.Monto, I.IdCategoria, I.IdUsuario, I.Estado, C.Nombre AS NombreCategoria " +
                                  "FROM INGRESO I " +
                                  "INNER JOIN CATEGORIA C ON I.IdCategoria = C.IdCategoria " +
                                  "WHERE I.IdHogar = @idHogar " +
                                  "AND I.Estado = 1 " +
                                  "AND MONTH(I.Fecha) = @mes " +
                                  "AND YEAR(I.Fecha) = @anio");

                datos.setParametro("@idHogar", idHogar);
                datos.setParametro("@mes", mes);
                datos.setParametro("@anio", anio);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Ingreso ingreso = new Ingreso();

                    ingreso.IdIngreso = (int)datos.Lector["IdIngreso"];
                    ingreso.Descripcion = (string)datos.Lector["Descripcion"];
                    ingreso.Fecha = (DateTime)datos.Lector["Fecha"];
                    ingreso.Monto = (decimal)datos.Lector["Monto"];
                    ingreso.Estado = (bool)datos.Lector["Estado"];

                    ingreso.Hogar = new Hogar();
                    ingreso.Hogar.IdHogar = (int)datos.Lector["IdHogar"];

                    ingreso.Categoria = new Categoria();
                    ingreso.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    ingreso.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    lista.Add(ingreso);
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

        public decimal TotalIngresosMesActual(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT ISNULL(SUM(Monto), 0) AS Total " +
                                  "FROM INGRESO " +
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

        public decimal TotalIngresosMesActualHogar(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT ISNULL(SUM(Monto), 0) AS Total " +
                                  "FROM INGRESO " +
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

        public List<Ingreso> ListarPorUsuarioPorMes(int idUsuario, int mes, int anio)
        {
            List<Ingreso> lista = new List<Ingreso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT I.IdIngreso, I.Descripcion, I.Fecha, I.Monto, I.IdCategoria, I.IdUsuario, I.Estado, C.Nombre AS NombreCategoria " +
                                  "FROM INGRESO I " +
                                  "INNER JOIN CATEGORIA C ON I.IdCategoria = C.IdCategoria " +
                                  "WHERE I.IdUsuario = @idUsuario " +
                                  "AND I.Estado = 1 " +
                                  "AND MONTH(I.Fecha) = @mes " +
                                  "AND YEAR(I.Fecha) = @anio");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@mes", mes);
                datos.setParametro("@anio", anio);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Ingreso ingreso = new Ingreso();

                    ingreso.IdIngreso = (int)datos.Lector["IdIngreso"];
                    ingreso.Descripcion = (string)datos.Lector["Descripcion"];
                    ingreso.Fecha = (DateTime)datos.Lector["Fecha"];
                    ingreso.Monto = (decimal)datos.Lector["Monto"];
                    ingreso.Estado = (bool)datos.Lector["Estado"];

                    ingreso.Usuario = new Usuario();
                    ingreso.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    ingreso.Categoria = new Categoria();
                    ingreso.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    ingreso.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    lista.Add(ingreso);
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
