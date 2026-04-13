using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        private SqlTransaction transaccion;

        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos()
        {
            conexion = new SqlConnection(
            //CONEXIÒN MERI
            //"server=.\\SQLEXPRESS; database=ADMIN_GASTOS_DB; integrated security=true"

            //
            //"server=(localdb)\\MSSQLLocalDB; database=ADMIN_GASTOS_DB; integrated security=true"

            //CONEXION AILIN
            //"server=.\\sqlexpress; database=admin_gastos_db; integrated security=true"

            //CONEXION JOAKO
            //"server=localhost; database=ADMIN_GASTOS_DB; Persist Security Info=True; User ID= sa; Password=Contra993!"

            //CONEXION JOHAN
            "server=localhost; database=ADMIN_GASTOS_DB; Persist Security Info=True; User ID= sa; Password=Johann123"
            );
            comando = new SqlCommand();
        }
        public void setConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
            comando.Parameters.Clear();

        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                if(conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public int ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Funcion para obtener el ID de un usuario recien generado
        /// </summary>
        /// <returns>id del usuario recien generado</returns>
        public int ejecutarEscalar()
        {
            comando.Connection = conexion;
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                return Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void setParametro(string nombre, Object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public void iniciarTransaccion()
        {
            try
            {
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                comando.Transaction = transaccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void confirmarTransaccion()
        {
            try
            {
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cancelarTransaccion()
        {
            try
            {
                transaccion.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }
    }
}
