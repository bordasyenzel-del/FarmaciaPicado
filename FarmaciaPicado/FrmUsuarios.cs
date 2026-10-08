using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioDAO dao = new UsuarioDAO();
        private List<Rol> roles = new List<Rol>();
        private int idSeleccionado = 0;
        private bool cargando = false;

        private Label lblModo;
        private Guna2TextBox txtUsuario, txtContrasena;
        private Guna2ComboBox cboRol;
        private Guna2CheckBox chkActivo;
        private Guna2DataGridView dgv;
        private Guna2Button btnGuardar, btnNuevo, btnEliminar;

        public FrmUsuarios()
        {
            InitializeComponent();
            ConstruirUI();
            this.Load += (s, e) =>
            {
                try { CargarRoles(); CargarLista(); Limpiar(); }
                catch (Exception ex) { EstiloUI.Error("Error al cargar los datos:\n" + ex.Message); }
            };
        }

        private void ConstruirUI()
        {
            var cuerpo = EstiloUI.ConfigurarForm(this, "Usuarios", "Administración de cuentas y roles de acceso", 1100, 660);
            EstiloUI.DosColumnas(cuerpo, 380, out Guna2Panel izq, out Guna2Panel der);

            // ---------- Formulario ----------
            lblModo = EstiloUI.TituloTarjeta("Nuevo usuario");
            izq.Controls.Add(lblModo);

            txtUsuario = EstiloUI.Caja("Nombre de usuario");
            txtContrasena = EstiloUI.Caja("Contraseña");
            txtContrasena.UseSystemPasswordChar = true;
            cboRol = EstiloUI.Combo();

            EstiloUI.Campo(izq, "Usuario", txtUsuario, 20, 56, 340);
            EstiloUI.Campo(izq, "Contraseña", txtContrasena, 20, 118, 340);
            EstiloUI.Campo(izq, "Rol", cboRol, 20, 180, 340);

            chkActivo = new Guna2CheckBox
            {
                Text = "Usuario activo",
                Checked = true,
                BackColor = Color.White,
                ForeColor = EstiloUI.Texto,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 250),
                AutoSize = true
            };
            izq.Controls.Add(chkActivo);

            btnGuardar = EstiloUI.Boton("Guardar", EstiloUI.Azul, 340);
            btnGuardar.Location = new Point(20, 300);
            btnNuevo = EstiloUI.Boton("Nuevo", EstiloUI.TextoSuave, 165);
            btnNuevo.Location = new Point(20, 350);
            btnEliminar = EstiloUI.Boton("Eliminar", EstiloUI.Rojo, 165);
            btnEliminar.Location = new Point(195, 350);

            btnGuardar.Click += BtnGuardar_Click;
            btnNuevo.Click += (s, e) => Limpiar();
            btnEliminar.Click += BtnEliminar_Click;

            izq.Controls.Add(btnGuardar);
            izq.Controls.Add(btnNuevo);
            izq.Controls.Add(btnEliminar);

            // ---------- Listado ----------
            der.Padding = new Padding(20, 56, 20, 20);
            dgv = EstiloUI.CrearGrid();
            dgv.Columns.Add("Usuario", "Usuario");
            dgv.Columns.Add("Rol", "Rol");
            dgv.Columns.Add("Estado", "Estado");
            EstiloUI.DesactivarOrden(dgv);
            dgv.SelectionChanged += Dgv_SelectionChanged;

            der.Controls.Add(dgv);
            der.Controls.Add(EstiloUI.TituloTarjeta("Usuarios registrados"));
        }

        private void CargarRoles()
        {
            roles = dao.ObtenerRoles();
            cboRol.DataSource = null;
            cboRol.DisplayMember = "NombreRol";
            cboRol.ValueMember = "IdRol";
            cboRol.DataSource = roles;
        }

        private void CargarLista()
        {
            cargando = true;
            dgv.Rows.Clear();
            foreach (Usuario u in dao.Obtener())
            {
                int i = dgv.Rows.Add(u.NombreUsuario, u.Rol, u.Activo ? "Activo" : "Inactivo");
                var fila = dgv.Rows[i];
                fila.Tag = u;
                fila.Cells["Estado"].Style.ForeColor = u.Activo ? EstiloUI.Verde : EstiloUI.Rojo;
            }
            dgv.ClearSelection();
            dgv.CurrentCell = null;
            cargando = false;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || dgv.SelectedRows.Count == 0) return;
            if (!(dgv.SelectedRows[0].Tag is Usuario u)) return;

            idSeleccionado = u.IdUsuario;
            txtUsuario.Text = u.NombreUsuario;
            txtContrasena.Clear();
            txtContrasena.PlaceholderText = "Dejar vacío para no cambiarla";
            chkActivo.Checked = u.Activo;

            var rol = roles.FirstOrDefault(r => r.NombreRol == u.Rol);
            if (rol != null) cboRol.SelectedValue = rol.IdRol;

            lblModo.Text = "Editar usuario";
            btnEliminar.Enabled = true;
        }

        private void Limpiar()
        {
            idSeleccionado = 0;
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtContrasena.PlaceholderText = "Contraseña";
            cboRol.SelectedIndex = roles.Count > 0 ? 0 : -1;
            chkActivo.Checked = true;
            lblModo.Text = "Nuevo usuario";
            btnEliminar.Enabled = false;
            dgv.ClearSelection();
            txtUsuario.Focus();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (nombre == "") { EstiloUI.Aviso("Ingrese el nombre de usuario."); return; }
            if (cboRol.SelectedValue == null) { EstiloUI.Aviso("Seleccione un rol."); return; }
            if (idSeleccionado == 0 && contrasena == "") { EstiloUI.Aviso("Ingrese una contraseña para el nuevo usuario."); return; }

            var u = new Usuario
            {
                IdUsuario = idSeleccionado,
                NombreUsuario = nombre,
                Contrasena = contrasena,
                IdRol = Convert.ToInt32(cboRol.SelectedValue),
                Activo = chkActivo.Checked
            };

            try
            {
                if (idSeleccionado == 0) dao.Insertar(u);
                else if (contrasena == "") dao.ActualizarSinContrasena(u);
                else dao.ActualizarConContrasena(u);

                EstiloUI.Info(idSeleccionado == 0 ? "Usuario creado." : "Usuario actualizado.");
                CargarLista();
                Limpiar();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo guardar:\n" + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0) { EstiloUI.Aviso("Seleccione un usuario de la lista."); return; }
            if (!EstiloUI.Confirmar("¿Eliminar al usuario \"" + txtUsuario.Text + "\"?")) return;

            try
            {
                dao.Eliminar(idSeleccionado);
                CargarLista();
                Limpiar();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo eliminar. Puede que tenga movimientos registrados; en ese caso desactívalo en vez de eliminarlo.\n\n" + ex.Message);
            }
        }
    }
}
