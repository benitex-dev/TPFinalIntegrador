using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    class MetaAhorroNegocio
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

                if (!ExisteMetaAhorro(idMeta))
                    throw new Exception("La meta no existe o ya fue eliminada.");

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

        //LISTA

        //EXISTE
        public bool ExisteMetaAhorro(int idMeta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdMeta FROM META_AHORRO WHERE IdMeta = @idMeta AND Estado = 1");
                datos.setParametro("@idMeta", idMeta);

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
