namespace FarmaciaPicado
{
    partial class FrmReportes
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

        // El diseño se construye por código en FrmReportes.cs (método ConstruirUI).
        // Esto es lo mínimo que Windows Forms necesita para abrir el formulario.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // FrmReportes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmReportes";
            this.Text = "FrmReportes";
            this.ResumeLayout(false);
        }
    }
}
