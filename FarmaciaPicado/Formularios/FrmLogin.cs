using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioNegocio negocio = new UsuarioNegocio();

        private Guna2TextBox txtUsuario, txtContrasena;
        private Guna2Button btnIniciarSesion;
        private Label lblMensaje;

        // Usuario que inició sesión correctamente (lo lee Program.cs)
        public Usuario UsuarioAutenticado { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            ConstruirUI();
        }

        // El diseño se construye por código (igual que FrmUsuarios y FrmMenuPrincipal)
        // usando la paleta y los helpers de EstiloUI.
        private void ConstruirUI()
        {
            this.Text = "Iniciar sesión - Farmacia Picado";
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);
            this.ClientSize = new Size(860, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ---------- Panel derecho: formulario ----------
            var panelDerecho = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            panelDerecho.Controls.Add(new Label
            {
                Text = "Iniciar sesión",
                Font = new Font("Segoe UI Semibold", 20F),
                ForeColor = EstiloUI.Texto,
                BackColor = Color.Transparent,
                Location = new Point(80, 108),
                AutoSize = true
            });
            panelDerecho.Controls.Add(new Label
            {
                Text = "Ingresa tus credenciales para continuar",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = EstiloUI.TextoSuave,
                BackColor = Color.Transparent,
                Location = new Point(82, 152),
                AutoSize = true
            });

            txtUsuario = EstiloUI.Caja("Nombre de usuario");
            txtContrasena = EstiloUI.Caja("Contraseña");
            txtContrasena.UseSystemPasswordChar = true;

            EstiloUI.Campo(panelDerecho, "Usuario", txtUsuario, 80, 200, 340);
            EstiloUI.Campo(panelDerecho, "Contraseña", txtContrasena, 80, 268, 340);

            lblMensaje = new Label
            {
                ForeColor = EstiloUI.Rojo,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(80, 334),
                Size = new Size(340, 36),
                AutoSize = false
            };
            panelDerecho.Controls.Add(lblMensaje);

            btnIniciarSesion = EstiloUI.Boton("Iniciar sesión", EstiloUI.Azul, 340);
            btnIniciarSesion.Location = new Point(80, 378);
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            panelDerecho.Controls.Add(btnIniciarSesion);

            // Enter = iniciar sesión
            this.AcceptButton = btnIniciarSesion;

            // ---------- Panel izquierdo: marca (mismo azul oscuro del menú lateral) ----------
            var panelIzquierdo = new Panel
            {
                Dock = DockStyle.Left,
                Width = 360,
                BackColor = Color.FromArgb(17, 24, 46)
            };

            var logo = new Guna2Panel
            {
                Size = new Size(72, 72),
                Location = new Point(40, 190),
                FillColor = EstiloUI.Azul,
                BackColor = Color.FromArgb(17, 24, 46),
                BorderRadius = 36
            };
            logo.Controls.Add(new Label
            {
                Text = "💊",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Emoji", 24F),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            });
            panelIzquierdo.Controls.Add(logo);

            panelIzquierdo.Controls.Add(new Label
            {
                Text = "Farmacia Picado",
                Font = new Font("Segoe UI Semibold", 22F),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(38, 280),
                AutoSize = true
            });
            panelIzquierdo.Controls.Add(new Label
            {
                Text = "Sistema de gestión",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.Transparent,
                Location = new Point(41, 326),
                AutoSize = true
            });

            // Se agrega primero el panel que llena y después el lateral,
            // igual que en FrmMenuPrincipal, para que el acople funcione bien.
            this.Controls.Add(panelDerecho);
            this.Controls.Add(panelIzquierdo);

            this.Load += (s, e) => txtUsuario.Focus();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            try
            {
                Usuario u = negocio.ValidarLogin(txtUsuario.Text, txtContrasena.Text);

                if (u != null)
                {
                    // Program.cs lee este valor y abre el menú principal
                    UsuarioAutenticado = u;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    txtContrasena.Clear();
                    txtContrasena.Focus();
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
