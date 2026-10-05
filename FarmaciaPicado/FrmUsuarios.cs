using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarmaciaPicado.Negocio;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioNegocio negocio = new UsuarioNegocio();
        private int idUsuarioSeleccionado = 0;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();
            CargarUsuarios();
            HabilitarCampos(false);
        }

        private void CargarRoles()
        {
            try
            {
                List<Rol> roles = negocio.ObtenerRoles();
                cmbRol.DataSource = roles;
                cmbRol.DisplayMember = "NombreRol";
                cmbRol.ValueMember = "IdRol";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                List<Usuario> usuarios = negocio.Obtener();
                dgvUsuarios.DataSource = usuarios;

                if (dgvUsuarios.Columns["IdUsuario"] != null)
                    dgvUsuarios.Columns["IdUsuario"].Visible = false;
                if (dgvUsuarios.Columns["Contrasena"] != null)
                    dgvUsuarios.Columns["Contrasena"].Visible = false;
                if (dgvUsuarios.Columns["IdRol"] != null)
                    dgvUsuarios.Columns["IdRol"].Visible = false;

                if (dgvUsuarios.Columns["NombreUsuario"] != null)
                    dgvUsuarios.Columns["NombreUsuario"].HeaderText = "Usuario";
                if (dgvUsuarios.Columns["Rol"] != null)
                    dgvUsuarios.Columns["Rol"].HeaderText = "Rol";
                if (dgvUsuarios.Columns["Activo"] != null)
                    dgvUsuarios.Columns["Activo"].HeaderText = "Activo";

                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtNombreUsuario.Enabled = habilitar;
            txtContraseñaUsuario.Enabled = habilitar;
            cmbRol.Enabled = habilitar;
            chkActivo.Enabled = habilitar;

            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;

            btnAgregar.Enabled = !habilitar;
            btnEditar.Enabled = !habilitar;
            btnEliminar.Enabled = !habilitar;
            dgvUsuarios.Enabled = !habilitar;
        }

        private void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtContraseñaUsuario.Clear();
            if (cmbRol.Items.Count > 0)
                cmbRol.SelectedIndex = 0;
            chkActivo.Checked = true;
            idUsuarioSeleccionado = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            idUsuarioSeleccionado = 0;
            LimpiarCampos();
            HabilitarCampos(true);
            txtNombreUsuario.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario de la lista para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Usuario u = (Usuario)dgvUsuarios.SelectedRows[0].DataBoundItem;

            idUsuarioSeleccionado = u.IdUsuario;
            txtNombreUsuario.Text = u.NombreUsuario;
            txtContraseñaUsuario.Text = ""; // nunca se muestra la contraseña actual
            cmbRol.Text = u.Rol;
            chkActivo.Checked = u.Activo;

            HabilitarCampos(true);
            txtNombreUsuario.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario
            {
                IdUsuario = idUsuarioSeleccionado,
                NombreUsuario = txtNombreUsuario.Text.Trim(),
                IdRol = Convert.ToInt32(cmbRol.SelectedValue),
                Activo = chkActivo.Checked,
            };

            try
            {
                negocio.Guardar(u, txtContraseñaUsuario.Text);

                MessageBox.Show("Usuario guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                HabilitarCampos(false);
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario de la lista para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Usuario u = (Usuario)dgvUsuarios.SelectedRows[0].DataBoundItem;

            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro que desea eliminar al usuario \"{u.NombreUsuario}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                negocio.Eliminar(u.IdUsuario);

                MessageBox.Show("Usuario eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar el usuario.\n" +
                    "Puede que tenga movimientos de entradas o salidas asociados.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(false);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}