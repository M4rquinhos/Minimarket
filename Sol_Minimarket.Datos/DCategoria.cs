using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Sol_Minimarket.Entidades;

namespace Sol_Minimarket.Datos
{
    public class DCategoria
    {
        public DataTable ListarCategorias(string categoria)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("sp_ListarCategorias", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@categoria", SqlDbType.VarChar).Value = categoria;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }
    }
}
