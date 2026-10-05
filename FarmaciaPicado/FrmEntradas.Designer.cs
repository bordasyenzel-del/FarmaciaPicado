namespace FarmaciaPicado
{
    partial class FrmEntradas
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
            panelRegistro = new Panel();
            btnVolver = new Button();
            btnRegistrar = new Button();
            nudCantidad = new NumericUpDown();
            lblCantidad = new Label();
            cmbMedicamento = new ComboBox();
            lblMedicamento = new Label();
            dgvEntradas = new DataGridView();
            panelRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEntradas).BeginInit();
            SuspendLayout();
            // 
            // panelRegistro
            // 
            panelRegistro.Controls.Add(btnVolver);
            panelRegistro.Controls.Add(btnRegistrar);
            panelRegistro.Controls.Add(nudCantidad);
            panelRegistro.Controls.Add(lblCantidad);
            panelRegistro.Controls.Add(cmbMedicamento);
            panelRegistro.Controls.Add(lblMedicamento);
            panelRegistro.Dock = DockStyle.Top;
            panelRegistro.Location = new Point(0, 0);
            panelRegistro.Name = "panelRegistro";
            panelRegistro.Size = new Size(782, 87);
            panelRegistro.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.DarkRed;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(635, 17);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(135, 29);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "&Volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.Green;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(525, 16);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 29);
            btnRegistrar.TabIndex = 4;
            btnRegistrar.Text = "&Registrar entrada";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(357, 17);
            nudCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(150, 27);
            nudCantidad.TabIndex = 3;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(282, 21);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(72, 20);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Cantidad:";
            // 
            // cmbMedicamento
            // 
            cmbMedicamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedicamento.FormattingEnabled = true;
            cmbMedicamento.Location = new Point(116, 21);
            cmbMedicamento.Name = "cmbMedicamento";
            cmbMedicamento.Size = new Size(151, 28);
            cmbMedicamento.TabIndex = 1;
            // 
            // lblMedicamento
            // 
            lblMedicamento.AutoSize = true;
            lblMedicamento.Location = new Point(7, 21);
            lblMedicamento.Name = "lblMedicamento";
            lblMedicamento.Size = new Size(104, 20);
            lblMedicamento.TabIndex = 0;
            lblMedicamento.Text = "Medicamento:";
            // 
            // dgvEntradas
            // 
            dgvEntradas.AllowUserToAddRows = false;
            dgvEntradas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntradas.Dock = DockStyle.Fill;
            dgvEntradas.Location = new Point(0, 87);
            dgvEntradas.Name = "dgvEntradas";
            dgvEntradas.ReadOnly = true;
            dgvEntradas.RowHeadersWidth = 51;
            dgvEntradas.Size = new Size(782, 466);
            dgvEntradas.TabIndex = 1;
            // 
            // FrmEntradas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(782, 553);
            Controls.Add(dgvEntradas);
            Controls.Add(panelRegistro);
            MaximizeBox = false;
            Name = "FrmEntradas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia Picado - Entradas de Inventario";
            Load += FrmEntradas_Load;
            panelRegistro.ResumeLayout(false);
            panelRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEntradas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRegistro;
        private ComboBox cmbMedicamento;
        private Label lblMedicamento;
        private Button btnRegistrar;
        private NumericUpDown nudCantidad;
        private Label lblCantidad;
        private Button btnVolver;
        private DataGridView dgvEntradas;
    }
}