using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Guna.UI2.WinForms;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;
using FarmaciaPicado.Orm;

//ANTES DE TOCAR ESTE MENÚ SE RECOMIENDA REZAR 2 PADRES NUESTROS Y 5 AVES MARIAS PORQUE SOLO DIOS SABE COMO FUNCIONA


namespace FarmaciaPicado
{
    public partial class FrmMenuPrincipal : Form
    {
        // ---- Colores reutilizables (paleta tipo dashboard) ----
        private static readonly Color ColorFondo = Color.FromArgb(243, 244, 247);
        private static readonly Color ColorSidebar = Color.FromArgb(17, 24, 46);
        private static readonly Color ColorSidebarHover = Color.FromArgb(29, 78, 216);
        private static readonly Color ColorAzul = Color.FromArgb(37, 99, 235);
        private static readonly Color ColorVerde = Color.FromArgb(16, 185, 129);
        private static readonly Color ColorRojo = Color.FromArgb(239, 68, 68);
        private static readonly Color ColorMorado = Color.FromArgb(139, 92, 246);
        private static readonly Color ColorNaranja = Color.FromArgb(245, 158, 11);
        private static readonly Color ColorTexto = Color.FromArgb(31, 41, 55);
        private static readonly Color ColorTextoSuave = Color.FromArgb(107, 114, 128);

        // ---- Datos de sesión ----
        private readonly int idUsuario;
        private readonly string nombreUsuario;
        private readonly string rol;

        // Indica si el usuario ha pulsado "Cerrar sesión" para volver al login.
        public bool CerroSesion { get; private set; } = false;

        // ---- DAOs ----
        private readonly MedicamentoDAO medicamentoDAO = new MedicamentoDAO();
        private readonly ReporteDAO reporteDAO = new ReporteDAO();
        private readonly ReporteServicioEf reportesEf = new ReporteServicioEf();
        private bool alertaVencimientoMostrada = false;

        // ---- Controles que se actualizan dinámicamente ----
        private Label lblValorTotal;
        private Label lblValorStockBajo;
        private Label lblValorProximos;
        private Guna2TextBox txtBuscar;
        private Guna2DataGridView dgvStock;
        private Guna2DataGridView dgvRecientes;
        private Chart chartMovimientos;
        private Label lblUsuarioActivo;
        private Label lblRolActivo;
        private Guna2Button btnMedicamentos, btnCategorias, btnEntradas, btnSalidas, btnUsuarios, btnReportes;

        public FrmMenuPrincipal(int idUsuario, string nombreUsuario, string rol)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            this.rol = rol;
            ConstruirUI();
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuarioActivo.Text = nombreUsuario;
            lblRolActivo.Text = rol;
            ConfigurarMenuPorRol();
            CargarResumen();
        }

        private void ConfigurarMenuPorRol()
        {
            switch (rol)
            {
                case "Administrador":
                    break;
                case "Encargado de inventario":
                    btnUsuarios.Visible = false;
                    break;
                case "Vendedor":
                    btnCategorias.Visible = false;
                    btnEntradas.Visible = false;
                    btnUsuarios.Visible = false;
                    btnReportes.Visible = false;
                    break;
            }
        }

        // =====================================================================
        //  CONSTRUCCIÓN DEL LAYOUT
        // =====================================================================
        private void ConstruirUI()
        {
            this.BackColor = ColorFondo;
            this.Font = new Font("Segoe UI", 9F);

            var panelSidebar = ConstruirSidebar();
            var panelTop = ConstruirTopBar();
            var panelCuerpo = ConstruirCuerpo();

            this.Controls.Add(panelCuerpo);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelSidebar);
        }

        // Estilo uniforme para las tablas (quita el encabezado de dos colores)
        private void EstilizarGrid(Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 247);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTexto;
            dgv.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(243, 244, 247);
            dgv.ThemeStyle.HeaderStyle.ForeColor = ColorTexto;
            dgv.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9.5F);
            dgv.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgv.ThemeStyle.RowsStyle.ForeColor = ColorTexto;
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgv.ThemeStyle.RowsStyle.SelectionForeColor = ColorTexto;
            dgv.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgv.ThemeStyle.GridColor = Color.FromArgb(229, 231, 235);
        }

        // Quita la flechita de ordenamiento de los encabezados
        private void DesactivarOrden(Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private Guna2Panel ConstruirSidebar()
        {
            var panel = new Guna2Panel
            {
                Dock = DockStyle.Left,
                Width = 230,
                FillColor = ColorSidebar
            };

            var avatar = new Guna2CirclePictureBox { Size = new Size(46, 46), Location = new Point(20, 20), FillColor = ColorAzul, BackColor = ColorSidebar };
            lblUsuarioActivo = new Label { Text = nombreUsuario, ForeColor = Color.White, BackColor = Color.Transparent, Font = new Font("Segoe UI Semibold", 11F), Location = new Point(76, 20), AutoSize = true };
            lblRolActivo = new Label { Text = rol, ForeColor = Color.FromArgb(148, 163, 184), BackColor = Color.Transparent, Font = new Font("Segoe UI", 9F), Location = new Point(76, 42), AutoSize = true };
            panel.Controls.Add(avatar);
            panel.Controls.Add(lblUsuarioActivo);
            panel.Controls.Add(lblRolActivo);

            var navPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Location = new Point(0, 90),
                Size = new Size(230, 400),
                WrapContents = false,
                BackColor = ColorSidebar
            };

            btnMedicamentos = CrearBotonNav("💊  Medicamentos");
            btnCategorias = CrearBotonNav("🏷️  Categorías");
            btnEntradas = CrearBotonNav("⬇️  Entradas");
            btnSalidas = CrearBotonNav("⬆️  Salidas");
            btnUsuarios = CrearBotonNav("👤  Usuarios");
            btnReportes = CrearBotonNav("📊  Reportes");

            btnMedicamentos.Click += (s, e) => { new FrmMedicamentos().ShowDialog(); CargarResumen(); };
            btnCategorias.Click += (s, e) => new FrmCategorias().ShowDialog();
            btnEntradas.Click += (s, e) => { new FrmEntradas(idUsuario).ShowDialog(); CargarResumen(); };
            btnSalidas.Click += (s, e) => { new FrmSalidas(idUsuario).ShowDialog(); CargarResumen(); };
            btnUsuarios.Click += (s, e) => new FrmUsuarios().ShowDialog();
            btnReportes.Click += (s, e) => new FrmReportes().ShowDialog();

            navPanel.Controls.Add(btnMedicamentos);
            navPanel.Controls.Add(btnCategorias);
            navPanel.Controls.Add(btnEntradas);
            navPanel.Controls.Add(btnSalidas);
            navPanel.Controls.Add(btnUsuarios);
            navPanel.Controls.Add(btnReportes);
            panel.Controls.Add(navPanel);

            var btnCerrarSesion = new Guna2Button
            {
                Text = "⏻  Cerrar sesión",
                Dock = DockStyle.Bottom,
                Height = 48,
                FillColor = Color.FromArgb(185, 28, 28),
                ForeColor = Color.White,
                BorderRadius = 0,
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            panel.Controls.Add(btnCerrarSesion);

            return panel;
        }

        private Guna2Button CrearBotonNav(string texto)
        {
            return new Guna2Button
            {
                Text = texto,
                Width = 210,
                Height = 46,
                TextAlign = HorizontalAlignment.Left,
                ImageAlign = HorizontalAlignment.Left,
                BackColor = ColorSidebar,
                FillColor = ColorSidebar,
                ForeColor = Color.FromArgb(203, 213, 225),
                HoverState = { FillColor = ColorSidebarHover, ForeColor = Color.White },
                BorderRadius = 8,
                Font = new Font("Segoe UI Semibold", 9.5F),
                Margin = new Padding(10, 2, 10, 2)
            };
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                // Señalamos que el usuario ha cerrado sesión para que el bucle en Program.Main
                // vuelva a mostrar el formulario de login en lugar de terminar la aplicación.
                this.CerroSesion = true;
                new FrmLogin().Show();
                this.Close();
            }
        }

        private Panel ConstruirTopBar()
        {
            var panel = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White };

            var lblTitulo = new Label { Text = "Farmacia Picado", Font = new Font("Segoe UI Semibold", 16F), ForeColor = ColorTexto, BackColor = Color.Transparent, Location = new Point(30, 8), AutoSize = true };
            var lblSubtitulo = new Label { Text = "Sistema de gestión", Font = new Font("Segoe UI", 9F), ForeColor = ColorTextoSuave, BackColor = Color.Transparent, Location = new Point(32, 42), AutoSize = true };

            var lblFecha = new Label
            {
                Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy\nhh:mm tt", new System.Globalization.CultureInfo("es-ES")),
                Font = new Font("Segoe UI", 9F),
                ForeColor = ColorTextoSuave,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Right,
                Width = 300,
                Padding = new Padding(0, 14, 24, 0)
            };

            panel.Controls.Add(lblFecha);
            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(lblSubtitulo);
            return panel;
        }

        private Panel ConstruirCuerpo()
        {
            var panelCuerpo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24), BackColor = ColorFondo };

            // --- Tarjetas resumen (arriba) ---
            var panelTarjetas = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 110,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = ColorFondo
            };
            panelTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            panelTarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblValorTotal = new Label();
            lblValorStockBajo = new Label();
            lblValorProximos = new Label();

            panelTarjetas.Controls.Add(CrearTarjeta("Total de medicamentos", lblValorTotal, ColorAzul, "💊"), 0, 0);
            panelTarjetas.Controls.Add(CrearTarjeta("Stock bajo", lblValorStockBajo, ColorVerde, "⚠️"), 1, 0);
            panelTarjetas.Controls.Add(CrearTarjeta("Próximos a vencer", lblValorProximos, ColorRojo, "📅"), 2, 0);
            panelTarjetas.GetControlFromPosition(2, 0).Margin = new Padding(0);

            // --- Zona central: izquierda (stock + gráfica) / derecha (acciones + recientes) ---
            var panelCentral = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 16, 0, 0),
                BackColor = ColorFondo
            };
            panelCentral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            panelCentral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            panelCentral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var panelIzquierdo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = ColorFondo
            };
            panelIzquierdo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelIzquierdo.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            panelIzquierdo.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            panelIzquierdo.Controls.Add(ConstruirPanelStock(), 0, 0);
            panelIzquierdo.Controls.Add(ConstruirPanelGrafica(), 0, 1);

            var panelDerecho = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(16, 0, 0, 0),
                BackColor = ColorFondo
            };
            panelDerecho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Absolute, 230));
            panelDerecho.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelDerecho.Controls.Add(ConstruirPanelAccionesRapidas(), 0, 0);
            panelDerecho.Controls.Add(ConstruirPanelRecientes(), 0, 1);

            panelCentral.Controls.Add(panelIzquierdo, 0, 0);
            panelCentral.Controls.Add(panelDerecho, 1, 0);

            panelCuerpo.Controls.Add(panelCentral);
            panelCuerpo.Controls.Add(panelTarjetas);
            return panelCuerpo;
        }

        private Guna2Panel CrearTarjeta(string titulo, Label lblValor, Color color, string icono)
        {
            var card = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 16, 0),
                BackColor = ColorFondo,
                FillColor = Color.White,
                BorderRadius = 14,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(229, 231, 235)
            };

            var circulo = new Guna2CircleButton
            {
                Text = icono,
                Font = new Font("Segoe UI Emoji", 13F),
                ForeColor = Color.White,
                BackColor = Color.White,
                FillColor = color,
                Size = new Size(44, 44),
                Location = new Point(18, 18),
                Animated = false
            };
            circulo.HoverState.FillColor = color;
            circulo.PressedColor = color;

            var lblTitulo = new Label { Text = titulo, Font = new Font("Segoe UI", 9.5F), ForeColor = ColorTextoSuave, BackColor = Color.Transparent, Location = new Point(76, 22), AutoSize = true };

            lblValor.Text = "0";
            lblValor.Font = new Font("Segoe UI Semibold", 20F);
            lblValor.ForeColor = ColorTexto;
            lblValor.BackColor = Color.Transparent;
            lblValor.Location = new Point(76, 44);
            lblValor.AutoSize = true;

            card.Controls.Add(circulo);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblValor);
            return card;
        }

        private Guna2Panel ConstruirPanelStock()
        {
            var panel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 16),
                BackColor = ColorFondo,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(20, 90, 20, 20)
            };

            var lblTitulo = new Label { Text = "Stock de medicamentos", Font = new Font("Segoe UI Semibold", 12F), ForeColor = ColorTexto, BackColor = Color.Transparent, Location = new Point(20, 16), AutoSize = true };
            txtBuscar = new Guna2TextBox { PlaceholderText = "Buscar medicamento...", Location = new Point(20, 48), Width = 260, Height = 32, BackColor = Color.White };
            txtBuscar.TextChanged += (s, e) => CargarStock(txtBuscar.Text);

            dgvStock = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 36
            };
            EstilizarGrid(dgvStock);
            dgvStock.Columns.Add("Nombre", "Nombre");
            dgvStock.Columns.Add("Categoria", "Categoría");
            dgvStock.Columns.Add("Stock", "Stock");
            dgvStock.Columns.Add("Precio", "Precio");
            dgvStock.Columns.Add("Estado", "Estado");
            DesactivarOrden(dgvStock);

            panel.Controls.Add(dgvStock);
            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(txtBuscar);
            return panel;
        }

        private Guna2Panel ConstruirPanelGrafica()
        {
            var panel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorFondo,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(20, 50, 20, 20)
            };
            var lblTitulo = new Label { Text = "Movimientos de la semana", Font = new Font("Segoe UI Semibold", 12F), ForeColor = ColorTexto, BackColor = Color.Transparent, Location = new Point(20, 16), AutoSize = true };

            chartMovimientos = new Chart
            {
                Dock = DockStyle.Fill,
                Size = new Size(400, 200),
                MinimumSize = new Size(100, 100)
            };
            var area = new ChartArea("area1");
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisX.Interval = 1;
            chartMovimientos.ChartAreas.Add(area);
            chartMovimientos.Legends.Add(new Legend("leyenda") { Docking = Docking.Bottom });

            panel.Controls.Add(chartMovimientos);
            panel.Controls.Add(lblTitulo);
            return panel;
        }

        private Guna2Panel ConstruirPanelAccionesRapidas()
        {
            var panel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 16),
                BackColor = ColorFondo,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(20, 50, 20, 20)
            };
            var lblTitulo = new Label { Text = "Accesos rápidos", Font = new Font("Segoe UI Semibold", 12F), ForeColor = ColorTexto, BackColor = Color.Transparent, Location = new Point(20, 16), AutoSize = true };

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.White
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            grid.Controls.Add(CrearBotonAccion("Reportes", ColorAzul, (s, e) => new FrmReportes().ShowDialog()), 0, 0);
            grid.Controls.Add(CrearBotonAccion("Usuarios", ColorMorado, (s, e) => new FrmUsuarios().ShowDialog()), 1, 0);
            grid.Controls.Add(CrearBotonAccion("Salidas", ColorVerde, (s, e) => { new FrmSalidas(idUsuario).ShowDialog(); CargarResumen(); }), 0, 1);
            grid.Controls.Add(CrearBotonAccion("Entradas", ColorNaranja, (s, e) => { new FrmEntradas(idUsuario).ShowDialog(); CargarResumen(); }), 1, 1);

            panel.Controls.Add(grid);
            panel.Controls.Add(lblTitulo);
            return panel;
        }

        private Guna2Button CrearBotonAccion(string texto, Color color, EventHandler onClick)
        {
            var btn = new Guna2Button
            {
                Text = texto,
                Dock = DockStyle.Fill,
                Margin = new Padding(6),
                BackColor = Color.White,
                FillColor = color,
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Segoe UI Semibold", 9.5F)
            };
            btn.Click += onClick;
            return btn;
        }

        private Guna2Panel ConstruirPanelRecientes()
        {
            var panel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorFondo,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(20, 50, 20, 20)
            };
            var lblTitulo = new Label { Text = "Últimas entradas / salidas", Font = new Font("Segoe UI Semibold", 12F), ForeColor = ColorTexto, BackColor = Color.Transparent, Location = new Point(20, 16), AutoSize = true };

            dgvRecientes = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 32
            };
            EstilizarGrid(dgvRecientes);
            dgvRecientes.Columns.Add("Tipo", "Tipo");
            dgvRecientes.Columns.Add("Medicamento", "Medicamento");
            dgvRecientes.Columns.Add("Cantidad", "Cant.");
            DesactivarOrden(dgvRecientes);
            dgvRecientes.Columns["Tipo"].FillWeight = 25;
            dgvRecientes.Columns["Medicamento"].FillWeight = 50;
            dgvRecientes.Columns["Cantidad"].FillWeight = 25;

            panel.Controls.Add(dgvRecientes);
            panel.Controls.Add(lblTitulo);
            return panel;
        }

        // =====================================================================
        //  CARGA DE DATOS (usa tus DAOs reales)
        // =====================================================================
        private void CargarResumen()
        {
            var proximosVencer = new List<Medicamento>();

            try
            {
                CargarStock("");

                var stockBajo = reporteDAO.ObtenerStockBajo();
                proximosVencer = reporteDAO.ObtenerProximosVencer();
                var historial = reporteDAO.ObtenerHistorial();

                lblValorTotal.Text = medicamentoDAO.Obtener().Count.ToString();
                lblValorStockBajo.Text = stockBajo.Count.ToString();
                lblValorProximos.Text = proximosVencer.Count.ToString();

                CargarRecientes(historial);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el resumen:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // La gráfica se carga aparte: si algo falla arriba, igual se intenta dibujar
            CargarGrafica();

            // La alerta se muestra cuando el formulario ya está visible
            var lista = proximosVencer;
            this.BeginInvoke((Action)(() => MostrarAlertaVencimientos(lista)));
        }

        private void CargarStock(string filtro)
        {
            dgvStock.Rows.Clear();
            foreach (Medicamento m in medicamentoDAO.Obtener(filtro ?? ""))
            {
                int dias = (m.FechaVencimiento.Date - DateTime.Today).Days;
                string estado = dias < 0 ? "Vencido"
                              : m.StockActual <= 0 ? "Agotado"
                              : dias <= 30 ? "Por vencer"
                              : "Disponible";
                Color colorEstado = (dias < 0 || m.StockActual <= 0) ? ColorRojo
                                  : dias <= 30 ? ColorNaranja
                                  : ColorVerde;

                int idx = dgvStock.Rows.Add(m.Nombre, m.Categoria, m.StockActual, m.PrecioVenta.ToString("C2"), estado);
                dgvStock.Rows[idx].Cells["Estado"].Style.ForeColor = colorEstado;
            }
            dgvStock.ClearSelection();
        }

        private void CargarRecientes(List<HistorialMovimiento> historial)
        {
            dgvRecientes.Rows.Clear();
            foreach (var mov in historial.Take(8))
            {
                int idx = dgvRecientes.Rows.Add(mov.Tipo, mov.Medicamento, mov.Cantidad);
                dgvRecientes.Rows[idx].Cells["Tipo"].Style.ForeColor = mov.Tipo == "Entrada" ? ColorVerde : ColorRojo;
            }
            dgvRecientes.ClearSelection();
        }

        // Avisa una sola vez por sesión si hay medicamentos vencidos o por vencer (30 días)
        private void MostrarAlertaVencimientos(List<Medicamento> lista)
        {
            if (alertaVencimientoMostrada || lista == null || lista.Count == 0) return;
            alertaVencimientoMostrada = true;

            int vencidos = lista.Count(m => m.FechaVencimiento.Date < DateTime.Today);
            int porVencer = lista.Count - vencidos;

            MessageBox.Show(
                "Atención:\n\n• Medicamentos vencidos: " + vencidos +
                "\n• Vencen en los próximos 30 días: " + porVencer +
                "\n\nRevise el reporte «Próximos a vencer».",
                "Alerta de vencimientos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Gráfica de los últimos 7 días (consulta LINQ con EF Core)
        private void CargarGrafica()
        {
            chartMovimientos.Series.Clear();
            chartMovimientos.Titles.Clear();

            try
            {
                var datos = reportesEf.MovimientosPorDia(7);
                var cultura = new System.Globalization.CultureInfo("es-ES");

                var serieEntradas = new Series("Entradas")
                {
                    ChartType = SeriesChartType.Line,
                    Color = ColorAzul,
                    BorderWidth = 3,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 7,
                    IsXValueIndexed = true
                };
                var serieSalidas = new Series("Salidas")
                {
                    ChartType = SeriesChartType.Line,
                    Color = ColorRojo,
                    BorderWidth = 3,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 7,
                    IsXValueIndexed = true
                };

                foreach (var d in datos)
                {
                    string etiqueta = d.Dia.ToString("ddd dd", cultura);
                    serieEntradas.Points.AddXY(etiqueta, d.Entradas);
                    serieSalidas.Points.AddXY(etiqueta, d.Salidas);
                }

                chartMovimientos.Series.Add(serieEntradas);
                chartMovimientos.Series.Add(serieSalidas);
            }
            catch (Exception ex)
            {
                // Si falla, el motivo queda visible en la propia gráfica
                chartMovimientos.Titles.Add(new Title("No se pudo cargar la gráfica: " + ex.Message) { ForeColor = ColorRojo });
            }
        }
    }
}