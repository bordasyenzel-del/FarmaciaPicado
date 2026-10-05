using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;

namespace FarmaciaPicado
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            CargarStockBajo();
            CargarProximosVencer();
            CargarHistorial();

        }
        private void CargarStockBajo()
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    string sql = @"SELECT Nombre, Presentacion, StockActual, StockMinimo, FechaVencimiento
                                   FROM Medicamentos
                                   WHERE StockActual <= StockMinimo
                                   ORDER BY StockActual ASC";

                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    conexion.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    DataTable tabla = new DataTable();
                    tabla.Load(lector);

                    dgvStockBajo.DataSource = tabla;

                    if (dgvStockBajo.Columns["Nombre"] != null)
                        dgvStockBajo.Columns["Nombre"].HeaderText = "Nombre";
                    if (dgvStockBajo.Columns["Presentacion"] != null)
                        dgvStockBajo.Columns["Presentacion"].HeaderText = "Presentación";
                    if (dgvStockBajo.Columns["StockActual"] != null)
                        dgvStockBajo.Columns["StockActual"].HeaderText = "Stock Actual";
                    if (dgvStockBajo.Columns["StockMinimo"] != null)
                        dgvStockBajo.Columns["StockMinimo"].HeaderText = "Stock Mínimo";
                    if (dgvStockBajo.Columns["FechaVencimiento"] != null)
                        dgvStockBajo.Columns["FechaVencimiento"].HeaderText = "Fecha Vencimiento";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de stock bajo:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------
        // Reporte 2: Próximos a vencer (dentro de 30 días)
        // ---------------------------------------------------------
        private void CargarProximosVencer()
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    string sql = @"SELECT Nombre, Presentacion, StockActual, FechaVencimiento,
                                          DATEDIFF(DAY, GETDATE(), FechaVencimiento) AS DiasParaVencer
                                   FROM Medicamentos
                                   WHERE FechaVencimiento <= DATEADD(DAY, 30, GETDATE())
                                   ORDER BY FechaVencimiento ASC";

                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    conexion.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    DataTable tabla = new DataTable();
                    tabla.Load(lector);

                    dgvProximosVencer.DataSource = tabla;

                    if (dgvProximosVencer.Columns["Nombre"] != null)
                        dgvProximosVencer.Columns["Nombre"].HeaderText = "Nombre";
                    if (dgvProximosVencer.Columns["Presentacion"] != null)
                        dgvProximosVencer.Columns["Presentacion"].HeaderText = "Presentación";
                    if (dgvProximosVencer.Columns["StockActual"] != null)
                        dgvProximosVencer.Columns["StockActual"].HeaderText = "Stock Actual";
                    if (dgvProximosVencer.Columns["FechaVencimiento"] != null)
                        dgvProximosVencer.Columns["FechaVencimiento"].HeaderText = "Fecha Vencimiento";
                    if (dgvProximosVencer.Columns["DiasParaVencer"] != null)
                        dgvProximosVencer.Columns["DiasParaVencer"].HeaderText = "Días Restantes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de próximos a vencer:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void CargarHistorial()
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    // UNION combina los resultados de Entradas y Salidas en una sola tabla,
                    // agregando una columna "Tipo" para diferenciarlos
                    string sql = @"
                        SELECT 'Entrada' AS Tipo, M.Nombre AS Medicamento, E.Cantidad, 
                               E.FechaEntrada AS Fecha, U.NombreUsuario AS Usuario
                        FROM Entradas E
                        INNER JOIN Medicamentos M ON E.IdMedicamento = M.IdMedicamento
                        INNER JOIN Usuarios U ON E.IdUsuario = U.IdUsuario
 
                        UNION ALL
 
                        SELECT 'Salida' AS Tipo, M.Nombre AS Medicamento, S.Cantidad, 
                               S.FechaSalida AS Fecha, U.NombreUsuario AS Usuario
                        FROM Salidas S
                        INNER JOIN Medicamentos M ON S.IdMedicamento = M.IdMedicamento
                        INNER JOIN Usuarios U ON S.IdUsuario = U.IdUsuario
 
                        ORDER BY Fecha DESC";

                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    conexion.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    DataTable tabla = new DataTable();
                    tabla.Load(lector);

                    dgvHistorial.DataSource = tabla;

                    if (dgvHistorial.Columns["Tipo"] != null)
                        dgvHistorial.Columns["Tipo"].HeaderText = "Tipo";
                    if (dgvHistorial.Columns["Medicamento"] != null)
                        dgvHistorial.Columns["Medicamento"].HeaderText = "Medicamento";
                    if (dgvHistorial.Columns["Cantidad"] != null)
                        dgvHistorial.Columns["Cantidad"].HeaderText = "Cantidad";
                    if (dgvHistorial.Columns["Fecha"] != null)
                        dgvHistorial.Columns["Fecha"].HeaderText = "Fecha";
                    if (dgvHistorial.Columns["Usuario"] != null)
                        dgvHistorial.Columns["Usuario"].HeaderText = "Registrado por";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
