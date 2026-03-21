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

                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
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
    }
}
