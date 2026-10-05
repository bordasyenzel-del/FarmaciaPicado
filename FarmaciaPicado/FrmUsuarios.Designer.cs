namespace FarmaciaPicado
{
    partial class FrmUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvUsuarios = new DataGridView();
            panelFormulario = new Panel();
            btnVolver = new Button();
            btnCancelar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnAgregar = new Button();
            chkActivo = new CheckBox();
            lblActivo = new Label();
            cmbRol = new ComboBox();
            lblRol = new Label();
            txtContraseñaUsuario = new TextBox();
            lblContraseña = new Label();
            txtNombreUsuario = new TextBox();
            lblUsuario = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            panelFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Dock = DockStyle.Top;
            dgvUsuarios.Location = new Point(0, 0);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(682, 272);
            dgvUsuarios.TabIndex = 0;
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.PaleTurquoise;
            panelFormulario.Controls.Add(btnVolver);
            panelFormulario.Controls.Add(btnCancelar);
            panelFormulario.Controls.Add(btnEliminar);
            panelFormulario.Controls.Add(btnGuardar);
            panelFormulario.Controls.Add(btnEditar);
            panelFormulario.Controls.Add(btnAgregar);
            panelFormulario.Controls.Add(chkActivo);
            panelFormulario.Controls.Add(lblActivo);
            panelFormulario.Controls.Add(cmbRol);
            panelFormulario.Controls.Add(lblRol);
            panelFormulario.Controls.Add(txtContraseñaUsuario);
            panelFormulario.Controls.Add(lblContraseña);
            panelFormulario.Controls.Add(txtNombreUsuario);
            panelFormulario.Controls.Add(lblUsuario);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(0, 272);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(682, 231);
            panelFormulario.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.DarkSlateGray;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(547, 190);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(119, 29);
            btnVolver.TabIndex = 13;
            btnVolver.Text = "Volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.DarkSlateGray;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(446, 190);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.DarkSlateGray;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(336, 190);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "&Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.DarkSlateGray;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(227, 190);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "&Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.DarkSlateGray;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(115, 190);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.DarkSlateGray;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(9, 190);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 8;
            btnAgregar.Text = "&Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(67, 121);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(18, 17);
            chkActivo.TabIndex = 7;
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // lblActivo
            // 
            lblActivo.AutoSize = true;
            lblActivo.Location = new Point(11, 121);
            lblActivo.Name = "lblActivo";
            lblActivo.Size = new Size(54, 20);
            lblActivo.TabIndex = 6;
            lblActivo.Text = "Activo:";
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(74, 87);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(151, 28);
            cmbRol.TabIndex = 5;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new Point(14, 89);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(34, 20);
            lblRol.TabIndex = 4;
            lblRol.Text = "Rol:";
            // 
            // txtContraseñaUsuario
            // 
            txtContraseñaUsuario.Location = new Point(109, 48);
            txtContraseñaUsuario.Name = "txtContraseñaUsuario";
            txtContraseñaUsuario.PasswordChar = '*';
            txtContraseñaUsuario.Size = new Size(125, 27);
            txtContraseñaUsuario.TabIndex = 3;
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(13, 50);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(86, 20);
            lblContraseña.TabIndex = 2;
            lblContraseña.Text = "Contraseña:";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(84, 8);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(125, 27);
            txtNombreUsuario.TabIndex = 1;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(16, 9);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(62, 20);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(682, 503);
            Controls.Add(panelFormulario);
            Controls.Add(dgvUsuarios);
            MaximizeBox = false;
            Name = "FrmUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia Picado - Gestión de Usuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsuarios;
        private Panel panelFormulario;
        private Label lblRol;
        private TextBox txtContraseñaUsuario;
        private Label lblContraseña;
        private TextBox txtNombreUsuario;
        private Label lblUsuario;
        private CheckBox chkActivo;
        private Label lblActivo;
        private ComboBox cmbRol;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnAgregar;
        private Button btnVolver;
        private Button btnCancelar;
    }
}