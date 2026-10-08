namespace FarmaciaPicado
{
    partial class FrmMenuPrincipal
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

        // El diseño ya no se arma con el diseñador visual: se construye por
        // código en FrmMenuPrincipal.cs (método ConstruirUI). Esto es lo
        // mínimo que Windows Forms necesita para poder abrir el formulario.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // FrmMenuPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 860);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FrmMenuPrincipal";
            this.Text = "Farmacia Picado - Menú principal";
            this.Load += new System.EventHandler(this.FrmMenuPrincipal_Load);
            this.ResumeLayout(false);
        }
    }
}
