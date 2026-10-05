namespace FarmaciaPicado
{
    partial class FrmCategorias
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
            dgvCategorias = new DataGridView();
            panelFormulario = new Panel();
            btnVolver = new Button();
            btnCancelar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnAgregar = new Button();
            txtNombreCategoria = new TextBox();
            lblNombreCategoria = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            panelFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCategorias
            // 
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Dock = DockStyle.Top;
            dgvCategorias.Location = new Point(0, 0);
            dgvCategorias.MultiSelect = false;
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.ReadOnly = true;
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.Size = new Size(582, 188);
            dgvCategorias.TabIndex = 0;
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.WhiteSmoke;
            panelFormulario.Controls.Add(btnVolver);
            panelFormulario.Controls.Add(btnCancelar);
            panelFormulario.Controls.Add(btnEliminar);
            panelFormulario.Controls.Add(btnGuardar);
            panelFormulario.Controls.Add(btnEditar);
            panelFormulario.Controls.Add(btnAgregar);
            panelFormulario.Controls.Add(txtNombreCategoria);
            panelFormulario.Controls.Add(lblNombreCategoria);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(0, 188);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(582, 265);
            panelFormulario.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.DarkSlateGray;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(432, 231);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(135, 29);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "&Volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.DarkSlateGray;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(18, 231);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.DarkRed;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(16, 190);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "&Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.YellowGreen;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Location = new Point(17, 151);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "&Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.DarkSlateGray;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(16, 112);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.DarkSlateGray;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(17, 74);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "&Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtNombreCategoria
            // 
            txtNombreCategoria.Location = new Point(202, 31);
            txtNombreCategoria.Name = "txtNombreCategoria";
            txtNombreCategoria.Size = new Size(125, 27);
            txtNombreCategoria.TabIndex = 1;
            // 
            // lblNombreCategoria
            // 
            lblNombreCategoria.AutoSize = true;
            lblNombreCategoria.Location = new Point(17, 33);
            lblNombreCategoria.Name = "lblNombreCategoria";
            lblNombreCategoria.Size = new Size(171, 20);
            lblNombreCategoria.TabIndex = 0;
            lblNombreCategoria.Text = "Nombre de la categoria:";
            // 
            // FrmCategorias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(582, 453);
            Controls.Add(panelFormulario);
            Controls.Add(dgvCategorias);
            MinimizeBox = false;
            Name = "FrmCategorias";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia Picado - Categorías";
            Load += FrmCategorias_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCategorias;
        private Panel panelFormulario;
        private TextBox txtNombreCategoria;
        private Label lblNombreCategoria;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnCancelar;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnVolver;
    }
}