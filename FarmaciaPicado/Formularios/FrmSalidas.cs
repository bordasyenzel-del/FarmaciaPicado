using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmSalidas : Form
    {
        private readonly SalidaDAO dao = new SalidaDAO();
        private readonly int idUsuario;

        private Guna2ComboBox cboMedicamento;
        private Guna2TextBox txtCantidad;
        private Label lblStock;
        private Guna2DataGridView dgv;
        private Guna2Button btnRegistrar;

        public FrmSalidas(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            ConstruirUI();
            this.Load += (s, e) =>
            {
                try { CargarMedicamentos(); CargarLista(); }
                catch (Exception ex) { EstiloUI.Error("Error al cargar los datos:\n" + ex.Message); }
            };
        }

        private void ConstruirUI()
        {
            var cuerpo = EstiloUI.ConfigurarForm(this, "Salidas", "Registro de medicamentos que salen del inventario", 1100, 640);
            EstiloUI.DosColumnas(cuerpo, 360, out Guna2Panel izq, out Guna2Panel der);

            // ---------- Formulario ----------
            izq.Controls.Add(EstiloUI.TituloTarjeta("Registrar salida"));

            cboMedicamento = EstiloUI.Combo();
            cboMedicamento.SelectedIndexChanged += (s, e) => ActualizarStock();
            txtCantidad = EstiloUI.Caja("Cantidad que sale");
            EstiloUI.Campo(izq, "Medicamento", cboMedicamento, 20, 56, 320);

            lblStock = new Label
            {
                Text = "",
                Font = new Font("Segoe UI Semibold", 9.5F),
                ForeColor = EstiloUI.TextoSuave,
                BackColor = Color.Transparent,
                Location = new Point(20, 112),
                AutoSize = true
            };
            izq.Controls.Add(lblStock);

            EstiloUI.Campo(izq, "Cantidad", txtCantidad, 20, 142, 320);

            btnRegistrar = EstiloUI.Boton("Registrar salida", EstiloUI.Naranja, 320);
            btnRegistrar.Location = new Point(20, 224);
            btnRegistrar.Click += BtnRegistrar_Click;
            izq.Controls.Add(btnRegistrar);

            // ---------- Historial ----------
            der.Padding = new Padding(20, 56, 20, 20);
            dgv = EstiloUI.CrearGrid();
            dgv.Columns.Add("Fecha", "Fecha");
            dgv.Columns.Add("Medicamento", "Medicamento");
            dgv.Columns.Add("Cantidad", "Cantidad");
            dgv.Columns.Add("Usuario", "Registrado por");
            EstiloUI.DesactivarOrden(dgv);

            der.Controls.Add(dgv);
            der.Controls.Add(EstiloUI.TituloTarjeta("Historial de salidas"));
        }

        private void CargarMedicamentos()
        {
            int? seleccionado = cboMedicamento.SelectedValue is int id ? id : (int?)null;

            cboMedicamento.DataSource = null;
            cboMedicamento.DisplayMember = "Nombre";
            cboMedicamento.ValueMember = "IdMedicamento";
            cboMedicamento.DataSource = dao.ObtenerMedicamentos();

            if (seleccionado.HasValue) cboMedicamento.SelectedValue = seleccionado.Value;
            ActualizarStock();
        }

        private void ActualizarStock()
        {
            if (cboMedicamento.SelectedItem is Medicamento m)
            {
                lblStock.Text = "Stock disponible: " + m.StockActual;
                lblStock.ForeColor = m.StockActual > 0 ? EstiloUI.Verde : EstiloUI.Rojo;
            }
            else
            {
                lblStock.Text = "";
            }
        }

        private void CargarLista()
        {
            dgv.Rows.Clear();
            foreach (Salida sa in dao.Obtener())
            {
                dgv.Rows.Add(sa.FechaSalida.ToString("dd/MM/yyyy HH:mm"), sa.Medicamento, sa.Cantidad, sa.Usuario);
            }
            dgv.ClearSelection();
            dgv.CurrentCell = null;
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (!(cboMedicamento.SelectedItem is Medicamento m)) { EstiloUI.Aviso("Seleccione un medicamento."); return; }
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            { EstiloUI.Aviso("La cantidad debe ser un número entero mayor que 0."); return; }
            if (cantidad > m.StockActual)
            { EstiloUI.Aviso("No hay stock suficiente. Disponible: " + m.StockActual); return; }

            try
            {
                dao.Registrar(m.IdMedicamento, cantidad, idUsuario);
                EstiloUI.Info("Salida registrada correctamente.");
                txtCantidad.Clear();
                CargarMedicamentos();
                CargarLista();
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo registrar la salida:\n" + ex.Message);
            }
        }
    }
}
