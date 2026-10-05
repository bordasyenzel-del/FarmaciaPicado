using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmMedicamentos : Form
    {
        private readonly MedicamentoNegocio negocio = new MedicamentoNegocio();
        private int idMedicamentoSeleccionado = 0;

        public FrmMedicamentos()
        {
            InitializeComponent();
        }

        private void FrmMedicamentos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarMedicamentos();
            HabilitarCampos(false);
        }

       
        // Cargar el ComboBox de categorías (ahora vía Negocio)
       
        private void CargarCategorias()
        {
            try
            {
                List<Categoria> categorias = negocio.ObtenerCategorias();
                cmbCategoria.DataSource = categorias;
                cmbCategoria.DisplayMember = "NombreCategoria";
                cmbCategoria.ValueMember = "IdCategoria";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void CargarMedicamentos(string filtroNombre = "")
        {
            try
            {
                List<Medicamento> medicamentos = negocio.Obtener(filtroNombre);
                dgvMedicamentos.DataSource = medicamentos;

                if (dgvMedicamentos.Columns["IdMedicamento"] != null)
                    dgvMedicamentos.Columns["IdMedicamento"].Visible = false;
                if (dgvMedicamentos.Columns["IdCategoria"] != null)
                    dgvMedicamentos.Columns["IdCategoria"].Visible = false;

                if (dgvMedicamentos.Columns["Nombre"] != null)
                    dgvMedicamentos.Columns["Nombre"].HeaderText = "Nombre";
                if (dgvMedicamentos.Columns["Presentacion"] != null)
                    dgvMedicamentos.Columns["Presentacion"].HeaderText = "Presentación";
                if (dgvMedicamentos.Columns["Categoria"] != null)
                    dgvMedicamentos.Columns["Categoria"].HeaderText = "Categoría";
                if (dgvMedicamentos.Columns["PrecioCompra"] != null)
                    dgvMedicamentos.Columns["PrecioCompra"].HeaderText = "Precio Compra";
                if (dgvMedicamentos.Columns["PrecioVenta"] != null)
                    dgvMedicamentos.Columns["PrecioVenta"].HeaderText = "Precio Venta";
                if (dgvMedicamentos.Columns["StockActual"] != null)
                    dgvMedicamentos.Columns["StockActual"].HeaderText = "Stock Actual";
                if (dgvMedicamentos.Columns["StockMinimo"] != null)
                    dgvMedicamentos.Columns["StockMinimo"].HeaderText = "Stock Mínimo";
                if (dgvMedicamentos.Columns["FechaVencimiento"] != null)
                    dgvMedicamentos.Columns["FechaVencimiento"].HeaderText = "Fecha Vencimiento";

                dgvMedicamentos.MultiSelect = false;
                dgvMedicamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar medicamentos:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
        private void HabilitarCampos(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtPresentacion.Enabled = habilitar;
            cmbCategoria.Enabled = habilitar;
            txtPrecioCompra.Enabled = habilitar;
            txtPrecioVenta.Enabled = habilitar;
            txtStockActual.Enabled = habilitar;
            txtStockMinimo.Enabled = habilitar;
            dtpFechaVencimiento.Enabled = habilitar;

            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;

            btnAgregar.Enabled = !habilitar;
            btnEditar.Enabled = !habilitar;
            btnEliminar.Enabled = !habilitar;
            dgvMedicamentos.Enabled = !habilitar;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtPresentacion.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStockActual.Clear();
            txtStockMinimo.Clear();
            dtpFechaVencimiento.Value = DateTime.Now;
            if (cmbCategoria.Items.Count > 0)
                cmbCategoria.SelectedIndex = 0;
            idMedicamentoSeleccionado = 0;
        }

       
        private bool ValidarFormato()
        {
            if (!decimal.TryParse(txtPrecioCompra.Text, out _))
            {
                MessageBox.Show("El precio de compra debe ser un número válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioCompra.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out _))
            {
                MessageBox.Show("El precio de venta debe ser un número válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioVenta.Focus();
                return false;
            }

            if (!int.TryParse(txtStockActual.Text, out _))
            {
                MessageBox.Show("El stock actual debe ser un número entero válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockActual.Focus();
                return false;
            }

            if (!int.TryParse(txtStockMinimo.Text, out _))
            {
                MessageBox.Show("El stock mínimo debe ser un número entero válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockMinimo.Focus();
                return false;
            }

            return true;
        }

       
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            idMedicamentoSeleccionado = 0;
            LimpiarCampos();
            HabilitarCampos(true);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.CurrentRow == null || dgvMedicamentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un medicamento de la lista para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

       
            Medicamento m = (Medicamento)dgvMedicamentos.SelectedRows[0].DataBoundItem;

            idMedicamentoSeleccionado = m.IdMedicamento;
            txtNombre.Text = m.Nombre;
            txtPresentacion.Text = m.Presentacion;
            txtPrecioCompra.Text = m.PrecioCompra.ToString();
            txtPrecioVenta.Text = m.PrecioVenta.ToString();
            txtStockActual.Text = m.StockActual.ToString();
            txtStockMinimo.Text = m.StockMinimo.ToString();
            dtpFechaVencimiento.Value = m.FechaVencimiento;
            cmbCategoria.Text = m.Categoria;

            HabilitarCampos(true);
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormato())
                return;

            Medicamento m = new Medicamento
            {
                IdMedicamento = idMedicamentoSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                Presentacion = txtPresentacion.Text.Trim(),
                PrecioCompra = decimal.Parse(txtPrecioCompra.Text),
                PrecioVenta = decimal.Parse(txtPrecioVenta.Text),
                StockActual = int.Parse(txtStockActual.Text),
                StockMinimo = int.Parse(txtStockMinimo.Text),
                FechaVencimiento = dtpFechaVencimiento.Value.Date,
                IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue),
            };

            try
            {
                negocio.Guardar(m); // aquí se validan las reglas de negocio y se guarda

                MessageBox.Show("Medicamento guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                HabilitarCampos(false);
                CargarMedicamentos();
            }
            catch (Exception ex)
            {
                // Aquí llegan tanto errores de negocio (ej. "precio inválido")
                // como errores de base de datos
                MessageBox.Show(ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.CurrentRow == null || dgvMedicamentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un medicamento de la lista para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Medicamento m = (Medicamento)dgvMedicamentos.SelectedRows[0].DataBoundItem;

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro que desea eliminar el medicamento \"{m.Nombre}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                negocio.Eliminar(m.IdMedicamento);

                MessageBox.Show("Medicamento eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarMedicamentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar el medicamento.\n" +
                    "Puede que tenga movimientos de entradas o salidas asociados.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(false);
        }

       
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarMedicamentos(txtBuscar.Text.Trim());
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}