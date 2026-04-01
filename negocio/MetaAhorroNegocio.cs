using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MetaAhorroNegocio
    {
        //ALTA
        public void AgregarMetaAhorro(MetaAhorro nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nueva == null)
                    throw new Exception("La meta de ahorro no puede ser nula.");

                if (string.IsNullOrWhiteSpace(nueva.Nombre))
                    throw new Exception("El nombre de la meta es obligatorio.");

                if (nueva.Nombre.Any(char.IsDigit))
                    throw new Exception("El nombre no puede contener números.");

                if (nueva.MontoObjetivo <= 0)
                    throw new Exception("El monto objetivo debe ser mayor a cero.");

                if (nueva.FechaObjetivo == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha objetivo válida.");

                // Debe pertenecer a usuario o hogar
                if ((nueva.Usuario == null || nueva.Usuario.IdUsuario <= 0) &&
                    (nueva.Hogar == null || nueva.Hogar.IdHogar <= 0))
                    throw new Exception("La meta debe estar asociada a un usuario o a un hogar.");

                // No puede pertenecer a ambos al mismo tiempo
                if ((nueva.Usuario != null && nueva.Usuario.IdUsuario > 0) &&
                    (nueva.Hogar != null && nueva.Hogar.IdHogar > 0))
                    throw new Exception("La meta no puede pertenecer a usuario y hogar al mismo tiempo.");

                if (nueva.FechaObjetivo <= DateTime.Today)
                    throw new Exception("La fecha objetivo debe ser futura.");

                datos.setConsulta("INSERT INTO META_AHORRO (Nombre, MontoObjetivo, FechaObjetivo, IdUsuario, IdHogar, Estado) " +
                                  "VALUES (@nombre, @montoObjetivo, @fechaObjetivo, @idUsuario, @idHogar, @estado)");

                datos.setParametro("@nombre", nueva.Nombre.Trim());
                datos.setParametro("@montoObjetivo", nueva.MontoObjetivo);
                datos.setParametro("@fechaObjetivo", nueva.FechaObjetivo);

                // Usuario opcional
                if (nueva.Usuario != null && nueva.Usuario.IdUsuario > 0)
                    datos.setParametro("@idUsuario", nueva.Usuario.IdUsuario);
                else
                    datos.setParametro("@idUsuario", DBNull.Value);

                // Hogar opcional
                if (nueva.Hogar != null && nueva.Hogar.IdHogar > 0)
                    datos.setParametro("@idHogar", nueva.Hogar.IdHogar);
                else
                    datos.setParametro("@idHogar", DBNull.Value);

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
        public void EliminarLogico(int idMeta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idMeta <= 0)
                    throw new Exception("Id de meta inválido.");

                datos.setConsulta("UPDATE META_AHORRO SET Estado = 0 WHERE IdMeta = @idMeta");
                datos.setParametro("@idMeta", idMeta);

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
        //MODIFICACION
        public void ModificarMeta(MetaAhorro meta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (meta == null)
                    throw new Exception("La meta no existe o ya fue eliminada.");

                if (meta.IdMeta <= 0)
                    throw new Exception("La meta debe tener un Id válido.");

                if (string.IsNullOrWhiteSpace(meta.Nombre))
                    throw new Exception("El nombre es obligatorio.");

                if (meta.MontoObjetivo <= 0)
                    throw new Exception("El monto debe ser mayor a cero.");

                if (meta.FechaObjetivo == DateTime.MinValue)
                    throw new Exception("Debe ingresar una fecha válida.");

                if ((meta.Usuario == null || meta.Usuario.IdUsuario <= 0) &&
                    (meta.Hogar == null || meta.Hogar.IdHogar <= 0))
                    throw new Exception("Debe pertenecer a usuario o hogar.");

                if ((meta.Usuario != null && meta.Usuario.IdUsuario > 0) &&
                    (meta.Hogar != null && meta.Hogar.IdHogar > 0))
                    throw new Exception("No puede pertenecer a ambos.");


                datos.setConsulta("UPDATE META_AHORRO SET Nombre = @nombre, MontoObjetivo = @monto, FechaObjetivo = @fecha, IdUsuario = @idUsuario, IdHogar = @idHogar, Estado = @estado WHERE IdMeta = @idMeta");

                datos.setParametro("@idMeta", meta.IdMeta);
                datos.setParametro("@nombre", meta.Nombre.Trim());
                datos.setParametro("@monto", meta.MontoObjetivo);
                datos.setParametro("@fecha", meta.FechaObjetivo);

                if (meta.Usuario != null && meta.Usuario.IdUsuario > 0)
                    datos.setParametro("@idUsuario", meta.Usuario.IdUsuario);
                else
                    datos.setParametro("@idUsuario", DBNull.Value);

                if (meta.Hogar != null && meta.Hogar.IdHogar > 0)
                    datos.setParametro("@idHogar", meta.Hogar.IdHogar);
                else
                    datos.setParametro("@idHogar", DBNull.Value);

                datos.setParametro("@estado", meta.Estado);

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
        //LISTA
        public List<MetaAhorro> Listar(int idUsuario = 0, int idHogar = 0, string nombre = "", EstadoMetaAhorro? estado = null)
        {
            List<MetaAhorro> lista = new List<MetaAhorro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdMeta, Nombre, MontoObjetivo, FechaObjetivo, IdUsuario, IdHogar, Estado FROM META_AHORRO WHERE 1=1";

                if (idUsuario > 0)
                    consulta += " AND IdUsuario = @idUsuario";

                if (idHogar > 0)
                    consulta += " AND IdHogar = @idHogar";

                if (!string.IsNullOrWhiteSpace(nombre))
                    consulta += " AND Nombre LIKE @nombre";

                if (estado != null)
                    consulta += " AND Estado = @estado";

                datos.setConsulta(consulta);

                if (idUsuario > 0)
                    datos.setParametro("@idUsuario", idUsuario);

                if (idHogar > 0)
                    datos.setParametro("@idHogar", idHogar);

                if (!string.IsNullOrWhiteSpace(nombre))
                    datos.setParametro("@nombre", "%" + nombre.Trim() + "%");

                if (estado != null)
                    datos.setParametro("@estado", estado.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    MetaAhorro aux = new MetaAhorro();

                    aux.IdMeta = (int)datos.Lector["IdMeta"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.MontoObjetivo = Convert.ToDecimal(datos.Lector["MontoObjetivo"]);
                   
                    aux.Estado = (EstadoMetaAhorro)Convert.ToInt32(datos.Lector["Estado"]);

                    

                    if(!(datos.Lector["FechaObjetivo"] is DBNull))
                    {
                        aux.FechaObjetivo = Convert.ToDateTime(datos.Lector["FechaObjetivo"]);
                    }
                    else
                    {
                        aux.FechaObjetivo = null;
                    }

                    if (!(datos.Lector["IdUsuario"] is DBNull))
                    {
                        aux.Usuario = new Usuario();
                        aux.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    }

                    if (!(datos.Lector["IdHogar"] is DBNull))
                    {
                        aux.Hogar = new Hogar();
                        aux.Hogar.IdHogar = (int)datos.Lector["IdHogar"];
                    }

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
        public MetaAhorro ExisteMetaAhorro(int idUsuario, string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdMeta, Nombre, MontoObjetivo, FechaObjetivo, IdUsuario, IdHogar, Estado FROM META_AHORRO WHERE IdUsuario = @idUsuario AND Nombre = @nombre AND Estado = 1");
                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@nombre", nombre.Trim());

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    MetaAhorro aux = new MetaAhorro();

                    aux.IdMeta = (int)datos.Lector["IdMeta"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.MontoObjetivo = (decimal)datos.Lector["MontoObjetivo"];
                    aux.FechaObjetivo = (DateTime)datos.Lector["FechaObjetivo"];
                    aux.Estado = (EstadoMetaAhorro)datos.Lector["Estado"];

                    if (!(datos.Lector["IdUsuario"] is DBNull))
                    {
                        aux.Usuario = new Usuario();
                        aux.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    }

                    if (!(datos.Lector["IdHogar"] is DBNull))
                    {
                        aux.Hogar = new Hogar();
                        aux.Hogar.IdHogar = (int)datos.Lector["IdHogar"];
                    }

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
