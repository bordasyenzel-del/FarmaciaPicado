namespace FarmaciaPicado
{
    partial class FrmEntradas
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

        // El diseño se construye por código en FrmEntradas.cs (método ConstruirUI).
        // Esto es lo mínimo que Windows Forms necesita para abrir el formulario.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // FrmEntradas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmEntradas";
            this.Text = "FrmEntradas";
            this.ResumeLayout(false);
        }
    }
}
