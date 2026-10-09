using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Guna.UI2.WinForms;
using FarmaciaPicado.Entidades;
using FarmaciaPicado.Orm;

namespace FarmaciaPicado
{
    // Reportes dinámicos: las consultas se arman con LINQ (EF Core) según los filtros elegidos.
    // Cada proceso se ejecuta en segundo plano y, si tarda, muestra la ventana de carga
    // con el botón «Cancelar».
    public partial class FrmReportes : Form
    {
        // Milisegundos que debe tardar un proceso para que aparezca la ventana de carga.
        // Ponlo en 0 si quieres que aparezca siempre (por ejemplo, para una demostración).
        private const int MostrarCargaTrasMs = 200;

        private readonly ReporteServicioEf reportes = new ReporteServicioEf();
        private readonly CategoriaServicioEf categoriasSvc = new CategoriaServicioEf();

        private static readonly string[] nombres =
            { "Stock bajo", "Próximos a vencer", "Movimientos", "Rotación", "Inventario" };

        private Guna2Button[] pestanas;
        private int pestanaActual = 0;
        private bool cargando = true;
        private bool ocupado = false;

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
            cboCategoria.SelectedIndexChanged += (s, e) => { if (!cargando && !ocupado) Generar(); };

            cboTipo = EstiloUI.Combo();
            cboTipo.Items.AddRange(new object[] { "Todos", "Entrada", "Salida" });
            cboTipo.SelectedIndex = 0;
            cboTipo.SelectedIndexChanged += (s, e) => { if (!cargando && !ocupado) Generar(); };

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
        //  VENTANA DE CARGA CON BOTÓN CANCELAR
        // =====================================================================
        // Ejecuta el trabajo en segundo plano. Si tarda más de MostrarCargaTrasMs,
        // aparece la ventana de carga; al pulsar «Cancelar» se cancela el token.
        // Si el usuario cancela, se lanza OperationCanceledException.
        private async Task<T> ConCarga<T>(string mensaje, Func<CancellationToken, Task<T>> trabajo)
        {
            using (var cts = new CancellationTokenSource())
            using (var dialogo = new DialogoProgreso(mensaje, cts))
            {
                try
                {
                    Task<T> tarea = Task.Run(() => trabajo(cts.Token));

                    if (MostrarCargaTrasMs <= 0 || await Task.WhenAny(tarea, Task.Delay(MostrarCargaTrasMs)) != tarea)
                        dialogo.Show(this);

                    return await tarea;
                }
                catch (Exception) when (cts.IsCancellationRequested)
                {
                    // SQL Server puede reportar la cancelación con otra excepción: se unifica aquí
                    throw new OperationCanceledException(cts.Token);
                }
                finally
                {
                    dialogo.CerrarDialogo();
                }
            }
        }

        // =====================================================================
        //  PESTAÑAS Y FILTROS
        // =====================================================================
        private void Mostrar(int indice)
        {
            if (ocupado) return;
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

        private async void Generar()
        {
            if (cargando || ocupado) return;
            ocupado = true;

            dgv.Rows.Clear();
            dgv.Columns.Clear();
            lblConteo.Text = "";

            try
            {
                switch (pestanaActual)
                {
                    case 0: await ReporteStockBajo(); break;
                    case 1: await ReporteProximosVencer(); break;
                    case 2: await ReporteMovimientos(); break;
                    case 3: await ReporteRotacion(); break;
                    default: await ReporteInventario(); break;
                }
            }
            catch (OperationCanceledException)
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
                lblConteo.Text = "Operación cancelada por el usuario";
            }
            catch (Exception ex)
            {
                lblConteo.Text = "";
                EstiloUI.Error("No se pudo generar el reporte:\n" + ex.Message);
            }
            finally
            {
                ocupado = false;
            }

            dgv.ClearSelection();
            dgv.CurrentCell = null;
        }

        // =====================================================================
        //  REPORTES
        // =====================================================================
        private async Task ReporteStockBajo()
        {
            int categoria = CategoriaSeleccionada();
            var lista = await ConCarga("Generando reporte de stock bajo...",
                ct => reportes.StockBajoAsync(categoria, ct));

            Columnas("Medicamento", "Presentación", "Categoría", "Stock actual", "Stock mínimo", "Vence");

            foreach (var m in lista)
            {
                int i = dgv.Rows.Add(m.Nombre, m.Presentacion, m.Categoria, m.StockActual, m.StockMinimo,
                                     m.FechaVencimiento.ToString("dd/MM/yyyy"));
                dgv.Rows[i].Cells[3].Style.ForeColor = EstiloUI.Rojo;
            }
            lblConteo.Text = lista.Count + " medicamento(s) con stock bajo";
        }

        private async Task ReporteProximosVencer()
        {
            // Validación del filtro de días
            if (!int.TryParse(txtDias.Text, out int dias) || dias < 0 || dias > 3650)
            {
                EstiloUI.Aviso("Ingrese un número de días válido (entre 0 y 3650).");
                return;
            }

            int categoria = CategoriaSeleccionada();
            var lista = await ConCarga("Buscando medicamentos próximos a vencer...",
                ct => reportes.ProximosVencerAsync(dias, categoria, ct));

            Columnas("Medicamento", "Presentación", "Categoría", "Stock", "Vence", "Estado");

            foreach (var m in lista)
            {
                int faltan = (m.FechaVencimiento.Date - DateTime.Today).Days;
                string estado = faltan < 0 ? "Vencido" : faltan == 0 ? "Vence hoy" : "En " + faltan + " día(s)";

                int i = dgv.Rows.Add(m.Nombre, m.Presentacion, m.Categoria, m.StockActual,
                                     m.FechaVencimiento.ToString("dd/MM/yyyy"), estado);

                // Vencidos y los que vencen en 7 días o menos: rojo. El resto: naranja.
                dgv.Rows[i].Cells[5].Style.ForeColor = faltan <= 7 ? EstiloUI.Rojo : EstiloUI.Naranja;
            }

            int vencidos = lista.Count(m => m.FechaVencimiento.Date < DateTime.Today);
            lblConteo.Text = lista.Count + " medicamento(s)  |  Vencidos: " + vencidos;
        }

        private async Task ReporteMovimientos()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                EstiloUI.Aviso("La fecha \"Desde\" no puede ser posterior a \"Hasta\".");
                return;
            }

            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;
            string tipo = cboTipo.SelectedItem as string ?? "Todos";
            string texto = txtBuscar.Text.Trim();

            var lista = await ConCarga("Consultando movimientos...",
                ct => reportes.MovimientosAsync(desde, hasta, tipo, texto, ct));

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

        private async Task ReporteRotacion()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                EstiloUI.Aviso("La fecha \"Desde\" no puede ser posterior a \"Hasta\".");
                return;
            }

            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;
            int categoria = CategoriaSeleccionada();

            var lista = await ConCarga("Calculando la rotación...",
                ct => reportes.RotacionAsync(desde, hasta, categoria, ct));

            Columnas("Medicamento", "Categoría", "Entradas", "Salidas", "Stock actual");

            foreach (var r in lista)
            {
                int i = dgv.Rows.Add(r.Medicamento, r.Categoria, r.Entradas, r.Salidas, r.StockActual);
                dgv.Rows[i].Cells[2].Style.ForeColor = EstiloUI.Verde;
                dgv.Rows[i].Cells[3].Style.ForeColor = EstiloUI.Rojo;
            }
            lblConteo.Text = lista.Count + " medicamento(s) analizados";
        }

        private async Task ReporteInventario()
        {
            var lista = await ConCarga("Calculando el valor del inventario...",
                ct => reportes.InventarioPorCategoriaAsync(ct));

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
        private async void ExportarExcel()
        {
            if (ocupado) return;

            if (dgv.Rows.Count == 0)
            {
                EstiloUI.Aviso("No hay datos para exportar. Genere un reporte primero.");
                return;
            }

            string ruta;
            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Libro de Excel (*.xlsx)|*.xlsx";
                dialogo.FileName = "reporte_" + nombres[pestanaActual].Replace(" ", "_").ToLower()
                                   + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

                if (dialogo.ShowDialog() != DialogResult.OK) return;
                ruta = dialogo.FileName;
            }

            // La tabla solo puede leerse desde el hilo de la interfaz: se copian los datos antes
            string hoja = nombres[pestanaActual];
            string[] encabezados = dgv.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToArray();
            var filas = new List<object[]>();
            foreach (DataGridViewRow fila in dgv.Rows)
                filas.Add(fila.Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());

            ocupado = true;
            try
            {
                await ConCarga("Exportando a Excel...",
                    ct => Task.Run(() => { EscribirExcel(ruta, hoja, encabezados, filas, ct); return true; }, ct));

                if (EstiloUI.Confirmar("Reporte exportado correctamente.\n\n¿Desea abrir el archivo ahora?"))
                    Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (OperationCanceledException)
            {
                try { if (System.IO.File.Exists(ruta)) System.IO.File.Delete(ruta); } catch { }
                EstiloUI.Info("Exportación cancelada.");
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo exportar:\n" + ex.Message);
            }
            finally
            {
                ocupado = false;
            }
        }

        private static void EscribirExcel(string ruta, string nombreHoja, string[] encabezados, List<object[]> filas, CancellationToken ct)
        {
            using (var libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add(nombreHoja);

                // Encabezados
                for (int c = 0; c < encabezados.Length; c++)
                {
                    var celda = hoja.Cell(1, c + 1);
                    celda.Value = encabezados[c];
                    celda.Style.Font.Bold = true;
                    celda.Style.Font.FontColor = XLColor.White;
                    celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#2563EB");
                }

                // Datos (los números se guardan como números, no como texto)
                int numeroFila = 2;
                foreach (object[] fila in filas)
                {
                    ct.ThrowIfCancellationRequested();

                    for (int c = 0; c < fila.Length; c++)
                    {
                        object valor = fila[c];
                        var celda = hoja.Cell(numeroFila, c + 1);

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

                    if (fila.Length > 0 && Convert.ToString(fila[0]) == "TOTAL")
                        hoja.Row(numeroFila).Style.Font.Bold = true;

                    numeroFila++;
                }

                ct.ThrowIfCancellationRequested();
                hoja.Columns().AdjustToContents();
                libro.SaveAs(ruta);
            }
        }
    }
}