using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    class HogarNegocio
    {
        //ALTA
        public void AgregarHogar(Hogar nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nuevo == null)
                    throw new Exception("El hogar no puede ser nulo.");

                if (string.IsNullOrWhiteSpace(nuevo.Nombre))
                    throw new Exception("El nombre del hogar es obligatorio.");

                if (nuevo.Usuario == null || nuevo.Usuario.IdUsuario <= 0)
                    throw new Exception("El hogar debe estar asociado a un usuario.");

                datos.setConsulta("INSERT INTO HOGAR (Nombre, IdUsuario, Estado) " +
                                  "VALUES (@nombre, @idUsuario, @estado)");

                datos.setParametro("@nombre", nuevo.Nombre.Trim());
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
        //BAJA
        public void EliminarLogico(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idHogar <= 0)
                    throw new Exception("Id de hogar inválido.");

                if (!ExisteHogar(idHogar))
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

        //LISTAR

        //EXISTE
        public bool ExisteHogar(int idHogar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdHogar FROM HOGAR WHERE IdHogar = @idHogar AND Estado = 1");
                datos.setParametro("@idHogar", idHogar);
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
    }
}
