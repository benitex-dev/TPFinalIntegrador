using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    class DeudaNegocio
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
                                  "VALUES (@idUsuario, @nombreDeudor, @emailDeudor, @descripcion, @montoTotal, @cuotas, @fechaInicio, @estado)");

                datos.setParametro("@idUsuario", nueva.Usuario.IdUsuario);
                datos.setParametro("@nombreDeudor", nueva.NombreDeudor.Trim());
                datos.setParametro("@emailDeudor", nueva.EmailDeudor.Trim());
                datos.setParametro("@descripcion", nueva.Descripcion.Trim());
                datos.setParametro("@montoTotal", nueva.MontoTotal);
                datos.setParametro("@cuotas", nueva.Cuotas);
                datos.setParametro("@fechaInicio", nueva.FechaInicio);
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
        public void EliminarLogico(int idUsuario, string nombreDeudor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                Deuda deuda = ExisteDeuda(idUsuario, nombreDeudor);

                if (deuda == null)
                    throw new Exception("La deuda no existe o ya fue saldada.");

                datos.setConsulta("UPDATE DEUDA SET Estado = 0 WHERE IdDeuda = @idDeuda");
                datos.setParametro("@idDeuda", deuda.IdDeuda);

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


                datos.setConsulta("UPDATE DEUDA SET EmailDeudor = @emailDeudor, Descripcion = @descripcion, MontoTotal = @montoTotal, Cuotas = @cuotas, FechaInicio = @fechaInicio, Estado = @estado WHERE IdDeuda = @idDeuda");

                datos.setParametro("@idDeuda", deuda.IdDeuda);
                datos.setParametro("@emailDeudor", deuda.EmailDeudor.Trim());
                datos.setParametro("@descripcion", deuda.Descripcion.Trim());
                datos.setParametro("@montoTotal", deuda.MontoTotal);
                datos.setParametro("@cuotas", deuda.Cuotas);
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
        public List<Deuda> Listar(int idUsuario = 0, string nombreDeudor = "", bool? estado = null)
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
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    aux.Cuotas = (int)datos.Lector["Cuotas"];
                    aux.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    aux.Estado = (bool)datos.Lector["Estado"];

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
                datos.setConsulta("SELECT IdDeuda FROM DEUDA WHERE IdUsuario = @idUsuario AND LOWER(NombreDeudor) = LOWER(@nombreDeudor) AND Estado = 1");

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
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.MontoTotal = (decimal)datos.Lector["MontoTotal"];
                    aux.Cuotas = (int)datos.Lector["Cuotas"];
                    aux.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
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
