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

        public string GuardarCategoria(int opcion, ECategoria objCategoria)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("sp_GuardarCategoria", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = opcion;
                comando.Parameters.Add("@idCategoria", SqlDbType.Int).Value = objCategoria.IdCategoria;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = objCategoria.Descripcion;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar los datos";
            }
            catch (Exception ex)
            {
                respuesta =  ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return respuesta;
        }
    }
}
