using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmEntradas : Form
    {
        private readonly EntradaDAO dao = new EntradaDAO();
        private readonly int idUsuario;

        private Guna2ComboBox cboMedicamento;
        private Guna2TextBox txtCantidad;
        private Guna2DataGridView dgv;
        private Guna2Button btnRegistrar;

        public FrmEntradas(int idUsuario)
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
            var cuerpo = EstiloUI.ConfigurarForm(this, "Entradas", "Registro de medicamentos que ingresan al inventario", 1100, 640);
            EstiloUI.DosColumnas(cuerpo, 360, out Guna2Panel izq, out Guna2Panel der);

            // ---------- Formulario ----------
            izq.Controls.Add(EstiloUI.TituloTarjeta("Registrar entrada"));

            cboMedicamento = EstiloUI.Combo();
            txtCantidad = EstiloUI.Caja("Cantidad que ingresa");
            EstiloUI.Campo(izq, "Medicamento", cboMedicamento, 20, 56, 320);
            EstiloUI.Campo(izq, "Cantidad", txtCantidad, 20, 118, 320);

            btnRegistrar = EstiloUI.Boton("Registrar entrada", EstiloUI.Verde, 320);
            btnRegistrar.Location = new Point(20, 200);
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
            der.Controls.Add(EstiloUI.TituloTarjeta("Historial de entradas"));
        }

        private void CargarMedicamentos()
        {
            int? seleccionado = cboMedicamento.SelectedValue is int id ? id : (int?)null;

            cboMedicamento.DataSource = null;
            cboMedicamento.DisplayMember = "Nombre";
            cboMedicamento.ValueMember = "IdMedicamento";
            cboMedicamento.DataSource = dao.ObtenerMedicamentos();

            if (seleccionado.HasValue) cboMedicamento.SelectedValue = seleccionado.Value;
        }

        private void CargarLista()
        {
            dgv.Rows.Clear();
            foreach (Entrada en in dao.Obtener())
            {
                dgv.Rows.Add(en.FechaEntrada.ToString("dd/MM/yyyy HH:mm"), en.Medicamento, en.Cantidad, en.Usuario);
            }
            dgv.ClearSelection();
            dgv.CurrentCell = null;
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (cboMedicamento.SelectedValue == null) { EstiloUI.Aviso("Seleccione un medicamento."); return; }
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            { EstiloUI.Aviso("La cantidad debe ser un número entero mayor que 0."); return; }

            try
            {
                dao.Registrar(Convert.ToInt32(cboMedicamento.SelectedValue), cantidad, idUsuario);
                EstiloUI.Info("Entrada registrada correctamente.");
                txtCantidad.Clear();
                CargarLista();
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo registrar la entrada:\n" + ex.Message);
            }
        }
    }
}
