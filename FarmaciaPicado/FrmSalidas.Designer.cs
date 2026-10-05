namespace FarmaciaPicado
{
    partial class FrmSalidas
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
            dgvSalidas = new DataGridView();
            lblStockDisponible = new Label();
            panelRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSalidas).BeginInit();
            SuspendLayout();
            // 
            // panelRegistro
            // 
            panelRegistro.BackColor = Color.WhiteSmoke;
            panelRegistro.Controls.Add(btnVolver);
            panelRegistro.Controls.Add(btnRegistrar);
            panelRegistro.Controls.Add(nudCantidad);
            panelRegistro.Controls.Add(lblCantidad);
            panelRegistro.Controls.Add(cmbMedicamento);
            panelRegistro.Controls.Add(lblMedicamento);
            panelRegistro.Dock = DockStyle.Top;
            panelRegistro.Location = new Point(0, 0);
            panelRegistro.Name = "panelRegistro";
            panelRegistro.Size = new Size(782, 117);
            panelRegistro.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.DarkSeaGreen;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Location = new Point(535, 64);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(132, 29);
            btnVolver.TabIndex = 4;
            btnVolver.Text = "&Volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.IndianRed;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(534, 26);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(133, 29);
            btnRegistrar.TabIndex = 3;
            btnRegistrar.Text = "&Registrar salida";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(358, 26);
            nudCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(150, 27);
            nudCantidad.TabIndex = 2;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(283, 28);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(72, 20);
            lblCantidad.TabIndex = 1;
            lblCantidad.Text = "Cantidad:";
            // 
            // cmbMedicamento
            // 
            cmbMedicamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedicamento.FormattingEnabled = true;
            cmbMedicamento.Location = new Point(118, 25);
            cmbMedicamento.Name = "cmbMedicamento";
            cmbMedicamento.Size = new Size(151, 28);
            cmbMedicamento.TabIndex = 1;
            // 
            // lblMedicamento
            // 
            lblMedicamento.AutoSize = true;
            lblMedicamento.Location = new Point(12, 28);
            lblMedicamento.Name = "lblMedicamento";
            lblMedicamento.Size = new Size(104, 20);
            lblMedicamento.TabIndex = 0;
            lblMedicamento.Text = "Medicamento:";
            // 
            // dgvSalidas
            // 
            dgvSalidas.AllowUserToAddRows = false;
            dgvSalidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSalidas.Dock = DockStyle.Fill;
            dgvSalidas.Location = new Point(0, 117);
            dgvSalidas.Name = "dgvSalidas";
            dgvSalidas.ReadOnly = true;
            dgvSalidas.RowHeadersWidth = 51;
            dgvSalidas.Size = new Size(782, 436);
            dgvSalidas.TabIndex = 5;
            // 
            // lblStockDisponible
            // 
            lblStockDisponible.AutoSize = true;
            lblStockDisponible.BackColor = Color.PaleGreen;
            lblStockDisponible.Font = new Font("Segoe UI", 10F);
            lblStockDisponible.Location = new Point(0, 521);
            lblStockDisponible.Name = "lblStockDisponible";
            lblStockDisponible.Size = new Size(149, 23);
            lblStockDisponible.TabIndex = 6;
            lblStockDisponible.Text = "Stock disponible: -";
            // 
            // FrmSalidas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(782, 553);
            Controls.Add(lblStockDisponible);
            Controls.Add(dgvSalidas);
            Controls.Add(panelRegistro);
            MaximizeBox = false;
            Name = "FrmSalidas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia Picado - Salidas de Inventario";
            Load += FrmSalidas_Load;
            panelRegistro.ResumeLayout(false);
            panelRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSalidas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelRegistro;
        private ComboBox cmbMedicamento;
        private Label lblMedicamento;
        private Button btnRegistrar;
        private NumericUpDown nudCantidad;
        private Label lblCantidad;
        private Button btnVolver;
        private DataGridView dgvSalidas;
        private Label lblStockDisponible;
    }
}