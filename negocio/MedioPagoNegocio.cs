using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    
        public class MedioPagoNegocio
        {
            public List<MedioPago> ListarPorUsuario(int idUsuario)
            {
                List<MedioPago> lista = new List<MedioPago>();
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setConsulta("SELECT IdMedioPago, Tipo, Descripcion, IdUsuario, DiaCierre, DiaVencimiento, Estado " +
                                      "FROM MEDIOPAGO " +
                                      "WHERE IdUsuario = @idUsuario AND Estado = 1");

                    datos.setParametro("@idUsuario", idUsuario);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        MedioPago medio = new MedioPago();

                        medio.IdMedioPago = (int)datos.Lector["IdMedioPago"];
                        medio.Tipo = (TipoPago)(int)datos.Lector["Tipo"];
                        medio.Descripcion = (string)datos.Lector["Descripcion"];

                        medio.Usuario = new Usuario();
                        medio.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                        if (datos.Lector["DiaCierre"] != DBNull.Value)
                            medio.DiaCierre = (int)datos.Lector["DiaCierre"];

                        if (datos.Lector["DiaVencimiento"] != DBNull.Value)
                            medio.DiaVencimiento = (int)datos.Lector["DiaVencimiento"];

                        medio.Estado = (bool)datos.Lector["Estado"];

                        lista.Add(medio);
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

            public bool ExisteMedioPago(string descripcion, int idUsuario)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setConsulta("SELECT IdMedioPago " +
                                      "FROM MEDIOPAGO " +
                                      "WHERE Descripcion = @descripcion AND IdUsuario = @idUsuario AND Estado = 1");

                    datos.setParametro("@descripcion", descripcion.Trim());
                    datos.setParametro("@idUsuario", idUsuario);

                    datos.ejecutarLectura();

                    return datos.Lector.Read();
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

            public void AgregarMedioPago(MedioPago nuevo)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    if (nuevo == null)
                        throw new Exception("El medio de pago no puede ser nulo.");

                    if (string.IsNullOrWhiteSpace(nuevo.Descripcion))
                        throw new Exception("La descripción es obligatoria.");

                    if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0)
                        throw new Exception("El medio de pago debe estar asociado a un usuario válido.");

                    if (ExisteMedioPago(nuevo.Descripcion, nuevo.Usuario.IdUsuario))
                        throw new Exception("Ya existe un medio de pago con esa descripción.");

                    // Validación fechas en los casos en que el tipo de medio de pago sea crédito 
                    if (nuevo.Tipo == TipoPago.Credito)
                    {
                        if (nuevo.DiaCierre <= 0 || nuevo.DiaCierre > 31)
                            throw new Exception("El día de cierre debe estar entre 1 y 31.");

                        if (nuevo.DiaVencimiento <= 0 || nuevo.DiaVencimiento > 31)
                            throw new Exception("El día de vencimiento debe estar entre 1 y 31.");
                    }

                    datos.setConsulta("INSERT INTO MEDIOPAGO (Tipo, Descripcion, IdUsuario, DiaCierre, DiaVencimiento, Estado) " +
                                      "VALUES (@tipo, @descripcion, @idUsuario, @diaCierre, @diaVencimiento, @estado)");

                    datos.setParametro("@tipo", (int)nuevo.Tipo);
                    datos.setParametro("@descripcion", nuevo.Descripcion.Trim());
                    datos.setParametro("@idUsuario", nuevo.Usuario.IdUsuario);

                    // Si no es crédito las fechas quedan NULL 
                    if (nuevo.Tipo == TipoPago.Credito)
                    {
                        datos.setParametro("@diaCierre", nuevo.DiaCierre);
                        datos.setParametro("@diaVencimiento", nuevo.DiaVencimiento);
                    }
                    else
                    {
                        datos.setParametro("@diaCierre", DBNull.Value);
                        datos.setParametro("@diaVencimiento", DBNull.Value);
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

            public void EliminarMedioPago(int idMedioPago)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setConsulta("UPDATE MEDIOPAGO SET Estado = 0 WHERE IdMedioPago = @idMedioPago");
                    datos.setParametro("@idMedioPago", idMedioPago);

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

