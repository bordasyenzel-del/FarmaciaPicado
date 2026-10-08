using System;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioNegocio negocio = new UsuarioNegocio();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            try
            {
                Usuario u = negocio.ValidarLogin(txtUsuario.Text, txtContraseña.Text);

                if (u != null)
                {
                    FrmMenuPrincipal menu = new FrmMenuPrincipal(u.IdUsuario, u.NombreUsuario, u.Rol);
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                // Aquí llegan tanto los mensajes de "campo vacío" (de Negocio)
                // como errores reales de conexión a la base de datos
                lblMensaje.Text = ex.Message;
                //Oe sacrifiquemos al arioc :D
            }
        }
    }
}