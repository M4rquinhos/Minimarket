using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sol_Minimarket.Datos
{
    public class Conexion
    {
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private bool Seguridad;
        private static Conexion Con = null;

        //Constructor
        private Conexion()
        {
            this.Base = "BD_MINIMARKET";
            this.Servidor = "DESKTOP-TBL2D3P\\SQLEXPRESS";
            this.Usuario = "sistemas";
            this.Clave = "1234";
            this.Seguridad = false;
        }

        //Crea la conexion a la BD
        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();
            try
            {
                //Cadena de conexion.
                cadena.ConnectionString = $"Server={this.Servidor}; Database={this.Base};";
                //Tipo de seguridad para la conexion.
                if (Seguridad)
                {
                    cadena.ConnectionString = cadena.ConnectionString + "Integrated Security = SSPI"; //Autenticacion de windows.
                }
                else
                {
                    cadena.ConnectionString = cadena.ConnectionString + $"User Id ={this.Usuario}; Password={this.Clave}"; //Autenticacion de SQL Server.
                }
            }
            catch (Exception ex)
            {
                //Si surge algun problema, la conexion es nula y muestra el error.
                cadena = null;
                throw ex;
            }
            return cadena;
        }

        public static Conexion GetInstancia()
        {
            //Si no hay una conexion previa. Crea una nueva instancia de la clase Conexion.
            if (Con == null) 
            {
                Con =  new Conexion();
            }
            return Con;
        }
    }
}
