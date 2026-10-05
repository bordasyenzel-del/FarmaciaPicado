using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmCategorias : Form
    {
        private readonly CategoriaNegocio negocio = new CategoriaNegocio();
        private int idCategoriaSeleccionada = 0;

        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            HabilitarCampos(false);
        }

     
        // Cargar categorías (ahora vía Negocio)
       
        private void CargarCategorias()
        {
            try
            {
                List<Categoria> categorias = negocio.Obtener();
                dgvCategorias.DataSource = categorias;

                if (dgvCategorias.Columns["IdCategoria"] != null)
                    dgvCategorias.Columns["IdCategoria"].Visible = false;

                if (dgvCategorias.Columns["NombreCategoria"] != null)
                    dgvCategorias.Columns["NombreCategoria"].HeaderText = "Nombre de la categoría";

                dgvCategorias.MultiSelect = false;
                dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void HabilitarCampos(bool habilitar)
        {
            txtNombreCategoria.Enabled = habilitar;

            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;

            btnAgregar.Enabled = !habilitar;
            btnEditar.Enabled = !habilitar;
            btnEliminar.Enabled = !habilitar;
            dgvCategorias.Enabled = !habilitar;
        }

        private void LimpiarCampos()
        {
            txtNombreCategoria.Clear();
            idCategoriaSeleccionada = 0;
        }

     
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            idCategoriaSeleccionada = 0;
            LimpiarCampos();
            HabilitarCampos(true);
            txtNombreCategoria.Focus();
        }

      
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null || dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Categoria c = (Categoria)dgvCategorias.SelectedRows[0].DataBoundItem;
            idCategoriaSeleccionada = c.IdCategoria;
            txtNombreCategoria.Text = c.NombreCategoria;

            HabilitarCampos(true);
            txtNombreCategoria.Focus();
        }

        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Categoria c = new Categoria
            {
                IdCategoria = idCategoriaSeleccionada,
                NombreCategoria = txtNombreCategoria.Text.Trim(),
            };

            try
            {
                negocio.Guardar(c);

                MessageBox.Show("Categoría guardada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                HabilitarCampos(false);
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

   
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null || dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Categoria c = (Categoria)dgvCategorias.SelectedRows[0].DataBoundItem;

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro que desea eliminar la categoría \"{c.NombreCategoria}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                negocio.Eliminar(c.IdCategoria);

                MessageBox.Show("Categoría eliminada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar la categoría.\n" +
                    "Puede que tenga medicamentos asociados a ella.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(false);
        }

      
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}