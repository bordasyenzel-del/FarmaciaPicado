using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace FarmaciaPicado
{
    // Ventana de carga con botón «Cancelar».
    // Se construye por completo en código (no necesita Designer).
    // Al pulsar Cancelar (o Esc) se cancela el CancellationToken del proceso en curso.
    public class DialogoProgreso : Form
    {
        private readonly CancellationTokenSource cts;
        private readonly Label lblMensaje;
        private readonly Guna2Button btnCancelar;
        private bool permitirCerrar = false;

        public DialogoProgreso(string mensaje, CancellationTokenSource cts)
        {
            this.cts = cts;

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            KeyPreview = true;
            BackColor = Color.White;
            ClientSize = new Size(380, 160);
            Font = new Font("Segoe UI", 9F);

            lblMensaje = new Label
            {
                Text = mensaje,
                Font = new Font("Segoe UI Semibold", 11F),
                ForeColor = EstiloUI.Texto,
                BackColor = Color.White,
                Location = new Point(24, 22),
                Size = new Size(332, 26)
            };

            var barra = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Location = new Point(24, 62),
                Size = new Size(332, 12)
            };

            btnCancelar = EstiloUI.Boton("Cancelar", EstiloUI.Rojo, 120);
            btnCancelar.Location = new Point(236, 100);
            btnCancelar.Click += (s, e) => Cancelar();

            Controls.Add(lblMensaje);
            Controls.Add(barra);
            Controls.Add(btnCancelar);

            Paint += (s, e) =>
            {
                using (var lapiz = new Pen(EstiloUI.Borde))
                    e.Graphics.DrawRectangle(lapiz, 0, 0, Width - 1, Height - 1);
            };

            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) Cancelar();
            };
        }

        private void Cancelar()
        {
            if (!btnCancelar.Enabled) return;

            btnCancelar.Enabled = false;
            lblMensaje.Text = "Cancelando...";

            try { cts.Cancel(); }
            catch (ObjectDisposedException) { }
        }

        // Cierra la ventana cuando el proceso termina (la X o Alt+F4 solo cancelan)
        public void CerrarDialogo()
        {
            permitirCerrar = true;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!permitirCerrar && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Cancelar();
                return;
            }
            base.OnFormClosing(e);
        }
    }
}
