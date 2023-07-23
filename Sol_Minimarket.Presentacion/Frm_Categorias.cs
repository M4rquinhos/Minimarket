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
using Sol_Minimarket.Presentacion.Reportes;

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
        int idCategoria = 0;

        #endregion

        #region "Mis metodos"
        private void FormatoGrid()
        {
            dtgvCategorias.Columns[0].Width = 100;
            dtgvCategorias.Columns[0].HeaderText = "Identificador";
            dtgvCategorias.Columns[1].Width = 400;
            dtgvCategorias.Columns[1].HeaderText = "Nombre de Categoria";
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

        private void SeleccionItem()
        {
            if (string.IsNullOrEmpty(Convert.ToString(dtgvCategorias.CurrentRow.Cells["idCategoria"].Value)))
            {
                MessageBox.Show("Campo vacio", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            else
            {
                this.idCategoria = Convert.ToInt32(dtgvCategorias.CurrentRow.Cells["idCategoria"].Value);
                txtDescripcionCat.Text = Convert.ToString(dtgvCategorias.CurrentRow.Cells["descripcion"].Value);
            }
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
                //Estado nuevo o actualizar}
                _ = (opcionGuardar == 1) ? this.opcionGuardar = 1 : this.opcionGuardar = 2;
                ECategoria objCategoria = new ECategoria
                {
                    IdCategoria = this.idCategoria,
                    Descripcion = txtDescripcionCat.Text.Trim()
                };
                respuesta = NCategoria.GuardarCategoria(this.opcionGuardar, objCategoria);
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
                    this.opcionGuardar = 0;
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
            this.EstadoBotones(false);
            this.BotonesProcesos(true);
            txtDescripcionCat.ReadOnly = false;
            this.SeleccionItem();
            TabPrincipal.SelectedIndex = 1;
            txtDescripcionCat.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.opcionGuardar = 0;
            this.idCategoria = 0;
            txtDescripcionCat.Text = string.Empty;
            txtDescripcionCat.ReadOnly = true;
            this.EstadoBotones(true);
            this.BotonesProcesos(false);
            TabPrincipal.SelectedIndex = 0;
        }

        private void dtgvCategorias_DoubleClick(object sender, EventArgs e)
        {
            this.SeleccionItem();
            this.BotonesProcesos(false);
            TabPrincipal.SelectedIndex = 1;
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            this.BotonesProcesos(false);
            TabPrincipal.SelectedIndex = 0;
            this.idCategoria = 0;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(dtgvCategorias.CurrentRow.Cells["idCategoria"].Value)))
            {
                MessageBox.Show("Campo vacio, o no se encuentra el registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Verificando que el usuario quiera eliminar el registro
                DialogResult opcion;
                opcion = MessageBox.Show($"¿Estás seguro de eliminar el registro {dtgvCategorias.CurrentRow.Cells["descripcion"].Value}",
                                          "Aviso del sistema",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

                if (opcion == DialogResult.Yes)
                {
                    string respuesta = string.Empty;
                    this.idCategoria = Convert.ToInt32(dtgvCategorias.CurrentRow.Cells["idCategoria"].Value);
                    respuesta = NCategoria.DesactivarCategoria(idCategoria);
                    if (respuesta.Equals("OK"))
                    {
                        this.idCategoria = 0;
                        this.ListarCategorias("%");
                        MessageBox.Show("Registro desactivado correctamente", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ListarCategorias(txtBuscar.Text.Trim());
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Rpt_Categoria reporte = new Rpt_Categoria();
            reporte.txtParametro.Text = txtBuscar.Text.Trim();
            reporte.ShowDialog();
        }
    }
}
