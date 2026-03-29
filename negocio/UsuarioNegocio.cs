using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdUsuario, Email, Password, Nombre, Apellido, FechaNac, ImagenURL, Estado FROM USUARIO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario user = new Usuario();

                    user.IdUsuario = (int)datos.Lector["IdUsuario"];
                    user.Email = (string)datos.Lector["Email"];
                    user.Password = (string)datos.Lector["Password"];
                    user.Nombre = (string)datos.Lector["Nombre"];
                    user.Apellido = (string)datos.Lector["Apellido"];
                    user.FechaNac = (DateTime)datos.Lector["FechaNac"];

                    if (!(datos.Lector["ImagenURL"] is DBNull))
                        user.ImagenURL = (string)datos.Lector["ImagenURL"];
                    else
                        user.ImagenURL = "";

                    user.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(user);
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

        public Usuario Login(string email, string password)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.setConsulta("SELECT IdUsuario, Email, Password, Nombre, Apellido, FechaNac, ImagenURL, Estado FROM USUARIO WHERE Email = @email AND Password = @password AND Estado = 1");
                datos.setParametro("@email", email);
                datos.setParametro("@password", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();

                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Email = (string)datos.Lector["Email"];
                    usuario.Password = (string)datos.Lector["Password"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["Apellido"];
                    usuario.FechaNac = (DateTime)datos.Lector["FechaNac"];

                    if (!(datos.Lector["ImagenURL"] is DBNull))
                        usuario.ImagenURL = (string)datos.Lector["ImagenURL"];
                    else
                        usuario.ImagenURL = "";

                    usuario.Estado = (bool)datos.Lector["Estado"];
                }

                return usuario;
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

        public void AgregarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.iniciarTransaccion();
                datos.setConsulta(@"INSERT INTO USUARIO (Email, Password, Nombre, Apellido, FechaNac, ImagenURL, Estado) VALUES (@email, @password, @nombre, @apellido, @fechaNac, @imagenURL, @estado); SELECT SCOPE_IDENTITY();");
                datos.setParametro("@email", nuevo.Email);
                datos.setParametro("@password", nuevo.Password);
                datos.setParametro("@nombre", nuevo.Nombre);
                datos.setParametro("@apellido", nuevo.Apellido);
                datos.setParametro("@fechaNac", nuevo.FechaNac);
                datos.setParametro("@imagenURL", string.IsNullOrEmpty(nuevo.ImagenURL) ? (object)DBNull.Value : nuevo.ImagenURL);
                datos.setParametro("@estado", nuevo.Estado);

                //Se ejecuta el insert y se guarda al ID en la variable
                int idNuevoUsuario = datos.ejecutarEscalar();
                datos.limpiarParametros();

                datos.setConsulta("INSERT INTO CATEGORIA(Nombre, Tipo, IdUsuario, IdHogar, Estado) VALUES ('Sueldo', 1, @idUsuarioNuevo, NULL, 1), ('Negocios/Ventas', 1, @idUsuarioNuevo, NULL, 1), ('Supermercado', 2, @idUsuarioNuevo, NULL, 1), ('Vehículo', 2, @idUsuarioNuevo, NULL, 1), ('Entretenimiento', 2, @idUsuarioNuevo, NULL, 1), ('Música e Instrumentos', 2, @idUsuarioNuevo, NULL, 1), ('Inversiones', 2, @idUsuarioNuevo, NULL, 1), ('Tecnología', 2, @idUsuarioNuevo, NULL, 1);");
                datos.setParametro("@idUsuarioNuevo", idNuevoUsuario);
                datos.ejecutarAccion();
                datos.limpiarParametros();

                datos.setConsulta("INSERT INTO MEDIOPAGO (Tipo, Descripcion, DiaCierre, DiaVencimiento, IdUsuario, IdHogar, Estado)\r\nVALUES \r\n(1, 'Efectivo', NULL, NULL, @idUsuarioNuevo, NULL, 1),\r\n(2, 'Mercado Pago', NULL, NULL, @idUsuarioNuevo, NULL, 1),\r\n(3, 'Tarjeta Visa Banco Patagonia', 25, 5, @idUsuarioNuevo, NULL, 1);");
                datos.setParametro("@idUsuarioNuevo", idNuevoUsuario);
                datos.ejecutarAccion();

                datos.confirmarTransaccion();
            }
            catch (Exception ex)
            {
                datos.cancelarTransaccion();
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdUsuario FROM USUARIO WHERE Email = @email");
                datos.setParametro("@email", email);
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

        public void EliminarLogico(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new Exception("El email es obligatorio.");

                email = email.Trim();

                if (!ExisteEmail(email))
                    throw new Exception("El usuario no existe.");

                datos.setConsulta("UPDATE USUARIO SET Estado = 0 WHERE Email = @email");
                datos.setParametro("@email", email);

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
        public void ResetPass(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Usuario set password = @pass  where IdUsuario = @id");
                datos.setParametro("@pass", usuario.Password);
                datos.setParametro("@id", usuario.IdUsuario);
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
        public Usuario buscarMail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = new Usuario();
            try
            {
                if (email != "")
                {
                    datos.setConsulta("select IdUsuario from usuario where email = @email");
                    datos.setParametro("@email", email);
                    datos.ejecutarLectura();
                    datos.Lector.Read();
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Email = email;

                    return usuario;
                }

                return usuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void ModificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Usuario set email=@email, nombre = @nombre, apellido = @apellido, ImagenURL = @img where IdUsuario = @id");
                datos.setParametro("@email", usuario.Email);
                datos.setParametro("@nombre", usuario.Nombre);
                datos.setParametro("@apellido", usuario.Apellido);
                datos.setParametro("@img", (object)usuario.ImagenURL ?? DBNull.Value);
                datos.setParametro("@id", usuario.IdUsuario);
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

