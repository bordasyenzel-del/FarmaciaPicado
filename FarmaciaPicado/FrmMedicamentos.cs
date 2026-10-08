using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FarmaciaPicado.Orm;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmMedicamentos : Form
    {
        private readonly MedicamentoServicioEf dao = new MedicamentoServicioEf();
        private List<Categoria> categorias = new List<Categoria>();
        private int idSeleccionado = 0;
        private bool cargando = false;

        private Label lblModo;
        private Guna2TextBox txtBuscar, txtNombre, txtPresentacion, txtPrecioCompra, txtPrecioVenta, txtStock, txtStockMin;
        private Guna2ComboBox cboCategoria;
        private Guna2DateTimePicker dtpVence;
        private Guna2DataGridView dgv;
        private Guna2Button btnGuardar, btnNuevo, btnEliminar;

        public FrmMedicamentos()
        {
            InitializeComponent();
            ConstruirUI();
            this.Load += (s, e) =>
            {
                try
                {
                    CargarCategorias();
                    CargarLista("");
                    Limpiar();
                }
                catch (Exception ex) { EstiloUI.Error("Error al cargar los datos:\n" + ex.Message); }
            };
        }

        private void ConstruirUI()
        {
            var cuerpo = EstiloUI.ConfigurarForm(this, "Medicamentos", "Altas, bajas y modificaciones del inventario", 1200, 740);
            EstiloUI.DosColumnas(cuerpo, 380, out Guna2Panel izq, out Guna2Panel der);

            // ---------- Formulario (izquierda) ----------
            lblModo = EstiloUI.TituloTarjeta("Nuevo medicamento");
            izq.Controls.Add(lblModo);

            txtNombre = EstiloUI.Caja("Nombre del medicamento");
            txtPresentacion = EstiloUI.Caja("Ej: Tabletas 500 mg");
            cboCategoria = EstiloUI.Combo();
            txtPrecioCompra = EstiloUI.Caja("0.00");
            txtPrecioVenta = EstiloUI.Caja("0.00");
            txtStock = EstiloUI.Caja("0");
            txtStockMin = EstiloUI.Caja("0");
            dtpVence = EstiloUI.Fecha();

            EstiloUI.Campo(izq, "Nombre", txtNombre, 20, 56, 340);
            EstiloUI.Campo(izq, "Presentación", txtPresentacion, 20, 118, 340);
            EstiloUI.Campo(izq, "Categoría", cboCategoria, 20, 180, 340);
            EstiloUI.Campo(izq, "Precio de compra", txtPrecioCompra, 20, 242, 165);
            EstiloUI.Campo(izq, "Precio de venta", txtPrecioVenta, 195, 242, 165);
            EstiloUI.Campo(izq, "Stock actual", txtStock, 20, 304, 165);
            EstiloUI.Campo(izq, "Stock mínimo", txtStockMin, 195, 304, 165);
            EstiloUI.Campo(izq, "Fecha de vencimiento", dtpVence, 20, 366, 340);

            btnGuardar = EstiloUI.Boton("Guardar", EstiloUI.Azul, 340);
            btnGuardar.Location = new Point(20, 444);
            btnNuevo = EstiloUI.Boton("Nuevo", EstiloUI.TextoSuave, 165);
            btnNuevo.Location = new Point(20, 494);
            btnEliminar = EstiloUI.Boton("Eliminar", EstiloUI.Rojo, 165);
            btnEliminar.Location = new Point(195, 494);

            btnGuardar.Click += BtnGuardar_Click;
            btnNuevo.Click += (s, e) => Limpiar();
            btnEliminar.Click += BtnEliminar_Click;

            izq.Controls.Add(btnGuardar);
            izq.Controls.Add(btnNuevo);
            izq.Controls.Add(btnEliminar);

            // ---------- Listado (derecha) ----------
            der.Padding = new Padding(20, 96, 20, 20);

            txtBuscar = EstiloUI.Caja("Buscar por nombre...");
            txtBuscar.Location = new Point(20, 48);
            txtBuscar.Width = 320;
            txtBuscar.TextChanged += (s, e) =>
            {
                try { CargarLista(txtBuscar.Text); }
                catch (Exception ex) { EstiloUI.Error(ex.Message); }
            };

            dgv = EstiloUI.CrearGrid();
            dgv.Columns.Add("Nombre", "Nombre");
            dgv.Columns.Add("Presentacion", "Presentación");
            dgv.Columns.Add("Categoria", "Categoría");
            dgv.Columns.Add("Precio", "P. venta");
            dgv.Columns.Add("Stock", "Stock");
            dgv.Columns.Add("Vence", "Vence");
            EstiloUI.DesactivarOrden(dgv);
            dgv.SelectionChanged += Dgv_SelectionChanged;

            der.Controls.Add(dgv);
            der.Controls.Add(EstiloUI.TituloTarjeta("Listado de medicamentos"));
            der.Controls.Add(txtBuscar);
        }

        // =====================================================================
        //  DATOS
        // =====================================================================
        private void CargarCategorias()
        {
            categorias = dao.ObtenerCategorias();
            cboCategoria.DataSource = null;
            cboCategoria.DisplayMember = "NombreCategoria";
            cboCategoria.ValueMember = "IdCategoria";
            cboCategoria.DataSource = categorias;
        }

        private void CargarLista(string filtro)
        {
            cargando = true;
            dgv.Rows.Clear();

            foreach (Medicamento m in dao.Obtener(filtro ?? ""))
            {
                int i = dgv.Rows.Add(m.Nombre, m.Presentacion, m.Categoria, m.PrecioVenta.ToString("C2"),
                                     m.StockActual, m.FechaVencimiento.ToString("dd/MM/yyyy"));
                var fila = dgv.Rows[i];
                fila.Tag = m;

                if (m.StockActual <= m.StockMinimo)
                    fila.Cells["Stock"].Style.ForeColor = EstiloUI.Rojo;

                if (m.FechaVencimiento.Date < DateTime.Today)
                    fila.Cells["Vence"].Style.ForeColor = EstiloUI.Rojo;
                else if (m.FechaVencimiento.Date <= DateTime.Today.AddDays(30))
                    fila.Cells["Vence"].Style.ForeColor = EstiloUI.Naranja;
            }

            dgv.ClearSelection();
            dgv.CurrentCell = null;
            cargando = false;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || dgv.SelectedRows.Count == 0) return;
            if (!(dgv.SelectedRows[0].Tag is Medicamento m)) return;

            idSeleccionado = m.IdMedicamento;
            txtNombre.Text = m.Nombre;
            txtPresentacion.Text = m.Presentacion;
            txtPrecioCompra.Text = m.PrecioCompra.ToString("0.00");
            txtPrecioVenta.Text = m.PrecioVenta.ToString("0.00");
            txtStock.Text = m.StockActual.ToString();
            txtStockMin.Text = m.StockMinimo.ToString();
            dtpVence.Value = m.FechaVencimiento;

            var cat = categorias.FirstOrDefault(c => c.NombreCategoria == m.Categoria);
            if (cat != null) cboCategoria.SelectedValue = cat.IdCategoria;

            lblModo.Text = "Editar medicamento";
            btnEliminar.Enabled = true;
        }

        private void Limpiar()
        {
            idSeleccionado = 0;
            txtNombre.Clear();
            txtPresentacion.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            txtStockMin.Clear();
            dtpVence.Value = DateTime.Today.AddYears(1);
            cboCategoria.SelectedIndex = categorias.Count > 0 ? 0 : -1;
            lblModo.Text = "Nuevo medicamento";
            btnEliminar.Enabled = false;
            dgv.ClearSelection();
            txtNombre.Focus();
        }

        // =====================================================================
        //  ACCIONES
        // =====================================================================
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (nombre == "") { EstiloUI.Aviso("Ingrese el nombre del medicamento."); return; }
            if (cboCategoria.SelectedValue == null) { EstiloUI.Aviso("Seleccione una categoría."); return; }

            if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) || precioCompra < 0)
            { EstiloUI.Aviso("El precio de compra no es válido."); return; }
            if (!decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta) || precioVenta < 0)
            { EstiloUI.Aviso("El precio de venta no es válido."); return; }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            { EstiloUI.Aviso("El stock actual debe ser un número entero mayor o igual a 0."); return; }
            if (!int.TryParse(txtStockMin.Text, out int stockMin) || stockMin < 0)
            { EstiloUI.Aviso("El stock mínimo debe ser un número entero mayor o igual a 0."); return; }

            var m = new Medicamento
            {
                IdMedicamento = idSeleccionado,
                Nombre = nombre,
                Presentacion = txtPresentacion.Text.Trim(),
                IdCategoria = Convert.ToInt32(cboCategoria.SelectedValue),
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                StockActual = stock,
                StockMinimo = stockMin,
                FechaVencimiento = dtpVence.Value.Date
            };

            try
            {
                if (idSeleccionado == 0) dao.Insertar(m);
                else dao.Actualizar(m);

                EstiloUI.Info(idSeleccionado == 0 ? "Medicamento registrado." : "Medicamento actualizado.");
                CargarLista(txtBuscar.Text);
                Limpiar();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo guardar:\n" + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0) { EstiloUI.Aviso("Seleccione un medicamento de la lista."); return; }
            if (!EstiloUI.Confirmar("¿Eliminar el medicamento \"" + txtNombre.Text + "\"?")) return;

            try
            {
                dao.Eliminar(idSeleccionado);
                CargarLista(txtBuscar.Text);
                Limpiar();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo eliminar. Puede que tenga entradas o salidas registradas.\n\n" + ex.Message);
            }
        }
    }
}
