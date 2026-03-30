using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CuotaNegocio
    {
        
        // ALTA
       
        public void AgregarCuota(Cuota nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nueva == null)
                    throw new Exception("La cuota no puede ser nula.");

                if (nueva.Gasto == null || nueva.Gasto.IdGasto <= 0)
                    throw new Exception("La cuota debe estar asociada a un gasto.");

                if (nueva.NumeroCuota <= 0)
                    throw new Exception("El número de cuota debe ser mayor a cero.");

                if (nueva.TotalCuotas <= 0)
                    throw new Exception("Debe indicar la cantidad total de cuotas.");

                if (nueva.Monto <= 0)
                    throw new Exception("El monto de la cuota debe ser mayor a cero.");

                if (nueva.Vencimiento == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha de vencimiento válida.");

                datos.setConsulta(@"INSERT INTO CUOTA 
                (IdGasto, NumeroCuota, TotalCuotas, Monto, Vencimiento, Estado) 
                VALUES (@idGasto, @numeroCuota, @totalCuotas, @monto, @vencimiento, @estado)");

                datos.setParametro("@idGasto", nueva.Gasto.IdGasto);
                datos.setParametro("@numeroCuota", nueva.NumeroCuota);
                datos.setParametro("@totalCuotas", nueva.TotalCuotas);
                datos.setParametro("@monto", nueva.Monto);
                datos.setParametro("@vencimiento", nueva.Vencimiento);
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

        
        // BAJA (lógica)
        
        public void EliminarLogico(int idCuota)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idCuota <= 0)
                    throw new Exception("Id de cuota inválido.");

                datos.setConsulta("UPDATE CUOTA SET Estado = @estado WHERE IdCuota = @idCuota");
                datos.setParametro("@idCuota", idCuota);
                datos.setParametro("@estado", (int)EstadoCuota.Vencida); // o el que quieras usar como "inactivo"

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

        
        // LISTAR POR GASTO
       
        public List<Cuota> ListarPorGasto(int idGasto)
        {
            List<Cuota> lista = new List<Cuota>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT IdCuota, IdGasto, NumeroCuota, TotalCuotas, Monto, Vencimiento, Estado 
                                FROM CUOTA 
                                WHERE IdGasto = @idGasto");

                datos.setParametro("@idGasto", idGasto);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cuota cuota = new Cuota();

                    cuota.IdCuota = (int)datos.Lector["IdCuota"];

                    cuota.Gasto = new Gasto();
                    cuota.Gasto.IdGasto = (int)datos.Lector["IdGasto"];

                    cuota.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    cuota.TotalCuotas = (int)datos.Lector["TotalCuotas"];
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
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

        
        // LISTAR GENERAL 
       
        public List<Cuota> Listar(int idGasto = 0, int numeroCuota = 0, DateTime? vencimiento = null)
        {
            List<Cuota> lista = new List<Cuota>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT IdCuota, IdGasto, NumeroCuota, TotalCuotas, Monto, Vencimiento, Estado 
                                FROM CUOTA WHERE 1=1";

                if (idGasto > 0)
                    consulta += " AND IdGasto = @idGasto";

                if (numeroCuota > 0)
                    consulta += " AND NumeroCuota = @numeroCuota";

                if (vencimiento != null)
                    consulta += " AND CAST(Vencimiento AS DATE) = @vencimiento";

                datos.setConsulta(consulta);

                if (idGasto > 0)
                    datos.setParametro("@idGasto", idGasto);

                if (numeroCuota > 0)
                    datos.setParametro("@numeroCuota", numeroCuota);

                if (vencimiento != null)
                    datos.setParametro("@vencimiento", vencimiento.Value.Date);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cuota cuota = new Cuota();

                    cuota.IdCuota = (int)datos.Lector["IdCuota"];

                    cuota.Gasto = new Gasto();
                    cuota.Gasto.IdGasto = (int)datos.Lector["IdGasto"];

                    cuota.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    cuota.TotalCuotas = (int)datos.Lector["TotalCuotas"];
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
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

        public List<Cuota> ListarPorUsuarioMesActual(int idUsuario)
        {
            List<Cuota> lista = new List<Cuota>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT 
                C.IdCuota,
                C.IdGasto,
                C.NumeroCuota,
                C.TotalCuotas,
                C.Monto,
                C.Vencimiento,
                C.Estado,
                G.Descripcion,
                G.IdCategoria,
                CAT.Nombre AS NombreCategoria
            FROM CUOTA C
            INNER JOIN GASTO G ON G.IdGasto = C.IdGasto
            INNER JOIN CATEGORIA CAT ON CAT.IdCategoria = G.IdCategoria
            WHERE G.IdUsuario = @idUsuario
              AND G.Estado = 1
              AND MONTH(C.Vencimiento) = MONTH(GETDATE())
              AND YEAR(C.Vencimiento) = YEAR(GETDATE())");

                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cuota cuota = new Cuota();

                    cuota.IdCuota = (int)datos.Lector["IdCuota"];

                    cuota.Gasto = new Gasto();
                    cuota.Gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    cuota.Gasto.Descripcion = (string)datos.Lector["Descripcion"];

                    cuota.Gasto.Categoria = new Categoria();
                    cuota.Gasto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    cuota.Gasto.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    cuota.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    cuota.TotalCuotas = (int)datos.Lector["TotalCuotas"];
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
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

        public List<Cuota> ListarPorUsuarioPorMes(int idUsuario, int mes, int anio)
        {
            List<Cuota> lista = new List<Cuota>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT 
                C.IdCuota,
                C.IdGasto,
                C.NumeroCuota,
                C.TotalCuotas,
                C.Monto,
                C.Vencimiento,
                C.Estado,
                G.Descripcion,
                G.IdCategoria,
                CAT.Nombre AS NombreCategoria
            FROM CUOTA C
            INNER JOIN GASTO G ON G.IdGasto = C.IdGasto
            INNER JOIN CATEGORIA CAT ON CAT.IdCategoria = G.IdCategoria
            WHERE G.IdUsuario = @idUsuario
              AND G.Estado = 1
              AND MONTH(C.Vencimiento) = @mes
              AND YEAR(C.Vencimiento) = @anio");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@mes", mes);
                datos.setParametro("@anio", anio);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cuota cuota = new Cuota();

                    cuota.IdCuota = (int)datos.Lector["IdCuota"];

                    cuota.Gasto = new Gasto();
                    cuota.Gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    cuota.Gasto.Descripcion = (string)datos.Lector["Descripcion"];

                    cuota.Gasto.Categoria = new Categoria();
                    cuota.Gasto.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    cuota.Gasto.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

                    cuota.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    cuota.TotalCuotas = (int)datos.Lector["TotalCuotas"];
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
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

        


        // MODIFICAR

        public void ModificarCuota(Cuota cuota)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (cuota == null)
                    throw new Exception("La cuota no existe.");

                if (cuota.IdCuota <= 0)
                    throw new Exception("La cuota debe tener un Id válido.");

                datos.setConsulta(@"UPDATE CUOTA SET 
                IdGasto = @idGasto,
                NumeroCuota = @numeroCuota,
                TotalCuotas = @totalCuotas,
                Monto = @monto,
                Vencimiento = @vencimiento,
                Estado = @estado
                WHERE IdCuota = @idCuota");

                datos.setParametro("@idCuota", cuota.IdCuota);
                datos.setParametro("@idGasto", cuota.Gasto.IdGasto);
                datos.setParametro("@numeroCuota", cuota.NumeroCuota);
                datos.setParametro("@totalCuotas", cuota.TotalCuotas);
                datos.setParametro("@monto", cuota.Monto);
                datos.setParametro("@vencimiento", cuota.Vencimiento);
                datos.setParametro("@estado", (int)cuota.Estado);

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

        
        // EXISTE
        
        public Cuota ExisteCuota(int idCuota)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT IdCuota, IdGasto, NumeroCuota, TotalCuotas, Monto, Vencimiento, Estado 
                                FROM CUOTA 
                                WHERE IdCuota = @idCuota");

                datos.setParametro("@idCuota", idCuota);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Cuota aux = new Cuota();

                    aux.IdCuota = (int)datos.Lector["IdCuota"];

                    aux.Gasto = new Gasto();
                    aux.Gasto.IdGasto = (int)datos.Lector["IdGasto"];

                    aux.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    aux.TotalCuotas = (int)datos.Lector["TotalCuotas"];
                    aux.Monto = (decimal)datos.Lector["Monto"];
                    aux.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
                    aux.Estado = (EstadoCuota)(int)datos.Lector["Estado"];

                    return aux;
                }

                return null;
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
