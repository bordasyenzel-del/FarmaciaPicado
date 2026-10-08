namespace FarmaciaPicado
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // El diseño se construye por código en FrmLogin.cs (método ConstruirUI).
        // Esto es lo mínimo que Windows Forms necesita para abrir el formulario.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // FrmLogin
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 520);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FrmLogin";
            this.Text = "Farmacia Picado - Iniciar sesión";
            this.ResumeLayout(false);
        }
    }
}
