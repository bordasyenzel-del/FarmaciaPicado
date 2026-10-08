using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClosedXML.Excel;
using Guna.UI2.WinForms;
using FarmaciaPicado.Entidades;
using FarmaciaPicado.Orm;

namespace FarmaciaPicado
{
    // Reportes dinámicos: las consultas se arman con LINQ (EF Core) según los filtros elegidos.
    public partial class FrmReportes : Form
    {
        private readonly ReporteServicioEf reportes = new ReporteServicioEf();
        private readonly CategoriaServicioEf categoriasSvc = new CategoriaServicioEf();

        private static readonly string[] nombres =
            { "Stock bajo", "Próximos a vencer", "Movimientos", "Rotación", "Inventario" };

        private Guna2Button[] pestanas;
        private int pestanaActual = 0;
        private bool cargando = true;

        private Panel grpDesde, grpHasta, grpCategoria, grpTipo, grpDias, grpBuscar;
        private Guna2DateTimePicker dtpDesde, dtpHasta;
        private Guna2ComboBox cboCategoria, cboTipo;
        private Guna2TextBox txtDias, txtBuscar;
        private Label lblConteo;
        private Guna2DataGridView dgv;

        public FrmReportes()
        {
            InitializeComponent();
            ConstruirUI();
            this.Load += (s, e) =>
            {
                try { CargarCategorias(); }
                catch (Exception ex) { EstiloUI.Error("Error al cargar las categorías:\n" + ex.Message); }

                cargando = false;
                Mostrar(0);
            };
        }

        // =====================================================================
        //  CONSTRUCCIÓN DE LA INTERFAZ
        // =====================================================================
        private void ConstruirUI()
        {
            var cuerpo = EstiloUI.ConfigurarForm(this, "Reportes", "Reportes dinámicos con filtros (Entity Framework Core + LINQ)", 1200, 720);

            var tarjeta = EstiloUI.Tarjeta();
            tarjeta.Padding = new Padding(20);

            dgv = EstiloUI.CrearGrid();

            // ---------- Barra de pestañas ----------
            var barra = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = Color.White };

            pestanas = new Guna2Button[nombres.Length];
            int x = 0;
            for (int i = 0; i < nombres.Length; i++)
            {
                int idx = i;
                pestanas[i] = EstiloUI.Boton(nombres[i], EstiloUI.Azul, i == 1 ? 160 : 125);
                pestanas[i].Location = new Point(x, 4);
                pestanas[i].Click += (s, e) => Mostrar(idx);
                barra.Controls.Add(pestanas[i]);
                x += pestanas[i].Width + 10;
            }

            lblConteo = new Label
            {
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = EstiloUI.TextoSuave,
                BackColor = Color.White,
                Dock = DockStyle.Right,
                Width = 320,
                TextAlign = ContentAlignment.MiddleRight
            };
            barra.Controls.Add(lblConteo);

            // ---------- Barra de filtros ----------
            var filtros = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 70,
                WrapContents = false,
                BackColor = Color.White
            };

            dtpDesde = EstiloUI.Fecha();
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta = EstiloUI.Fecha();
            dtpHasta.Value = DateTime.Today;

            cboCategoria = EstiloUI.Combo();
            cboCategoria.SelectedIndexChanged += (s, e) => { if (!cargando) Generar(); };

            cboTipo = EstiloUI.Combo();
            cboTipo.Items.AddRange(new object[] { "Todos", "Entrada", "Salida" });
            cboTipo.SelectedIndex = 0;
            cboTipo.SelectedIndexChanged += (s, e) => { if (!cargando) Generar(); };

            txtDias = EstiloUI.Caja("30");
            txtDias.Text = "30";
            txtBuscar = EstiloUI.Caja("Nombre del medicamento");

            grpDesde = Grupo("Desde", dtpDesde, 140);
            grpHasta = Grupo("Hasta", dtpHasta, 140);
            grpCategoria = Grupo("Categoría", cboCategoria, 190);
            grpTipo = Grupo("Tipo", cboTipo, 120);
            grpDias = Grupo("Vence en (días)", txtDias, 110);
            grpBuscar = Grupo("Medicamento", txtBuscar, 200);

            var btnGenerar = EstiloUI.Boton("Generar", EstiloUI.Azul, 110);
            btnGenerar.Click += (s, e) => Generar();
            var btnExportar = EstiloUI.Boton("Exportar Excel", EstiloUI.Verde, 140);
            btnExportar.Click += (s, e) => ExportarExcel();

            filtros.Controls.Add(grpDesde);
            filtros.Controls.Add(grpHasta);
            filtros.Controls.Add(grpCategoria);
            filtros.Controls.Add(grpTipo);
            filtros.Controls.Add(grpDias);
            filtros.Controls.Add(grpBuscar);
            filtros.Controls.Add(GrupoBoton(btnGenerar));
            filtros.Controls.Add(GrupoBoton(btnExportar));

            // El control con Dock = Fill se agrega primero; las barras después
            tarjeta.Controls.Add(dgv);
            tarjeta.Controls.Add(filtros);
            tarjeta.Controls.Add(barra);
            cuerpo.Controls.Add(tarjeta);
        }

        // Etiqueta + control agrupados (para poder mostrarlos/ocultarlos juntos)
        private Panel Grupo(string etiqueta, Control campo, int ancho)
        {
            var g = new Panel { Width = ancho, Height = 62, BackColor = Color.White, Margin = new Padding(0, 0, 14, 0) };
            g.Controls.Add(new Label
            {
                Text = etiqueta,
                ForeColor = EstiloUI.TextoSuave,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(0, 0),
                AutoSize = true
            });
            campo.Location = new Point(0, 20);
            campo.Width = ancho;
            g.Controls.Add(campo);
            return g;
        }

        private Panel GrupoBoton(Guna2Button boton)
        {
            var g = new Panel { Width = boton.Width, Height = 62, BackColor = Color.White, Margin = new Padding(0, 0, 14, 0) };
            boton.Location = new Point(0, 20);
            g.Controls.Add(boton);
            return g;
        }

        private void CargarCategorias()
        {
            var lista = categoriasSvc.Obtener();
            lista.Insert(0, new Categoria { IdCategoria = 0, NombreCategoria = "Todas" });

            cboCategoria.DataSource = null;
            cboCategoria.DisplayMember = "NombreCategoria";
            cboCategoria.ValueMember = "IdCategoria";
            cboCategoria.DataSource = lista;
            cboCategoria.SelectedIndex = 0;
        }

        // =====================================================================
        //  PESTAÑAS Y FILTROS
        // =====================================================================
        private void Mostrar(int indice)
        {
            pestanaActual = indice;

            for (int i = 0; i < pestanas.Length; i++)
            {
                bool activa = i == indice;
                pestanas[i].FillColor = activa ? EstiloUI.Azul : EstiloUI.Fondo;
                pestanas[i].ForeColor = activa ? Color.White : EstiloUI.Texto;
            }

            // Cada reporte muestra solo los filtros que le sirven
            grpDesde.Visible = indice == 2 || indice == 3;
            grpHasta.Visible = indice == 2 || indice == 3;
            grpTipo.Visible = indice == 2;
            grpBuscar.Visible = indice == 2;
            grpCategoria.Visible = indice == 0 || indice == 1 || indice == 3;
            grpDias.Visible = indice == 1;

            Generar();
        }

        private int CategoriaSeleccionada()
        {
            return cboCategoria.SelectedItem is Categoria c ? c.IdCategoria : 0;
        }

        private void Columnas(params string[] encabezados)
        {
            foreach (string h in encabezados) dgv.Columns.Add(h, h);
            EstiloUI.DesactivarOrden(dgv);
        }

        private void Generar()
        {
            if (cargando) return;

            dgv.Rows.Clear();
            dgv.Columns.Clear();

            try
            {
                switch (pestanaActual)
                {
                    case 0: ReporteStockBajo(); break;
                    case 1: ReporteProximosVencer(); break;
                    case 2: ReporteMovimientos(); break;
                    case 3: ReporteRotacion(); break;
                    default: ReporteInventario(); break;
                }
            }
            catch (Exception ex)
            {
                lblConteo.Text = "";
                EstiloUI.Error("No se pudo generar el reporte:\n" + ex.Message);
            }

            dgv.ClearSelection();
            dgv.CurrentCell = null;
        }

        // =====================================================================
        //  REPORTES
        // =====================================================================
        private void ReporteStockBajo()
        {
            var lista = reportes.StockBajo(CategoriaSeleccionada());
            Columnas("Medicamento", "Presentación", "Categoría", "Stock actual", "Stock mínimo", "Vence");

            foreach (var m in lista)
            {
                int i = dgv.Rows.Add(m.Nombre, m.Presentacion, m.Categoria, m.StockActual, m.StockMinimo,
                                     m.FechaVencimiento.ToString("dd/MM/yyyy"));
                dgv.Rows[i].Cells[3].Style.ForeColor = EstiloUI.Rojo;
            }
            lblConteo.Text = lista.Count + " medicamento(s) con stock bajo";
        }

        private void ReporteProximosVencer()
        {
            if (!int.TryParse(txtDias.Text, out int dias) || dias < 0)
            {
                EstiloUI.Aviso("Ingrese un número de días válido (0 o más).");
                return;
            }

            var lista = reportes.ProximosVencer(dias, CategoriaSeleccionada());
            Columnas("Medicamento", "Presentación", "Categoría", "Stock", "Vence", "Estado");

            foreach (var m in lista)
            {
                int faltan = (m.FechaVencimiento.Date - DateTime.Today).Days;
                string estado = faltan < 0 ? "Vencido" : faltan == 0 ? "Vence hoy" : "En " + faltan + " día(s)";

                int i = dgv.Rows.Add(m.Nombre, m.Presentacion, m.Categoria, m.StockActual,
                                     m.FechaVencimiento.ToString("dd/MM/yyyy"), estado);
                dgv.Rows[i].Cells[5].Style.ForeColor = faltan <= 0 ? EstiloUI.Rojo : EstiloUI.Naranja;
            }
            lblConteo.Text = lista.Count + " medicamento(s) vencidos o que vencen en " + dias + " días";
        }

        private void ReporteMovimientos()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                EstiloUI.Aviso("La fecha \"Desde\" no puede ser posterior a \"Hasta\".");
                return;
            }

            string tipo = cboTipo.SelectedItem as string ?? "Todos";
            var lista = reportes.Movimientos(dtpDesde.Value, dtpHasta.Value, tipo, txtBuscar.Text.Trim());
            Columnas("Fecha", "Tipo", "Medicamento", "Cantidad", "Usuario");

            foreach (var mov in lista)
            {
                int i = dgv.Rows.Add(mov.Fecha.ToString("dd/MM/yyyy HH:mm"), mov.Tipo, mov.Medicamento, mov.Cantidad, mov.Usuario);
                dgv.Rows[i].Cells[1].Style.ForeColor = mov.Tipo == "Entrada" ? EstiloUI.Verde : EstiloUI.Rojo;
            }

            int entradas = lista.Where(m => m.Tipo == "Entrada").Sum(m => m.Cantidad);
            int salidas = lista.Where(m => m.Tipo == "Salida").Sum(m => m.Cantidad);
            lblConteo.Text = lista.Count + " movimiento(s)  |  Entradas: " + entradas + "  Salidas: " + salidas;
        }

        private void ReporteRotacion()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                EstiloUI.Aviso("La fecha \"Desde\" no puede ser posterior a \"Hasta\".");
                return;
            }

            var lista = reportes.Rotacion(dtpDesde.Value, dtpHasta.Value, CategoriaSeleccionada());
            Columnas("Medicamento", "Categoría", "Entradas", "Salidas", "Stock actual");

            foreach (var r in lista)
            {
                int i = dgv.Rows.Add(r.Medicamento, r.Categoria, r.Entradas, r.Salidas, r.StockActual);
                dgv.Rows[i].Cells[2].Style.ForeColor = EstiloUI.Verde;
                dgv.Rows[i].Cells[3].Style.ForeColor = EstiloUI.Rojo;
            }
            lblConteo.Text = lista.Count + " medicamento(s) analizados";
        }

        private void ReporteInventario()
        {
            var lista = reportes.InventarioPorCategoria();
            Columnas("Categoría", "Medicamentos", "Unidades", "Valor de compra", "Valor de venta", "Margen potencial");

            foreach (var c in lista)
            {
                dgv.Rows.Add(c.Categoria, c.Medicamentos, c.Unidades,
                             c.ValorCompra, c.ValorVenta, c.ValorVenta - c.ValorCompra);
            }

            // Fila de totales
            decimal compra = lista.Sum(c => c.ValorCompra);
            decimal venta = lista.Sum(c => c.ValorVenta);
            int idx = dgv.Rows.Add("TOTAL", lista.Sum(c => c.Medicamentos), lista.Sum(c => c.Unidades),
                                   compra, venta, venta - compra);
            for (int k = 3; k <= 5; k++)
                dgv.Columns[k].DefaultCellStyle.Format = "C2";
            dgv.Rows[idx].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F);
            dgv.Rows[idx].DefaultCellStyle.BackColor = EstiloUI.Fondo;

            lblConteo.Text = lista.Count + " categoría(s)";
        }

        // =====================================================================
        //  EXPORTAR A EXCEL (.xlsx)
        // =====================================================================
        private void ExportarExcel()
        {
            if (dgv.Rows.Count == 0)
            {
                EstiloUI.Aviso("No hay datos para exportar. Genere un reporte primero.");
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Libro de Excel (*.xlsx)|*.xlsx";
                dialogo.FileName = "reporte_" + nombres[pestanaActual].Replace(" ", "_").ToLower()
                                   + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

                if (dialogo.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var libro = new XLWorkbook())
                    {
                        var hoja = libro.Worksheets.Add(nombres[pestanaActual]);

                        // Encabezados
                        for (int c = 0; c < dgv.Columns.Count; c++)
                        {
                            var celda = hoja.Cell(1, c + 1);
                            celda.Value = dgv.Columns[c].HeaderText;
                            celda.Style.Font.Bold = true;
                            celda.Style.Font.FontColor = XLColor.White;
                            celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#2563EB");
                        }

                        // Datos (los números se guardan como números, no como texto)
                        int fila = 2;
                        foreach (DataGridViewRow r in dgv.Rows)
                        {
                            for (int c = 0; c < dgv.Columns.Count; c++)
                            {
                                object valor = r.Cells[c].Value;
                                var celda = hoja.Cell(fila, c + 1);

                                if (valor is decimal d)
                                {
                                    celda.Value = d;
                                    celda.Style.NumberFormat.Format = "\"C$\"#,##0.00";
                                }
                                else if (valor is int n)
                                {
                                    celda.Value = n;
                                }
                                else
                                {
                                    celda.Value = Convert.ToString(valor) ?? "";
                                }
                            }

                            if (Convert.ToString(r.Cells[0].Value) == "TOTAL")
                                hoja.Row(fila).Style.Font.Bold = true;

                            fila++;
                        }

                        hoja.Columns().AdjustToContents();
                        libro.SaveAs(dialogo.FileName);
                    }

                    if (EstiloUI.Confirmar("Reporte exportado correctamente.\n\n¿Desea abrir el archivo ahora?"))
                        Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    EstiloUI.Error("No se pudo exportar:\n" + ex.Message);
                }
            }
        }
    }
}