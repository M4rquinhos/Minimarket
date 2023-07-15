using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sol_Minimarket.Negocio;
using Sol_Minimarket.Entidades;

namespace Sol_Minimarket.Presentacion
{
    public partial class Frm_Categorias : Form
    {
        public Frm_Categorias()
        {
            InitializeComponent();
        }

    #region "Mis metodos"
        private void FormatoGrid()
        {
            dtgvCategorias.Columns[0].Width = 100;
            dtgvCategorias.Columns[0].HeaderText = "id_Categoria";
            dtgvCategorias.Columns[1].Width = 400;
            dtgvCategorias.Columns[1].HeaderText = "Categoria";
        }

        private void ListarCategorias(string categoria)
        {
            try
            {
                dtgvCategorias.DataSource = NCategoria.ListarCategorias(categoria);
                this.FormatoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Error. No se pudo cargar la lista", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.ListarCategorias("%");
        }
    }
}
