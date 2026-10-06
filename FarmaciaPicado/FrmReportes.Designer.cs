namespace FarmaciaPicado
{
    partial class FrmReportes
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
            tabReportes = new TabControl();
            tabStockBajo = new TabPage();
            panelCargando = new Panel();
            btnCancelarCarga = new Button();
            lblCargando = new Label();
            dgvStockBajo = new DataGridView();
            tabProximosVencer = new TabPage();
            dgvProximosVencer = new DataGridView();
            tabHistorial = new TabPage();
            dgvHistorial = new DataGridView();
            panelSuperior = new Panel();
            btnVolver = new Button();
            tabReportes.SuspendLayout();
            tabStockBajo.SuspendLayout();
            panelCargando.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStockBajo).BeginInit();
            tabProximosVencer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProximosVencer).BeginInit();
            tabHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            panelSuperior.SuspendLayout();
            SuspendLayout();
            // 
            // tabReportes
            // 
            tabReportes.Controls.Add(tabStockBajo);
            tabReportes.Controls.Add(tabProximosVencer);
            tabReportes.Controls.Add(tabHistorial);
            tabReportes.Dock = DockStyle.Bottom;
            tabReportes.Location = new Point(0, 56);
            tabReportes.Name = "tabReportes";
            tabReportes.SelectedIndex = 0;
            tabReportes.Size = new Size(800, 394);
            tabReportes.TabIndex = 0;
            // 
            // tabStockBajo
            // 
            tabStockBajo.Controls.Add(dgvStockBajo);
            tabStockBajo.Location = new Point(4, 29);
            tabStockBajo.Name = "tabStockBajo";
            tabStockBajo.Padding = new Padding(3);
            tabStockBajo.Size = new Size(792, 361);
            tabStockBajo.TabIndex = 0;
            tabStockBajo.Text = "Stock Bajo";
            tabStockBajo.UseVisualStyleBackColor = true;
            // 
            // panelCargando
            // 
            panelCargando.BackColor = Color.Gainsboro;
            panelCargando.Controls.Add(btnCancelarCarga);
            panelCargando.Controls.Add(lblCargando);
            panelCargando.Dock = DockStyle.Fill;
            panelCargando.Location = new Point(3, 3);
            panelCargando.Name = "panelCargando";
            panelCargando.Size = new Size(786, 355);
            panelCargando.TabIndex = 2;
            panelCargando.Visible = false;
            // 
            // btnCancelarCarga
            // 
            btnCancelarCarga.BackColor = Color.Red;
            btnCancelarCarga.FlatStyle = FlatStyle.Flat;
            btnCancelarCarga.ForeColor = Color.White;
            btnCancelarCarga.Location = new Point(354, 245);
            btnCancelarCarga.Name = "btnCancelarCarga";
            btnCancelarCarga.Size = new Size(94, 29);
            btnCancelarCarga.TabIndex = 1;
            btnCancelarCarga.Text = "Cancelar";
            btnCancelarCarga.UseVisualStyleBackColor = false;
            btnCancelarCarga.Click += btnCancelarCarga_Click;
            // 
            // lblCargando
            // 
            lblCargando.Font = new Font("Segoe UI", 16F);
            lblCargando.ForeColor = Color.DarkCyan;
            lblCargando.Location = new Point(311, 147);
            lblCargando.Name = "lblCargando";
            lblCargando.Size = new Size(187, 44);
            lblCargando.TabIndex = 0;
            lblCargando.Text = "Cargando....";
            lblCargando.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvStockBajo
            // 
            dgvStockBajo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStockBajo.Dock = DockStyle.Fill;
            dgvStockBajo.Location = new Point(3, 3);
            dgvStockBajo.Name = "dgvStockBajo";
            dgvStockBajo.ReadOnly = true;
            dgvStockBajo.RowHeadersWidth = 51;
            dgvStockBajo.Size = new Size(786, 355);
            dgvStockBajo.TabIndex = 0;
            // 
            // tabProximosVencer
            // 
            tabProximosVencer.Controls.Add(dgvProximosVencer);
            tabProximosVencer.Location = new Point(4, 29);
            tabProximosVencer.Name = "tabProximosVencer";
            tabProximosVencer.Padding = new Padding(3);
            tabProximosVencer.Size = new Size(792, 361);
            tabProximosVencer.TabIndex = 1;
            tabProximosVencer.Text = "proximos a vencer";
            tabProximosVencer.UseVisualStyleBackColor = true;
            // 
            // dgvProximosVencer
            // 
            dgvProximosVencer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProximosVencer.Dock = DockStyle.Fill;
            dgvProximosVencer.Location = new Point(3, 3);
            dgvProximosVencer.Name = "dgvProximosVencer";
            dgvProximosVencer.ReadOnly = true;
            dgvProximosVencer.RowHeadersWidth = 51;
            dgvProximosVencer.Size = new Size(786, 355);
            dgvProximosVencer.TabIndex = 0;
            // 
            // tabHistorial
            // 
            tabHistorial.Controls.Add(dgvHistorial);
            tabHistorial.Location = new Point(4, 29);
            tabHistorial.Name = "tabHistorial";
            tabHistorial.Padding = new Padding(3);
            tabHistorial.Size = new Size(792, 361);
            tabHistorial.TabIndex = 2;
            tabHistorial.Text = "Historial de Movimientos";
            tabHistorial.UseVisualStyleBackColor = true;
            // 
            // dgvHistorial
            // 
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorial.Dock = DockStyle.Fill;
            dgvHistorial.Location = new Point(3, 3);
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.RowHeadersWidth = 51;
            dgvHistorial.Size = new Size(786, 355);
            dgvHistorial.TabIndex = 0;
            // 
            // panelSuperior
            // 
            panelSuperior.Controls.Add(btnVolver);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(800, 50);
            panelSuperior.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.DarkCyan;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(25, 12);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(137, 29);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "volver al menú";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmReportes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(panelCargando);
            Controls.Add(panelSuperior);
            Controls.Add(tabReportes);
            Name = "FrmReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia Picado - Reportes";
            WindowState = FormWindowState.Maximized;
            Load += FrmReportes_Load;
            tabReportes.ResumeLayout(false);
            tabStockBajo.ResumeLayout(false);
            panelCargando.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStockBajo).EndInit();
            tabProximosVencer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProximosVencer).EndInit();
            tabHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            panelSuperior.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabReportes;
        private TabPage tabStockBajo;
        private TabPage tabProximosVencer;
        private TabPage tabHistorial;
        private DataGridView dgvStockBajo;
        private DataGridView dgvProximosVencer;
        private DataGridView dgvHistorial;
        private Panel panelSuperior;
        private Button btnVolver;
        private Panel panelCargando;
        private Button btnCancelarCarga;
        private Label lblCargando;
    }
}