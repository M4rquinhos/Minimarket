using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Sol_Minimarket.Entidades;
using Sol_Minimarket.Datos;

namespace Sol_Minimarket.Negocio
{
    public class NCategoria
    {
        public static DataTable ListarCategorias(string categoria)
        {
            DCategoria datos = new DCategoria();
            return datos.ListarCategorias(categoria);
        }

        public static string GuardarCategoria(int opcion, ECategoria objCategoria)
        {
            DCategoria datos = new DCategoria();
            return datos.GuardarCategoria(opcion, objCategoria);
        }

        public static string DesactivarCategoria(int idCategoria)
        {
            DCategoria datos = new DCategoria();
            return datos.DesactivarCategoria(idCategoria);
        }
    }
}
