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

        //MODIFICAR

        //LISTAR
    }
}
