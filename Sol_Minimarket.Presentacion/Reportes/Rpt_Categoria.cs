using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sol_Minimarket.Presentacion.Reportes
{
    public partial class Rpt_Categoria : Form
    {
        public Rpt_Categoria()
        {
            InitializeComponent();
        }

        private void Rpt_Categoria_Load(object sender, EventArgs e)
        {
            this.sp_ListarCategoriasTableAdapter.Fill(this.dataSet_MiniMarket.sp_ListarCategorias, categoria:txtParametro.Text.Trim());
            this.reportViewer1.RefreshReport();
        }
    }
}
