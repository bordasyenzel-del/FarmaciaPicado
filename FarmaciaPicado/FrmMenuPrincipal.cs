using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;

namespace FarmaciaPicado
{
    public partial class FrmMenuPrincipal : Form
    {
        private int idUsuario;
        private string nombreUsuario;
        private string rol;

        public FrmMenuPrincipal(int idUsuario, string nombreUsuario, string rol)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            this.rol = rol;
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuarioActivo.Text = $"{nombreUsuario}\n({rol})";
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

        
        private void CargarResumen()
        {
            try
            {
                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                 
                    string sqlTotal = "SELECT COUNT(*) FROM Medicamentos";
                    SqlCommand cmdTotal = new SqlCommand(sqlTotal, conexion);
                    int total = (int)cmdTotal.ExecuteScalar();
                    lblTotalMedicamentos.Text = $"Total de medicamentos: {total}";

                  
                    string sqlStockBajo = "SELECT COUNT(*) FROM Medicamentos WHERE StockActual <= StockMinimo";
                    SqlCommand cmdStockBajo = new SqlCommand(sqlStockBajo, conexion);
                    int stockBajo = (int)cmdStockBajo.ExecuteScalar();
                    lblStockBajo.Text = $"Medicamentos con stock bajo: {stockBajo}";

                  
                    string sqlVencer = "SELECT COUNT(*) FROM Medicamentos WHERE FechaVencimiento <= DATEADD(DAY, 30, GETDATE())";
                    SqlCommand cmdVencer = new SqlCommand(sqlVencer, conexion);
                    int proximosVencer = (int)cmdVencer.ExecuteScalar();
                    lblProximosVencer.Text = $"Próximos a vencer: {proximosVencer}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el resumen:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMedicamentos_Click(object sender, EventArgs e)
        {
            FrmMedicamentos frm = new FrmMedicamentos();
            frm.ShowDialog();
            CargarResumen(); 
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FrmCategorias frm = new FrmCategorias();
            frm.ShowDialog();
        }

        private void btnEntradas_Click(object sender, EventArgs e)
        {
            FrmEntradas frm = new FrmEntradas(idUsuario);
            frm.ShowDialog();
            CargarResumen();
        }

        private void btnSalidas_Click(object sender, EventArgs e)
        {
            FrmSalidas frm = new FrmSalidas(idUsuario);
            frm.ShowDialog();
            CargarResumen();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FrmUsuarios frm = new FrmUsuarios();
            frm.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

            FrmReportes frm = new FrmReportes();
            frm.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión papu?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                FrmLogin login = new FrmLogin();
                login.Show();
                this.Close();
            }
        }
    }
}

