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
                datos.setConsulta("INSERT INTO USUARIO (Email, Password, Nombre, Apellido, FechaNac, ImagenURL, Estado) VALUES (@email, @password, @nombre, @apellido, @fechaNac, @imagenURL, @estado)");
                datos.setParametro("@email", nuevo.Email);
                datos.setParametro("@password", nuevo.Password);
                datos.setParametro("@nombre", nuevo.Nombre);
                datos.setParametro("@apellido", nuevo.Apellido);
                datos.setParametro("@fechaNac", nuevo.FechaNac);
                datos.setParametro("@imagenURL", string.IsNullOrEmpty(nuevo.ImagenURL) ? (object)DBNull.Value : nuevo.ImagenURL);
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

    }
}

