using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> ListarPorUsuario(int idUsuario)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCategoria, Nombre, IdUsuario, Tipo, Estado FROM CATEGORIA WHERE IdUsuario = @idUsuario AND Estado = 1");
                datos.setParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();

                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = (string)datos.Lector["Nombre"];
                    categoria.Usuario = new Usuario();
                    categoria.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    categoria.Tipo = (TipoCategoria)(int)datos.Lector["Tipo"];
                    categoria.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(categoria);
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

        public bool ExisteCategoria(string nombre, int idUsuario, TipoCategoria tipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCategoria FROM CATEGORIA WHERE Nombre = @nombre AND IdUsuario = @idUsuario AND Tipo = @tipo AND Estado = 1");
                datos.setParametro("@nombre", nombre.Trim());
                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@tipo", (int)tipo);
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

        public void AgregarCategoria(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nueva == null)
                    throw new Exception("La categoría no puede ser nula.");

                if (string.IsNullOrWhiteSpace(nueva.Nombre))
                    throw new Exception("El nombre de la categoría es obligatorio.");

                if (nueva.Usuario == null || nueva.Usuario.IdUsuario <= 0)
                    throw new Exception("La categoría debe estar asociada a un usuario válido.");

                if (ExisteCategoria(nueva.Nombre, nueva.Usuario.IdUsuario, nueva.Tipo))
                    throw new Exception("Ya existe una categoría con ese nombre para ese tipo.");

                datos.setConsulta("INSERT INTO CATEGORIA (Nombre, IdUsuario, Tipo, Estado) VALUES (@nombre, @idUsuario, @tipo, @estado)");
                datos.setParametro("@nombre", nueva.Nombre.Trim());
                datos.setParametro("@idUsuario", nueva.Usuario.IdUsuario);
                datos.setParametro("@tipo", (int)nueva.Tipo);
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

        public List<Categoria> ListarPorUsuarioYTipo(int idUsuario, TipoCategoria tipo)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCategoria, Nombre, IdUsuario, Tipo, Estado " +
                                  "FROM CATEGORIA " +
                                  "WHERE IdUsuario = @idUsuario AND Tipo = @tipo AND Estado = 1");

                datos.setParametro("@idUsuario", idUsuario);
                datos.setParametro("@tipo", (int)tipo);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();

                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = (string)datos.Lector["Nombre"];

                    categoria.Usuario = new Usuario();
                    categoria.Usuario.IdUsuario = (int)datos.Lector["IdUsuario"];

                    categoria.Tipo = (TipoCategoria)(int)datos.Lector["Tipo"];
                    categoria.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(categoria);
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
