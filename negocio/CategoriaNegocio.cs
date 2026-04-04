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

        public List<Categoria> ListarPorHogarYTipo(int idHogar, TipoCategoria tipo)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCategoria, Nombre, IdUsuario, IdHogar, Tipo, Estado " +
                                  "FROM CATEGORIA " +
                                  "WHERE IdHogar = @idHogar AND Tipo = @tipo AND Estado = 1");

                datos.setParametro("@idHogar", idHogar);
                datos.setParametro("@tipo", (int)tipo);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();

                    categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = (string)datos.Lector["Nombre"];

                    categoria.Hogar = new Hogar();
                    categoria.Hogar.IdHogar = (int)datos.Lector["IdHogar"];

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

        public bool ExisteCategoria(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT IdCategoria FROM CATEGORIA WHERE IdCategoria = @idCategoria AND Estado = 1");
                datos.setParametro("@idCategoria", idCategoria);

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

        public void EliminarLogico(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (idCategoria <= 0)
                    throw new Exception("Id de categoría inválido.");

                if (!ExisteCategoria(idCategoria))
                    throw new Exception("La categoría no existe o ya fue eliminada.");

                datos.setConsulta("UPDATE CATEGORIA SET Estado = 0 WHERE IdCategoria = @idCategoria");
                datos.setParametro("@idCategoria", idCategoria);

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

        public void ModificarCategoria(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (categoria == null)
                    throw new Exception("La categoría no puede ser nula.");

                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                    throw new Exception("El nombre de la categoría es obligatorio.");

                if (categoria.Usuario == null || categoria.Usuario.IdUsuario <= 0)
                    throw new Exception("La categoría debe estar asociada a un usuario válido.");

                // Validar que no exista otra categoría con el mismo nombre y tipo para el usuario (excepto la actual)
                AccesoDatos datosCheck = new AccesoDatos();
                try
                {
                    datosCheck.setConsulta("SELECT IdCategoria FROM CATEGORIA WHERE Nombre = @nombre AND IdUsuario = @idUsuario AND Tipo = @tipo AND Estado = 1 AND IdCategoria <> @idCategoria");
                    datosCheck.setParametro("@nombre", categoria.Nombre.Trim());
                    datosCheck.setParametro("@idUsuario", categoria.Usuario.IdUsuario);
                    datosCheck.setParametro("@tipo", (int)categoria.Tipo);
                    datosCheck.setParametro("@idCategoria", categoria.IdCategoria);
                    datosCheck.ejecutarLectura();
                    if (datosCheck.Lector.Read())
                        throw new Exception("Ya existe una categoría con ese nombre para ese tipo.");
                }
                finally
                {
                    datosCheck.cerrarConexion();
                }

                datos.setConsulta("UPDATE CATEGORIA SET Nombre = @nombre, Tipo = @tipo, Estado = @estado WHERE IdCategoria = @idCategoria");
                datos.setParametro("@nombre", categoria.Nombre.Trim());
                datos.setParametro("@tipo", (int)categoria.Tipo);
                datos.setParametro("@estado", categoria.Estado);
                datos.setParametro("@idCategoria", categoria.IdCategoria);

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

        public Categoria ObtenerOCrearCobroDeuda(int idUsuario)
        {
            // Primero busco si ya existe
            Categoria categoria = ListarPorUsuario(idUsuario)
                .Find(c => c.Nombre == "Cobro de deuda");

            // Si no existe la creo
            if (categoria == null)
            {
                categoria = new Categoria();
                categoria.Nombre = "Cobro de deuda";
                categoria.Tipo = TipoCategoria.Ingreso;
                categoria.Usuario = new Usuario() { IdUsuario = idUsuario };
                categoria.Estado = true;

                AgregarCategoria(categoria);

                // La busco de nuevo para obtener el IdCategoria generado
                categoria = ListarPorUsuario(idUsuario)
                    .Find(c => c.Nombre == "Cobro de deuda");
            }

            return categoria;
        }
    }
}
