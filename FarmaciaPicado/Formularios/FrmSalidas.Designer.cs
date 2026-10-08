namespace FarmaciaPicado
{
    partial class FrmSalidas
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

        // El diseño se construye por código en FrmSalidas.cs (método ConstruirUI).
        // Esto es lo mínimo que Windows Forms necesita para abrir el formulario.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // FrmSalidas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmSalidas";
            this.Text = "FrmSalidas";
            this.ResumeLayout(false);
        }
    }
}
