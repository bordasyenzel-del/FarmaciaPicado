using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmEntradas : Form
    {
        private readonly EntradaNegocio negocio = new EntradaNegocio();
        private int idUsuarioActivo;

        public FrmEntradas(int idUsuario)
        {
            InitializeComponent();
            this.idUsuarioActivo = idUsuario;
        }

        private void FrmEntradas_Load(object sender, EventArgs e)
        {
            CargarMedicamentos();
            CargarHistorial();
        }

        private void CargarMedicamentos()
        {
            try
            {
                List<Medicamento> medicamentos = negocio.ObtenerMedicamentos();
                cmbMedicamento.DataSource = medicamentos;
                cmbMedicamento.DisplayMember = "Nombre";
                cmbMedicamento.ValueMember = "IdMedicamento";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar medicamentos:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistorial()
        {
            try
            {
                List<Entrada> entradas = negocio.Obtener();
                dgvEntradas.DataSource = entradas;

                if (dgvEntradas.Columns["IdEntrada"] != null)
                    dgvEntradas.Columns["IdEntrada"].Visible = false;
                if (dgvEntradas.Columns["Medicamento"] != null)
                    dgvEntradas.Columns["Medicamento"].HeaderText = "Medicamento";
                if (dgvEntradas.Columns["Cantidad"] != null)
                    dgvEntradas.Columns["Cantidad"].HeaderText = "Cantidad";
                if (dgvEntradas.Columns["FechaEntrada"] != null)
                    dgvEntradas.Columns["FechaEntrada"].HeaderText = "Fecha";
                if (dgvEntradas.Columns["Usuario"] != null)
                    dgvEntradas.Columns["Usuario"].HeaderText = "Registrado por";
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

                negocio.Registrar(idMedicamento, cantidad, idUsuarioActivo);

                MessageBox.Show("Entrada registrada correctamente. Stock actualizado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                nudCantidad.Value = 1;
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