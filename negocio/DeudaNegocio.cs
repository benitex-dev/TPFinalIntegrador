using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace negocio
{
   public class DeudaNegocio
    {
        //ALTA
        public void AgregarDeuda(Deuda nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nueva == null)
                    throw new Exception("La deuda no puede ser nula.");

                if (nueva.Usuario == null || nueva.Usuario.IdUsuario <= 0)
                    throw new Exception("La deuda debe estar asociada a un usuario.");

                if (string.IsNullOrWhiteSpace(nueva.NombreDeudor))
                    throw new Exception("El nombre del deudor es obligatorio.");

                if (string.IsNullOrWhiteSpace(nueva.EmailDeudor))
                    throw new Exception("El email del deudor es obligatorio.");

                if (!nueva.EmailDeudor.Contains("@"))
                    throw new Exception("El email no es válido.");

                if (string.IsNullOrWhiteSpace(nueva.Descripcion))
                    throw new Exception("La descripción es obligatoria.");

                if (nueva.MontoTotal <= 0)
                    throw new Exception("El monto debe ser mayor a cero.");

                if (nueva.Cuotas <= 0)
                    throw new Exception("La cantidad de cuotas debe ser mayor a cero.");

                if (nueva.FechaInicio == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha válida.");

                datos.setConsulta("INSERT INTO DEUDA (IdUsuario, NombreDeudor, EmailDeudor, Descripcion, MontoTotal, Cuotas, FechaInicio, Estado) " +
                                  "VALUES (@idUsuario, @nombreDeudor, @emailDeudor, @descripcion, @montoTotal, @cuotas, @fechaInicio, @estado)" +
                                  "SELECT SCOPE_IDENTITY();");

                datos.setParametro("@idUsuario", nueva.Usuario.IdUsuario);
                datos.setParametro("@nombreDeudor", nueva.NombreDeudor.Trim());
                datos.setParametro("@emailDeudor", nueva.EmailDeudor.Trim());
                datos.setParametro("@descripcion", nueva.Descripcion.Trim());
                datos.setParametro("@montoTotal", nueva.MontoTotal);
                datos.setParametro("@cuotas", nueva.Cuotas);
                datos.setParametro("@fechaInicio", nueva.FechaInicio);
                datos.setParametro("@estado", nueva.Estado);

                nueva.IdDeuda = Convert.ToInt32(datos.ejecutarEscalar());
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
        public void EliminarLogico(int idDeuda)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE DEUDA SET Estado = @estado WHERE IdDeuda = @idDeuda");
                datos.setParametro("@idDeuda", idDeuda);
                datos.setParametro("@estado", (int)EstadoDeuda.Eliminado);
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
        //MODIFICAR
        public void ModificarDeuda(Deuda deuda)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (deuda == null)
                    throw new Exception("La deuda no existe o ya fue saldada");

                if (deuda.Usuario == null || deuda.Usuario.IdUsuario <= 0)
                    throw new Exception("La deuda debe estar asociada a un usuario válido.");

                if (string.IsNullOrWhiteSpace(deuda.NombreDeudor))
                    throw new Exception("El nombre del deudor es obligatorio.");

                if (string.IsNullOrWhiteSpace(deuda.EmailDeudor))
                    throw new Exception("El email del deudor es obligatorio.");

                if (string.IsNullOrWhiteSpace(deuda.Descripcion))
                    throw new Exception("La descripción es obligatoria.");

                if (deuda.MontoTotal <= 0)
                    throw new Exception("El monto total debe ser mayor a cero.");

                if (deuda.Cuotas <= 0)
                    throw new Exception("La cantidad de cuotas debe ser mayor a cero.");

                if (deuda.FechaInicio == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha válida.");


                datos.setConsulta("UPDATE DEUDA SET NombreDeudor = @NombreDeudor, EmailDeudor = @emailDeudor, Descripcion = @descripcion, MontoTotal = @montoTotal, Cuotas = @cuotas, FechaInicio = @fechaInicio, Estado = @estado WHERE IdDeuda = @idDeuda");

                datos.setParametro("@idDeuda", deuda.IdDeuda);
                datos.setParametro("@NombreDeudor", deuda.NombreDeudor);
                datos.setParametro("@emailDeudor", deuda.EmailDeudor.Trim());
                datos.setParametro("@descripcion", deuda.Descripcion.Trim());
                datos.setParametro("@montoTotal", deuda.MontoTotal);
                if (deuda.Cuotas.HasValue)
                    datos.setParametro("@cuotas", deuda.Cuotas.Value);
                else
                    datos.setParametro("@cuotas", DBNull.Value);
                datos.setParametro("@fechaInicio", deuda.FechaInicio);
                datos.setParametro("@estado", deuda.Estado);

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
        public List<Deuda> Listar(int idUsuario = 0, string nombreDeudor = "", EstadoDeuda? estado = null)
        {
            List<Deuda> lista = new List<Deuda>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdDeuda, IdUsuario, NombreDeudor, EmailDeudor, Descripcion, MontoTotal, Cuotas, FechaInicio, Estado FROM DEUDA WHERE 1=1";

                if (idUsuario > 0)
                    consulta += " AND IdUsuario = @idUsuario";

                if (!string.IsNullOrWhiteSpace(nombreDeudor))
                    consulta += " AND NombreDeudor LIKE @nombreDeudor";

                if (estado != null)
                    consulta += " AND Estado = @estado";

                datos.setConsulta(consulta);

                if (idUsuario > 0)
                    datos.setParametro("@idUsuario", idUsuario);

                if (!string.IsNullOrWhiteSpace(nombreDeudor))
                    datos.setParametro("@nombreDeudor", "%" + nombreDeudor.Trim() + "%");

                if (estado != null)
                    datos.setParametro("@estado", estado.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Deuda aux = new Deuda();

                    aux.IdDeuda = (int)datos.Lector["IdDeuda"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    aux.NombreDeudor = (string)datos.Lector["NombreDeudor"];
                    aux.EmailDeudor = (string)datos.Lector["EmailDeudor"];
                    aux.Descripcion = datos.Lector["Descripcion"] == DBNull.Value ? null : (string)datos.Lector["Descripcion"];
                    aux.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    aux.Cuotas = datos.Lector["Cuotas"] == DBNull.Value ? (int?)null : (int)datos.Lector["Cuotas"];
                    aux.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    aux.Estado = (EstadoDeuda)int.Parse(datos.Lector["Estado"].ToString());

                    lista.Add(aux);
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
        //EXISTE
        public Deuda ExisteDeuda(int idUsuario, string nombreDeudor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(" SELECT IdDeuda, IdUsuario, NombreDeudor, EmailDeudor, Descripcion, MontoTotal, Cuotas, FechaInicio, Estado\r\n  FROM DEUDA WHERE IdUsuario = @idUsuario AND LOWER(NombreDeudor) = LOWER(@nombreDeudor) AND Estado = 1");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@nombreDeudor", nombreDeudor.Trim());

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Deuda aux = new Deuda();

                    aux.IdDeuda = (int)datos.Lector["IdDeuda"];

                    aux.Usuario = new Usuario();
                    aux.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    aux.NombreDeudor = (string)datos.Lector["NombreDeudor"];
                    aux.EmailDeudor = (string)datos.Lector["EmailDeudor"];
                    aux.Descripcion = datos.Lector["Descripcion"] == DBNull.Value ? null : (string)datos.Lector["Descripcion"];
                    aux.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    aux.Cuotas = datos.Lector["Cuotas"] == DBNull.Value ? (int?)null : (int)datos.Lector["Cuotas"];
                    aux.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    aux.Estado = (EstadoDeuda)int.Parse(datos.Lector["Estado"].ToString());

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
        //public List<Deuda> ListarPorUsuario(int idUsuario)
        //{

        //    List<Deuda> listaFiltrada = new List<Deuda>();
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
               
        //        datos.setConsulta("SELECT IdDeuda, IdUsuario ,NombreDeudor,EmailDeudor,Descripcion,MontoTotal,Cuotas,FechaInicio,Estado FROM DEUDA WHERE IdUsuario = @idUsuario");
        //        datos.setParametro("@idUsuario", idUsuario);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Deuda deuda = new Deuda();
        //            deuda.IdDeuda = (int)datos.Lector["IdDeuda"];
        //            deuda.Usuario = new Usuario();
        //            deuda.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
        //            deuda.NombreDeudor = (string)datos.Lector["NombreDeudor"];
        //            deuda.EmailDeudor = (string)datos.Lector["EmailDeudor"];

        //            deuda.MontoTotal = (decimal)datos.Lector["MontoTotal"];


        //            if (!(datos.Lector["Cuotas"] is DBNull))
        //            {
        //                deuda.Cuotas = (int)datos.Lector["Cuotas"];

        //            }
        //            else
        //            {
        //                deuda.Cuotas = null;
        //            }
        //            if (!(datos.Lector["Descripcion"] is DBNull))
        //            {
        //                deuda.Descripcion = (string)datos.Lector["Descripcion"];

        //            }
        //            else
        //            {
        //                deuda.Descripcion = null;
        //            }

        //            deuda.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
        //            deuda.Estado = (EstadoDeuda)int.Parse(datos.Lector["Estado"].ToString());




        //            listaFiltrada.Add(deuda);
        //        }

        //        return listaFiltrada;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally { datos.cerrarConexion(); }
        //}
        //public List<Deuda> FiltrarPorEstado(int estadoDeuda,int idUsuario)
        //{
        //    List<Deuda> listaFiltrada = new List<Deuda>();
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        if (estadoDeuda == -1)
        //        {
        //            datos.setConsulta("SELECT IdDeuda, IdUsuario ,NombreDeudor,EmailDeudor,Descripcion,MontoTotal,Cuotas,FechaInicio,Estado FROM DEUDA WHERE IdUsuario = @idUsuario");

        //        }
        //        else
        //        {
        //            datos.setConsulta("SELECT IdDeuda, IdUsuario ,NombreDeudor,EmailDeudor,Descripcion,MontoTotal,Cuotas,FechaInicio,Estado FROM DEUDA WHERE Estado = @estadoDeuda AND IdUsuario = @idUsuario");

        //        }
                
        //        datos.setParametro("@estadoDeuda", estadoDeuda);
        //        datos.setParametro("@idUsuario", idUsuario);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Deuda deuda = new Deuda();
        //            deuda.IdDeuda = (int)datos.Lector["IdDeuda"];
        //            deuda.Usuario = new Usuario();
        //            deuda.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];   
        //            deuda.NombreDeudor = (string)datos.Lector["NombreDeudor"];
        //            deuda.EmailDeudor = (string)datos.Lector["EmailDeudor"];
                   
        //            deuda.MontoTotal = (decimal)datos.Lector["MontoTotal"];

                    
        //            if (!(datos.Lector["Cuotas"] is DBNull))
        //            {
        //                deuda.Cuotas = (int)datos.Lector["Cuotas"];

        //            }
        //            else {                        
        //                deuda.Cuotas = null;
        //            }
        //            if (!(datos.Lector["Descripcion"] is DBNull))
        //            {
        //                deuda.Descripcion = (string)datos.Lector["Descripcion"];

        //            }
        //            else
        //            {
        //                deuda.Descripcion = null;
        //            }

        //            deuda.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
        //            deuda.Estado = (EstadoDeuda)int.Parse(datos.Lector["Estado"].ToString());
                  

                   

        //            listaFiltrada.Add(deuda);
        //        }

        //        return listaFiltrada;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally {                 datos.cerrarConexion(); }
        //}
        public List<Deuda> ListarPorUsuario(int idUsuario, int estado = -1)
        {
            List<Deuda> lista = new List<Deuda>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
              SELECT D.IdDeuda, D.IdUsuario, D.NombreDeudor, D.EmailDeudor, D.Descripcion,
                     D.MontoTotal, D.Cuotas, D.FechaInicio, D.Estado,
                     ISNULL(SUM(CD.Monto), 0) AS MontoPendiente
              FROM DEUDA D
              LEFT JOIN CUOTA_DEUDA CD ON CD.IdDeuda = D.IdDeuda AND CD.Estado = 1
              WHERE D.IdUsuario = @idUsuario";

                if (estado != -1)
                    consulta += " AND D.Estado = @estado";

                consulta += @" GROUP BY D.IdDeuda, D.IdUsuario, D.NombreDeudor, D.EmailDeudor, D.Descripcion,
                                 D.MontoTotal, D.Cuotas, D.FechaInicio, D.Estado";

                datos.setConsulta(consulta);
                datos.setParametro("@idUsuario", idUsuario);

                if (estado != -1)
                    datos.setParametro("@estado", estado);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Deuda deuda = new Deuda();
                    deuda.IdDeuda = (int)datos.Lector["IdDeuda"];
                    deuda.Usuario = new Usuario();
                    deuda.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    deuda.NombreDeudor = (string)datos.Lector["NombreDeudor"];
                    deuda.EmailDeudor = (string)datos.Lector["EmailDeudor"];
                    deuda.Descripcion = datos.Lector["Descripcion"] is DBNull ? null : (string)datos.Lector["Descripcion"];
                    deuda.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    deuda.Cuotas = datos.Lector["Cuotas"] is DBNull ? (int?)null : (int)datos.Lector["Cuotas"];
                    deuda.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    deuda.Estado = (EstadoDeuda)int.Parse(datos.Lector["Estado"].ToString());
                    deuda.MontoPendiente = (decimal)datos.Lector["MontoPendiente"];

                    lista.Add(deuda);
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

        public void MarcarPagada(int idDeuda)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idDeuda <= 0)
                    throw new Exception("Id de deuda inválido.");

                datos.setConsulta("UPDATE DEUDA SET Estado = @estado WHERE IdDeuda = @idDeuda");
                datos.setParametro("@idDeuda", idDeuda);
                datos.setParametro("@estado", (int)EstadoDeuda.Pago);

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

        public Deuda ObtenerPorId(int idDeuda)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"SELECT IdDeuda, IdUsuario, NombreDeudor, EmailDeudor,
              Descripcion, MontoTotal, Cuotas, FechaInicio, Estado
              FROM DEUDA WHERE IdDeuda = @idDeuda");

                datos.setParametro("@idDeuda", idDeuda);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Deuda aux = new Deuda();
                    aux.IdDeuda = (int)datos.Lector["IdDeuda"];
                    aux.Usuario = new Usuario();
                    aux.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.NombreDeudor = (string)datos.Lector["NombreDeudor"];
                    aux.EmailDeudor = (string)datos.Lector["EmailDeudor"];
                    aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? null : (string)datos.Lector["Descripcion"];
                    aux.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    aux.Cuotas = datos.Lector["Cuotas"] is DBNull ? (int?)null : (int)datos.Lector["Cuotas"];
                    aux.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    aux.Estado = (EstadoDeuda)int.Parse(datos.Lector["Estado"].ToString());

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
