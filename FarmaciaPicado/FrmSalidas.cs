using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmSalidas : Form
    {
        private readonly SalidaNegocio negocio = new SalidaNegocio();
        private int idUsuarioActivo;
        private List<Medicamento> medicamentosCargados = new List<Medicamento>();

        public FrmSalidas(int idUsuario)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuario;
        }

        private void FrmSalidas_Load(object sender, EventArgs e)
        {
            CargarMedicamentos();
            CargarHistorial();
        }

        private void CargarMedicamentos()
        {
            try
            {
                medicamentosCargados = negocio.ObtenerMedicamentos();
                cmbMedicamento.DataSource = medicamentosCargados;
                cmbMedicamento.DisplayMember = "Nombre";
                cmbMedicamento.ValueMember = "IdMedicamento";

                MostrarStockDisponible();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar medicamentos:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarStockDisponible()
        {
            if (cmbMedicamento.SelectedItem == null) return;

            Medicamento m = (Medicamento)cmbMedicamento.SelectedItem;
            lblStockDisponible.Text = $"Stock disponible: {m.StockActual}";
        }

        private void cmbMedicamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarStockDisponible();
        }

        private void CargarHistorial()
        {
            try
            {
                List<Salida> salidas = negocio.Obtener();
                dgvSalidas.DataSource = salidas;

                if (dgvSalidas.Columns["IdSalida"] != null)
                    dgvSalidas.Columns["IdSalida"].Visible = false;
                if (dgvSalidas.Columns["Medicamento"] != null)
                    dgvSalidas.Columns["Medicamento"].HeaderText = "Medicamento";
                if (dgvSalidas.Columns["Cantidad"] != null)
                    dgvSalidas.Columns["Cantidad"].HeaderText = "Cantidad";
                if (dgvSalidas.Columns["FechaSalida"] != null)
                    dgvSalidas.Columns["FechaSalida"].HeaderText = "Fecha";
                if (dgvSalidas.Columns["Usuario"] != null)
                    dgvSalidas.Columns["Usuario"].HeaderText = "Registrado por";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int idMedicamento = cmbMedicamento.SelectedValue != null
                    ? Convert.ToInt32(cmbMedicamento.SelectedValue) : 0;
                int cantidad = (int)nudCantidad.Value;

                negocio.Registrar(idMedicamento, cantidad, idUsuarioActivo, medicamentosCargados);

                MessageBox.Show("Salida registrada correctamente. Stock actualizado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                nudCantidad.Value = 1;
                CargarMedicamentos();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo registrar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}