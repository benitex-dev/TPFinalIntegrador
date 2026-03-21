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
        //ALTA
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

                if (nueva.Monto <= 0)
                    throw new Exception("El monto de la cuota debe ser mayor a cero.");

                if (nueva.Vencimiento == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha de vencimiento válida.");

                datos.setConsulta("INSERT INTO CUOTA (IdGasto, NumeroCuota, Monto, Vencimiento, Estado) " +
                                  "VALUES (@idGasto, @numeroCuota, @monto, @vencimiento, @estado)");

                datos.setParametro("@idGasto", nueva.Gasto.IdGasto);
                datos.setParametro("@numeroCuota", nueva.NumeroCuota);
                datos.setParametro("@monto", nueva.Monto);
                datos.setParametro("@vencimiento", nueva.Vencimiento);
                datos.setParametro("@estado", nueva.Estado);

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
        //BAJA
        public void EliminarLogico(int idCuota)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idCuota <= 0)
                    throw new Exception("Id de cuota inválido.");

                datos.setConsulta("UPDATE CUOTA SET Estado = 0 WHERE IdCuota = @idCuota");
                datos.setParametro("@idCuota", idCuota);

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
        //LISTAR
        public List<Cuota> ListarPorGasto(int idGasto)
        {
            List<Cuota> lista = new List<Cuota>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCuota, IdGasto, NumeroCuota, Monto, Vencimiento, Estado FROM CUOTA WHERE IdGasto = @idGasto");
                datos.setParametro("@idGasto", idGasto);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cuota cuota = new Cuota();

                    cuota.IdCuota = (int)datos.Lector["IdCuota"];
                    
                    cuota.Gasto = new Gasto();
                    cuota.Gasto.IdGasto = (int)datos.Lector["IdGasto"];
                    
                    cuota.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
                    cuota.Estado = (bool)datos.Lector["Estado"];

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

        //listar 2
        public List<Cuota> Listar(int idGasto = 0, int numeroCuota = 0, DateTime? vencimiento = null)
        {
            List<Cuota> lista = new List<Cuota>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdCuota, IdGasto, NumeroCuota, Monto, Vencimiento, Estado FROM CUOTA WHERE 1=1";

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
                    cuota.Monto = (decimal)datos.Lector["Monto"];
                    cuota.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
                    cuota.Estado = (bool)datos.Lector["Estado"];

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

        //MODIFICAR
        public void ModificarCuota(Cuota cuota)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (cuota == null)
                    throw new Exception("La cuota no existe o fue saldada.");

                if (cuota.IdCuota <= 0)
                    throw new Exception("La cuota debe tener un Id válido.");

                if (cuota.Gasto == null || cuota.Gasto.IdGasto <= 0)
                    throw new Exception("La cuota debe estar asociada a un gasto válido.");

                if (cuota.NumeroCuota <= 0)
                    throw new Exception("El número de cuota debe ser mayor a cero.");

                if (cuota.Monto <= 0)
                    throw new Exception("El monto debe ser mayor a cero.");

                if (cuota.Vencimiento == DateTime.MinValue)
                    throw new Exception("Debe ingresar un vencimiento válido.");

                datos.setConsulta("UPDATE CUOTA SET IdGasto = @idGasto, NumeroCuota = @numeroCuota, Monto = @monto, Vencimiento = @vencimiento, Estado = @estado WHERE IdCuota = @idCuota");

                datos.setParametro("@idCuota", cuota.IdCuota);
                datos.setParametro("@idGasto", cuota.Gasto.IdGasto);
                datos.setParametro("@numeroCuota", cuota.NumeroCuota);
                datos.setParametro("@monto", cuota.Monto);
                datos.setParametro("@vencimiento", cuota.Vencimiento);
                datos.setParametro("@estado", cuota.Estado);

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

        //EXISTE
        public Cuota ExisteCuota(int idCuota)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCuota, IdGasto, NumeroCuota, Monto, Vencimiento, Estado FROM CUOTA WHERE IdCuota = @idCuota AND Estado = 1");
                datos.setParametro("@idCuota", idCuota);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Cuota aux = new Cuota();

                    aux.IdCuota = (int)datos.Lector["IdCuota"];

                    aux.Gasto = new Gasto();
                    aux.Gasto.IdGasto = (int)datos.Lector["IdGasto"];

                    aux.NumeroCuota = (int)datos.Lector["NumeroCuota"];
                    aux.Monto = (decimal)datos.Lector["Monto"];
                    aux.Vencimiento = (DateTime)datos.Lector["Vencimiento"];
                    aux.Estado = (bool)datos.Lector["Estado"];

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
