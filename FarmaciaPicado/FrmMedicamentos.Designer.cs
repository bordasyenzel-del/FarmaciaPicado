namespace FarmaciaPicado
{
    partial class FrmMedicamentos
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
            panelListado = new Panel();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            dgvMedicamentos = new DataGridView();
            panelFormulario = new Panel();
            btnVolver = new Button();
            btnCancelar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnAgregar = new Button();
            dtpFechaVencimiento = new DateTimePicker();
            lblFechaVencimiento = new Label();
            txtStockMinimo = new TextBox();
            lblStockMinimo = new Label();
            txtStockActual = new TextBox();
            lblStockActual = new Label();
            txtPrecioVenta = new TextBox();
            lblPrecioVenta = new Label();
            txtPrecioCompra = new TextBox();
            lblPrecioCompra = new Label();
            cmbCategoria = new ComboBox();
            lblCategoria = new Label();
            txtPresentacion = new TextBox();
            lblPresentacion = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMedicamentos).BeginInit();
            panelFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // panelListado
            // 
            panelListado.Controls.Add(txtBuscar);
            panelListado.Controls.Add(lblBuscar);
            panelListado.Controls.Add(dgvMedicamentos);
            panelListado.Dock = DockStyle.Top;
            panelListado.Location = new Point(0, 0);
            panelListado.Name = "panelListado";
            panelListado.Size = new Size(794, 214);
            panelListado.TabIndex = 0;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(673, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(116, 27);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 12F);
            lblBuscar.Location = new Point(586, 9);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(77, 28);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar :";
            // 
            // dgvMedicamentos
            // 
            dgvMedicamentos.AllowUserToAddRows = false;
            dgvMedicamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMedicamentos.Location = new Point(3, 9);
            dgvMedicamentos.MultiSelect = false;
            dgvMedicamentos.Name = "dgvMedicamentos";
            dgvMedicamentos.ReadOnly = true;
            dgvMedicamentos.RowHeadersWidth = 51;
            dgvMedicamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicamentos.Size = new Size(577, 199);
            dgvMedicamentos.TabIndex = 0;
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.LightGray;
            panelFormulario.BorderStyle = BorderStyle.Fixed3D;
            panelFormulario.Controls.Add(btnVolver);
            panelFormulario.Controls.Add(btnCancelar);
            panelFormulario.Controls.Add(btnEliminar);
            panelFormulario.Controls.Add(btnGuardar);
            panelFormulario.Controls.Add(btnEditar);
            panelFormulario.Controls.Add(btnAgregar);
            panelFormulario.Controls.Add(dtpFechaVencimiento);
            panelFormulario.Controls.Add(lblFechaVencimiento);
            panelFormulario.Controls.Add(txtStockMinimo);
            panelFormulario.Controls.Add(lblStockMinimo);
            panelFormulario.Controls.Add(txtStockActual);
            panelFormulario.Controls.Add(lblStockActual);
            panelFormulario.Controls.Add(txtPrecioVenta);
            panelFormulario.Controls.Add(lblPrecioVenta);
            panelFormulario.Controls.Add(txtPrecioCompra);
            panelFormulario.Controls.Add(lblPrecioCompra);
            panelFormulario.Controls.Add(cmbCategoria);
            panelFormulario.Controls.Add(lblCategoria);
            panelFormulario.Controls.Add(txtPresentacion);
            panelFormulario.Controls.Add(lblPresentacion);
            panelFormulario.Controls.Add(txtNombre);
            panelFormulario.Controls.Add(lblNombre);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(0, 214);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(794, 384);
            panelFormulario.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.CadetBlue;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F);
            btnVolver.Location = new Point(625, 328);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(147, 42);
            btnVolver.TabIndex = 21;
            btnVolver.Text = "Volver al menu";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.CadetBlue;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(513, 330);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 40);
            btnCancelar.TabIndex = 20;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.CadetBlue;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10F);
            btnEliminar.Location = new Point(390, 328);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 42);
            btnEliminar.TabIndex = 19;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.CadetBlue;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10F);
            btnGuardar.ForeColor = SystemColors.ControlText;
            btnGuardar.Location = new Point(260, 328);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 42);
            btnGuardar.TabIndex = 18;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.CadetBlue;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 10F);
            btnEditar.Location = new Point(135, 330);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 40);
            btnEditar.TabIndex = 17;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.CadetBlue;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 10F);
            btnAgregar.Location = new Point(15, 330);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 42);
            btnAgregar.TabIndex = 16;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // dtpFechaVencimiento
            // 
            dtpFechaVencimiento.Location = new Point(225, 250);
            dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            dtpFechaVencimiento.Size = new Size(294, 27);
            dtpFechaVencimiento.TabIndex = 15;
            // 
            // lblFechaVencimiento
            // 
            lblFechaVencimiento.AutoSize = true;
            lblFechaVencimiento.Location = new Point(16, 250);
            lblFechaVencimiento.Name = "lblFechaVencimiento";
            lblFechaVencimiento.Size = new Size(156, 20);
            lblFechaVencimiento.TabIndex = 14;
            lblFechaVencimiento.Text = "Fecha de vencimiento:";
            // 
            // txtStockMinimo
            // 
            txtStockMinimo.Location = new Point(224, 211);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(125, 27);
            txtStockMinimo.TabIndex = 13;
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(15, 216);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(103, 20);
            lblStockMinimo.TabIndex = 12;
            lblStockMinimo.Text = "Stock minimo:";
            // 
            // txtStockActual
            // 
            txtStockActual.Location = new Point(224, 178);
            txtStockActual.Name = "txtStockActual";
            txtStockActual.Size = new Size(125, 27);
            txtStockActual.TabIndex = 11;
            // 
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Location = new Point(15, 176);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(94, 20);
            lblStockActual.TabIndex = 10;
            lblStockActual.Text = "Stock Actual:";
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.Location = new Point(223, 143);
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.Size = new Size(125, 27);
            txtPrecioVenta.TabIndex = 9;
            // 
            // lblPrecioVenta
            // 
            lblPrecioVenta.AutoSize = true;
            lblPrecioVenta.Location = new Point(15, 146);
            lblPrecioVenta.Name = "lblPrecioVenta";
            lblPrecioVenta.Size = new Size(114, 20);
            lblPrecioVenta.TabIndex = 8;
            lblPrecioVenta.Text = "Precio de venta:";
            // 
            // txtPrecioCompra
            // 
            txtPrecioCompra.Location = new Point(223, 110);
            txtPrecioCompra.Name = "txtPrecioCompra";
            txtPrecioCompra.Size = new Size(125, 27);
            txtPrecioCompra.TabIndex = 7;
            // 
            // lblPrecioCompra
            // 
            lblPrecioCompra.AutoSize = true;
            lblPrecioCompra.Location = new Point(14, 113);
            lblPrecioCompra.Name = "lblPrecioCompra";
            lblPrecioCompra.Size = new Size(129, 20);
            lblPrecioCompra.TabIndex = 6;
            lblPrecioCompra.Text = "Precio de compra:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(222, 77);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(151, 28);
            cmbCategoria.TabIndex = 5;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(14, 76);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(77, 20);
            lblCategoria.TabIndex = 4;
            lblCategoria.Text = "Categoria:";
            // 
            // txtPresentacion
            // 
            txtPresentacion.Location = new Point(223, 45);
            txtPresentacion.Name = "txtPresentacion";
            txtPresentacion.Size = new Size(125, 27);
            txtPresentacion.TabIndex = 3;
            // 
            // lblPresentacion
            // 
            lblPresentacion.AutoSize = true;
            lblPresentacion.Location = new Point(14, 46);
            lblPresentacion.Name = "lblPresentacion";
            lblPresentacion.Size = new Size(96, 20);
            lblPresentacion.TabIndex = 2;
            lblPresentacion.Text = "Presentación:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(223, 12);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(14, 12);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // FrmMedicamentos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(794, 598);
            Controls.Add(panelFormulario);
            Controls.Add(panelListado);
            Name = "FrmMedicamentos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia Picado - Medicamentos";
            WindowState = FormWindowState.Maximized;
            Load += FrmMedicamentos_Load;
            panelListado.ResumeLayout(false);
            panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMedicamentos).EndInit();
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelListado;
        private DataGridView dgvMedicamentos;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Panel panelFormulario;
        private TextBox txtPresentacion;
        private Label lblPresentacion;
        private TextBox txtNombre;
        private Label lblNombre;
        private Label lblPrecioCompra;
        private ComboBox cmbCategoria;
        private Label lblCategoria;
        private TextBox txtPrecioVenta;
        private Label lblPrecioVenta;
        private TextBox txtPrecioCompra;
        private TextBox txtStockActual;
        private Label lblStockActual;
        private Label lblStockMinimo;
        private DateTimePicker dtpFechaVencimiento;
        private Label lblFechaVencimiento;
        private TextBox txtStockMinimo;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnCancelar;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnVolver;
    }
}