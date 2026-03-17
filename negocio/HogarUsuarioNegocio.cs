using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    class HogarUsuarioNegocio
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

        //MODIFICAR

        //LISTAR
    }
}
