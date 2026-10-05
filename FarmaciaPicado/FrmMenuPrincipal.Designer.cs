namespace FarmaciaPicado
{
    partial class FrmMenuPrincipal
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
            panelMenu = new Panel();
            btnCerrarSesion = new Button();
            btnReportes = new Button();
            btnUsuarios = new Button();
            btnSalidas = new Button();
            btnEntradas = new Button();
            btnCategorias = new Button();
            btnMedicamentos = new Button();
            lblUsuarioActivo = new Label();
            panelContenido = new Panel();
            lblProximosVencer = new Label();
            lblStockBajo = new Label();
            lblTotalMedicamentos = new Label();
            panelMenu.SuspendLayout();
            panelContenido.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.DarkTurquoise;
            panelMenu.Controls.Add(btnCerrarSesion);
            panelMenu.Controls.Add(btnReportes);
            panelMenu.Controls.Add(btnUsuarios);
            panelMenu.Controls.Add(btnSalidas);
            panelMenu.Controls.Add(btnEntradas);
            panelMenu.Controls.Add(btnCategorias);
            panelMenu.Controls.Add(btnMedicamentos);
            panelMenu.Controls.Add(lblUsuarioActivo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(250, 450);
            panelMenu.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.BackColor = Color.Firebrick;
            btnCerrarSesion.Dock = DockStyle.Bottom;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Location = new Point(0, 239);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(250, 29);
            btnCerrarSesion.TabIndex = 8;
            btnCerrarSesion.Text = "&Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.PowderBlue;
            btnReportes.Dock = DockStyle.Bottom;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.ForeColor = Color.Black;
            btnReportes.Location = new Point(0, 268);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(250, 31);
            btnReportes.TabIndex = 7;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.BackColor = Color.PowderBlue;
            btnUsuarios.Dock = DockStyle.Bottom;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.ForeColor = Color.Black;
            btnUsuarios.Location = new Point(0, 299);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(250, 29);
            btnUsuarios.TabIndex = 6;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnSalidas
            // 
            btnSalidas.BackColor = Color.PowderBlue;
            btnSalidas.Dock = DockStyle.Bottom;
            btnSalidas.FlatStyle = FlatStyle.Flat;
            btnSalidas.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSalidas.Location = new Point(0, 328);
            btnSalidas.Name = "btnSalidas";
            btnSalidas.Size = new Size(250, 29);
            btnSalidas.TabIndex = 5;
            btnSalidas.Text = "Salidas";
            btnSalidas.UseVisualStyleBackColor = false;
            btnSalidas.Click += btnSalidas_Click;
            // 
            // btnEntradas
            // 
            btnEntradas.BackColor = Color.LightBlue;
            btnEntradas.Dock = DockStyle.Bottom;
            btnEntradas.FlatStyle = FlatStyle.Flat;
            btnEntradas.Location = new Point(0, 357);
            btnEntradas.Name = "btnEntradas";
            btnEntradas.Size = new Size(250, 29);
            btnEntradas.TabIndex = 4;
            btnEntradas.Text = "Entradas";
            btnEntradas.UseVisualStyleBackColor = false;
            btnEntradas.Click += btnEntradas_Click;
            // 
            // btnCategorias
            // 
            btnCategorias.BackColor = Color.PowderBlue;
            btnCategorias.Dock = DockStyle.Bottom;
            btnCategorias.FlatStyle = FlatStyle.Flat;
            btnCategorias.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCategorias.ForeColor = Color.Black;
            btnCategorias.Location = new Point(0, 386);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(250, 35);
            btnCategorias.TabIndex = 3;
            btnCategorias.Text = "Categorias";
            btnCategorias.UseVisualStyleBackColor = false;
            btnCategorias.Click += btnCategorias_Click;
            // 
            // btnMedicamentos
            // 
            btnMedicamentos.BackColor = Color.PowderBlue;
            btnMedicamentos.Dock = DockStyle.Bottom;
            btnMedicamentos.FlatStyle = FlatStyle.Flat;
            btnMedicamentos.ForeColor = Color.Black;
            btnMedicamentos.Location = new Point(0, 421);
            btnMedicamentos.Name = "btnMedicamentos";
            btnMedicamentos.Size = new Size(250, 29);
            btnMedicamentos.TabIndex = 2;
            btnMedicamentos.Text = "Medicamentos";
            btnMedicamentos.UseVisualStyleBackColor = false;
            btnMedicamentos.Click += btnMedicamentos_Click;
            // 
            // lblUsuarioActivo
            // 
            lblUsuarioActivo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuarioActivo.ForeColor = Color.White;
            lblUsuarioActivo.Location = new Point(3, 9);
            lblUsuarioActivo.Name = "lblUsuarioActivo";
            lblUsuarioActivo.Size = new Size(241, 54);
            lblUsuarioActivo.TabIndex = 1;
            lblUsuarioActivo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.White;
            panelContenido.Controls.Add(lblProximosVencer);
            panelContenido.Controls.Add(lblStockBajo);
            panelContenido.Controls.Add(lblTotalMedicamentos);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(250, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(550, 450);
            panelContenido.TabIndex = 1;
            // 
            // lblProximosVencer
            // 
            lblProximosVencer.AutoSize = true;
            lblProximosVencer.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProximosVencer.ForeColor = Color.Red;
            lblProximosVencer.Location = new Point(27, 183);
            lblProximosVencer.Name = "lblProximosVencer";
            lblProximosVencer.Size = new Size(210, 28);
            lblProximosVencer.TabIndex = 2;
            lblProximosVencer.Text = "Proximos a vencer: 0";
            // 
            // lblStockBajo
            // 
            lblStockBajo.AutoSize = true;
            lblStockBajo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockBajo.Location = new Point(27, 122);
            lblStockBajo.Name = "lblStockBajo";
            lblStockBajo.Size = new Size(317, 28);
            lblStockBajo.TabIndex = 1;
            lblStockBajo.Text = "Medicamentos con stock bajo: 0";
            // 
            // lblTotalMedicamentos
            // 
            lblTotalMedicamentos.AutoSize = true;
            lblTotalMedicamentos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalMedicamentos.Location = new Point(27, 64);
            lblTotalMedicamentos.Name = "lblTotalMedicamentos";
            lblTotalMedicamentos.Size = new Size(255, 28);
            lblTotalMedicamentos.TabIndex = 0;
            lblTotalMedicamentos.Text = "Total de medicamentos: 0";
            // 
            // FrmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Name = "FrmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Farmacia picado - Menú principal";
            WindowState = FormWindowState.Maximized;
            Load += FrmMenuPrincipal_Load;
            panelMenu.ResumeLayout(false);
            panelContenido.ResumeLayout(false);
            panelContenido.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Label lblUsuarioActivo;
        private Button btnMedicamentos;
        private Button btnEntradas;
        private Button btnCategorias;
        private Button btnSalidas;
        private Button btnReportes;
        private Button btnUsuarios;
        private Button btnCerrarSesion;
        private Panel panelContenido;
        private Label lblTotalMedicamentos;
        private Label lblStockBajo;
        private Label lblProximosVencer;
    }
}