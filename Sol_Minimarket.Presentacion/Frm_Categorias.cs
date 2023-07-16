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

        #region "variables"
        int opcionGuardar = 0; //No tiene accion definida

        #endregion

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

        private void EstadoBotones(bool estado)
        {
            this.btnNuevo.Enabled = estado;
            this.btnActualizar.Enabled = estado;
            this.btnEliminar.Enabled = estado;
            this.btnReporte.Enabled = estado;
            this.btnSalir.Enabled = estado;
        }

        private void BotonesProcesos(bool estado)
        {
            this.btnCancelar.Visible = estado;
            this.btnGuardar.Visible = estado;
            this.btnVerificar.Visible = !estado;
        }
        #endregion

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.ListarCategorias("%");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcionCat.Text == string.Empty)
            {
                MessageBox.Show("Falta ingresar datos", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Registro de informacion
                string respuesta = string.Empty;
                ECategoria objCategoria = new ECategoria
                {
                    Descripcion = txtDescripcionCat.Text.Trim()
                };
                respuesta = NCategoria.GuardarCategoria(1, objCategoria);
                if (respuesta == "OK")
                {
                    this.ListarCategorias("%");
                    MessageBox.Show("Los datos han sido guardados", "Aviso del sistema", MessageBoxButtons.OK);
                    this.opcionGuardar = 0;
                    this.EstadoBotones(true);
                    this.BotonesProcesos(false);
                    txtDescripcionCat.ReadOnly = true;
                    txtDescripcionCat.Text = string.Empty;
                    TabPrincipal.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(respuesta, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.opcionGuardar = 1; //Nuevo regitro
            this.EstadoBotones(false);
            this.BotonesProcesos(true);
            txtDescripcionCat.ReadOnly = false;
            txtDescripcionCat.Text = string.Empty;
            TabPrincipal.SelectedIndex = 1;
            txtDescripcionCat.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.opcionGuardar = 2; //Actualizar registro
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.opcionGuardar = 0;
            txtDescripcionCat.Text = string.Empty;
            txtDescripcionCat.ReadOnly = true;
            this.EstadoBotones(true);
            this.BotonesProcesos(false);
            TabPrincipal.SelectedIndex = 0;
        }
    }
}
