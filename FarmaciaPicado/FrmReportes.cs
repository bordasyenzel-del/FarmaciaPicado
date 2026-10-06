using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmReportes : Form
    {
        private readonly ReporteNegocio negocio = new ReporteNegocio();
        private CancellationTokenSource cts;

        public FrmReportes()
        {
            InitializeComponent();
        }

        private async void FrmReportes_Load(object sender, EventArgs e)
        {
            await CargarReportesAsync();
        }

        // Carga los 3 reportes en un hilo secundario, mostrando el
        // panel de "Cargando..." mientras tanto, con opción a cancelar.

        private async Task CargarReportesAsync()
        {
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            MostrarCargando(true);

            try
            {
                var resultado = await Task.Run(() => ObtenerDatosReportes(token), token);

                // Volvemos automáticamente al hilo de la interfaz aquí
                // (eso es lo que hace "await"), por eso ya podemos
                // tocar los controles sin problema.
                dgvStockBajo.DataSource = resultado.stockBajo;
                ConfigurarColumnasStockBajo();

                dgvProximosVencer.DataSource = resultado.proximosVencer;
                ConfigurarColumnasProximosVencer();

                dgvHistorial.DataSource = resultado.historial;
                ConfigurarColumnasHistorial();
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Carga de reportes cancelada.", "Cancelado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los reportes:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCargando(false);
            }
        }

        private (List<Medicamento> stockBajo, List<Medicamento> proximosVencer, List<HistorialMovimiento> historial)
            ObtenerDatosReportes(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var stockBajo = negocio.ObtenerStockBajo();

            // Espera simulada: representa el tiempo que tomaría un reporte
            // más pesado en un sistema con muchos más datos.
            Thread.Sleep(800);
            token.ThrowIfCancellationRequested();

            var proximosVencer = negocio.ObtenerProximosVencer();

            Thread.Sleep(800);
            token.ThrowIfCancellationRequested();

            var historial = negocio.ObtenerHistorial();

            return (stockBajo, proximosVencer, historial);
        }


        // Mostrar / ocultar el panel de carga

        private void MostrarCargando(bool mostrar)
        {
            panelCargando.Visible = mostrar;
            tabReportes.Enabled = !mostrar;
        }
        private void btnCancelarCarga_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }


        // Configuración de columnas de cada grid
        
        private void ConfigurarColumnasStockBajo()
        {
            OcultarSiExiste(dgvStockBajo, "IdMedicamento", "IdCategoria", "Categoria", "PrecioCompra", "PrecioVenta");
            RenombrarSiExiste(dgvStockBajo, "Nombre", "Nombre");
            RenombrarSiExiste(dgvStockBajo, "Presentacion", "Presentación");
            RenombrarSiExiste(dgvStockBajo, "StockActual", "Stock Actual");
            RenombrarSiExiste(dgvStockBajo, "StockMinimo", "Stock Mínimo");
            RenombrarSiExiste(dgvStockBajo, "FechaVencimiento", "Fecha Vencimiento");
        }

        private void ConfigurarColumnasProximosVencer()
        {
            OcultarSiExiste(dgvProximosVencer, "IdMedicamento", "IdCategoria", "Categoria", "PrecioCompra", "PrecioVenta", "StockMinimo");
            RenombrarSiExiste(dgvProximosVencer, "Nombre", "Nombre");
            RenombrarSiExiste(dgvProximosVencer, "Presentacion", "Presentación");
            RenombrarSiExiste(dgvProximosVencer, "StockActual", "Stock Actual");
            RenombrarSiExiste(dgvProximosVencer, "FechaVencimiento", "Fecha Vencimiento");
        }

        private void ConfigurarColumnasHistorial()
        {
            RenombrarSiExiste(dgvHistorial, "Tipo", "Tipo");
            RenombrarSiExiste(dgvHistorial, "Medicamento", "Medicamento");
            RenombrarSiExiste(dgvHistorial, "Cantidad", "Cantidad");
            RenombrarSiExiste(dgvHistorial, "Fecha", "Fecha");
            RenombrarSiExiste(dgvHistorial, "Usuario", "Registrado por");
        }

        private void OcultarSiExiste(DataGridView grid, params string[] columnas)
        {
            foreach (string c in columnas)
                if (grid.Columns[c] != null)
                    grid.Columns[c].Visible = false;
        }

        private void RenombrarSiExiste(DataGridView grid, string columna, string nuevoTitulo)
        {
            if (grid.Columns[columna] != null)
                grid.Columns[columna].HeaderText = nuevoTitulo;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}