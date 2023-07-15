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
    }
}
