using dominio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class HogarNegocio
    {
        //ALTA
        public void AgregarHogar(Hogar nuevo,int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nuevo == null)
                    throw new Exception("El hogar no puede ser nulo.");

                if (string.IsNullOrWhiteSpace(nuevo.Nombre))
                    throw new Exception("El nombre del hogar es obligatorio.");

                /*if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0)
                    throw new Exception("El hogar debe estar asociado a un usuario.");*/

                datos.setConsulta("INSERT INTO HOGAR (Nombre, Estado) " +
                                  "VALUES (@nombre, @estado) SELECT SCOPE_IDENTITY();");

                datos.setParametro("@nombre", nuevo.Nombre.Trim());
                //datos.setParametro("@idUsuario", nuevo.Usuario.IdUsuario);
                datos.setParametro("@estado", nuevo.Estado);

                int idNuevoHogar = datos.ejecutarEscalar();

                datos.limpiarParametros();
                
                datos.setConsulta("INSERT INTO HOGAR_USUARIO (IdHogar, IdUsuario, Rol, Estado) VALUES (@idNuevoHogar, @idUsuario, 'ADMIN', 1)");
                datos.setParametro("@idNuevoHogar", idNuevoHogar);
                datos.setParametro("@idUsuario", idUsuario);

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
        public void EliminarLogico(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idHogar <= 0)
                    throw new Exception("Id de hogar inválido.");

                if (ExisteHogar(idHogar) == 0)
                    throw new Exception("El hogar no existe o ya fue eliminado.");

                datos.setConsulta("UPDATE HOGAR SET Estado = 0 WHERE IdHogar = @idHogar");
                datos.setParametro("@idHogar", idHogar);

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
        public void ModificarHogar(Hogar hogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (hogar == null)
                    throw new Exception("El hogar no puede ser nulo.");

                if (hogar.IdHogar <= 0)
                    throw new Exception("El id del hogar es inválido.");

                if (string.IsNullOrWhiteSpace(hogar.Nombre))
                    throw new Exception("El nombre del hogar es obligatorio.");

                /*if (hogar.Usuario == null || hogar.Usuario.IdUsuario <= 0)
                    throw new Exception("El hogar debe estar asociado a un usuario válido.");*/

                int idHogar = ExisteHogar(hogar.IdHogar);

                if (idHogar == 0)
                    throw new Exception("El hogar no existe o ya fue eliminado.");

                datos.setConsulta("UPDATE HOGAR SET Nombre = @nombre, IdUsuario = @idUsuario, Estado = @estado WHERE IdHogar = @idHogar");

                datos.setParametro("@idHogar", hogar.IdHogar);
                datos.setParametro("@nombre", hogar.Nombre.Trim());
                //datos.setParametro("@idUsuario", hogar.Usuario.IdUsuario);
                datos.setParametro("@estado", hogar.Estado);

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
        public List<Hogar> Listar(int idHogar = 0, string nombre = "", int idUsuario = 0, bool? estado = null)
        {
            List<Hogar> lista = new List<Hogar>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdHogar, Nombre, Estado FROM HOGAR WHERE 1=1";

                if (idHogar > 0)
                    consulta += " AND IdHogar = @idHogar";

                if (!string.IsNullOrWhiteSpace(nombre))
                    consulta += " AND Nombre LIKE @nombre";              

                if (estado != null)
                    consulta += " AND Estado = @estado";

                datos.setConsulta(consulta);

                if (idHogar > 0)
                    datos.setParametro("@idHogar", idHogar);

                if (!string.IsNullOrWhiteSpace(nombre))
                    datos.setParametro("@nombre", "%" + nombre.Trim() + "%");

                if (estado != null)
                    datos.setParametro("@estado", estado.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Hogar aux = new Hogar();

                    aux.IdHogar = (int)datos.Lector["IdHogar"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
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

        public Hogar listarUno(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();
            Hogar hogar = new Hogar();
            try
            {
                datos.setConsulta("SELECT IdHogar, Nombre FROM HOGAR WHERE IdHogar = @idHogar AND Estado = 1");
                datos.setParametro("@idHogar", idHogar);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    hogar.Nombre = (string)datos.Lector["Nombre"];
                    hogar.IdHogar = (int)datos.Lector["IdHogar"];
                }
                else
                {
                    hogar = null;
                }
                return hogar;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //EXISTE
        public int ExisteHogar(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdHogar FROM HOGAR WHERE IdHogar = @idHogar AND Estado = 1");
                datos.setParametro("@idHogar", idHogar);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["IdHogar"];
                else
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

        //SI DEVUELVE FALSE ES QUE EL INTEGRANTE AUN NO EXISTE EN EL HOGAR
        public bool AgregarIntegrante(int idHogar, int idUsuario, string rol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = @"
            IF NOT EXISTS (SELECT 1 FROM HOGAR_USUARIO WHERE IdUsuario = @idUsuario AND IdHogar = @idHogar)
            BEGIN
                INSERT INTO HOGAR_USUARIO (IdHogar, IdUsuario, Rol, Estado) 
                VALUES (@idHogar, @idUsuario, @rol, 1)
            END";

                datos.setConsulta(query);
                datos.setParametro("@idHogar", idHogar);
                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@rol", rol);
                int filasAfectadas = datos.ejecutarAccion();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
    }
}
