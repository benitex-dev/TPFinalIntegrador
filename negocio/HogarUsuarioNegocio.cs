using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class HogarUsuarioNegocio
    {
        //ALTA
        public void AgregarHogarUsuario(HogarUsuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nuevo == null)
                    throw new Exception("El miembro del hogar no puede ser nulo.");

                if (nuevo.Hogar == null || nuevo.Hogar.IdHogar <= 0)
                    throw new Exception("Debe asociar un hogar válido.");

                if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0)
                    throw new Exception("Debe asociar un usuario válido.");

                datos.setConsulta("INSERT INTO HOGAR_USUARIO (IdHogar, IdUsuario, Rol, Estado) " +
                                  "VALUES (@idHogar, @idUsuario, @rol, @estado)");

                datos.setParametro("@idHogar", nuevo.Hogar.IdHogar);
                datos.setParametro("@idUsuario", nuevo.Usuario.IdUsuario);

                datos.setParametro("@rol", nuevo.Rol.ToString());

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
        //BAJA
        public void EliminarLogico(int idMiembro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idMiembro <= 0)
                    throw new Exception("Id de miembro inválido.");

                if (!ExisteHogarUsuario(idMiembro))
                    throw new Exception("El miembro no existe o ya fue eliminado.");

                datos.setConsulta("UPDATE HOGAR_USUARIO SET Estado = 0 WHERE IdMiembro = @idMiembro");
                datos.setParametro("@idMiembro", idMiembro);

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

        //LISTAR

        //EXISTE
        public bool ExisteHogarUsuario(int idMiembro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdMiembro FROM HOGAR_USUARIO WHERE IdMiembro = @idMiembro AND Estado = 1");
                datos.setParametro("@idMiembro", idMiembro);
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
        public List<HogarUsuario> ListarPorHogar(int idHogar)
        {
            List<HogarUsuario> lista = new List<HogarUsuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
              SELECT HU.IdMiembro, HU.Rol, HU.Estado,
                     U.IdUsuario, U.Nombre, U.Apellido
              FROM HOGAR_USUARIO HU
              INNER JOIN USUARIO U ON U.IdUsuario = HU.IdUsuario
              WHERE HU.IdHogar = @idHogar AND HU.Estado = 1");

                datos.setParametro("@idHogar", idHogar);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HogarUsuario hu = new HogarUsuario();
                    hu.IdMiembro = (int)datos.Lector["IdMiembro"];

                    // Lectura segura de Rol (soporta strings con distinto case y valores numéricos)
                    hu.Rol = default(Rol); // valor por defecto si no se puede parsear
                    object rolObj = datos.Lector["Rol"];
                    if (rolObj != DBNull.Value && rolObj != null)
                    {
                        string rolStr = rolObj.ToString();

                        // Intentar parsear como nombre (ignore case) usando variable local
                        Rol rolParseado;
                        if (Enum.TryParse<Rol>(rolStr, true, out rolParseado))
                        {
                            hu.Rol = rolParseado;
                        }
                        else
                        {
                            // Si falla, intentar parsear como entero
                            if (int.TryParse(rolStr, out int rolInt) && Enum.IsDefined(typeof(Rol), rolInt))
                            {
                                hu.Rol = (Rol)rolInt;
                            }
                            else
                            {
                                // No se pudo mapear: dejar valor por defecto y registrar advertencia
                                System.Diagnostics.Trace.TraceWarning($"HogarUsuarioNegocio: Rol no reconocido para IdMiembro={hu.IdMiembro}: '{rolStr}'");
                                hu.Rol = default(Rol);
                            }
                        }
                    }

                    hu.Estado = (bool)datos.Lector["Estado"];

                    hu.Usuario = new Usuario();
                    hu.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    hu.Usuario.Nombre = (string)datos.Lector["Nombre"];
                    hu.Usuario.Apellido = datos.Lector["Apellido"] is DBNull ? "" : (string)datos.Lector["Apellido"];

                    lista.Add(hu);
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
