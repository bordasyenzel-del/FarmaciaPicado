using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace FarmaciaPicado
{
    // Estilos y helpers compartidos por los formularios secundarios,
    // para que todos tengan el mismo diseño del menú principal.
    public static class EstiloUI
    {
        public static readonly Color Fondo = Color.FromArgb(243, 244, 247);
        public static readonly Color Azul = Color.FromArgb(37, 99, 235);
        public static readonly Color Verde = Color.FromArgb(16, 185, 129);
        public static readonly Color Rojo = Color.FromArgb(239, 68, 68);
        public static readonly Color Morado = Color.FromArgb(139, 92, 246);
        public static readonly Color Naranja = Color.FromArgb(245, 158, 11);
        public static readonly Color Texto = Color.FromArgb(31, 41, 55);
        public static readonly Color TextoSuave = Color.FromArgb(107, 114, 128);
        public static readonly Color Borde = Color.FromArgb(229, 231, 235);

        // Configura el formulario (fondo, tamaño, encabezado) y devuelve el panel del cuerpo.
        public static Panel ConfigurarForm(Form f, string titulo, string subtitulo, int ancho, int alto)
        {
            f.Text = titulo + " - Farmacia Picado";
            f.BackColor = Fondo;
            f.Font = new Font("Segoe UI", 9F);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ClientSize = new Size(ancho, alto);

            var cuerpo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24), BackColor = Fondo };
            var encabezado = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White };

            encabezado.Controls.Add(new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI Semibold", 16F),
                ForeColor = Texto,
                BackColor = Color.Transparent,
                Location = new Point(30, 8),
                AutoSize = true
            });
            encabezado.Controls.Add(new Label
            {
                Text = subtitulo,
                Font = new Font("Segoe UI", 9F),
                ForeColor = TextoSuave,
                BackColor = Color.Transparent,
                Location = new Point(32, 42),
                AutoSize = true
            });

            f.Controls.Add(cuerpo);
            f.Controls.Add(encabezado);
            return cuerpo;
        }

        // Tarjeta blanca con bordes redondeados
        public static Guna2Panel Tarjeta()
        {
            return new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Fondo,
                FillColor = Color.White,
                BorderRadius = 14,
                BorderThickness = 1,
                BorderColor = Borde
            };
        }

        // Divide el cuerpo en dos tarjetas: formulario a la izquierda, contenido a la derecha
        public static void DosColumnas(Panel cuerpo, int anchoIzquierda, out Guna2Panel izquierda, out Guna2Panel derecha)
        {
            var tabla = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Fondo
            };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, anchoIzquierda));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            izquierda = Tarjeta();
            izquierda.Margin = new Padding(0, 0, 16, 0);
            derecha = Tarjeta();
            derecha.Margin = new Padding(0);

            tabla.Controls.Add(izquierda, 0, 0);
            tabla.Controls.Add(derecha, 1, 0);
            cuerpo.Controls.Add(tabla);
        }

        public static Label TituloTarjeta(string texto)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI Semibold", 12F),
                ForeColor = Texto,
                BackColor = Color.Transparent,
                Location = new Point(20, 16),
                AutoSize = true
            };
        }

        // Coloca una etiqueta y su campo (la etiqueta arriba, el campo 20 px debajo)
        public static void Campo(Control padre, string etiqueta, Control campo, int x, int y, int ancho)
        {
            var lbl = new Label
            {
                Text = etiqueta,
                ForeColor = TextoSuave,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(x, y),
                AutoSize = true
            };
            campo.Location = new Point(x, y + 20);
            campo.Width = ancho;
            padre.Controls.Add(lbl);
            padre.Controls.Add(campo);
        }

        public static Guna2TextBox Caja(string placeholder = "")
        {
            var caja = new Guna2TextBox
            {
                PlaceholderText = placeholder,
                BorderRadius = 8,
                Height = 36,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            caja.FocusedState.BorderColor = Azul;
            return caja;
        }

        public static Guna2ComboBox Combo()
        {
            return new Guna2ComboBox
            {
                BorderRadius = 8,
                ItemHeight = 30,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
        }

        public static Guna2DateTimePicker Fecha()
        {
            return new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Height = 36,
                Format = DateTimePickerFormat.Short,
                BackColor = Color.White,
                FillColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
        }

        public static Guna2Button Boton(string texto, Color color, int ancho)
        {
            return new Guna2Button
            {
                Text = texto,
                Width = ancho,
                Height = 40,
                BackColor = Color.White,
                FillColor = color,
                ForeColor = Color.White,
                BorderRadius = 10,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9.5F)
            };
        }

        // Tabla con el estilo del dashboard
        public static Guna2DataGridView CrearGrid()
        {
            var dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 36
            };
            dgv.RowTemplate.Height = 32;
            EstilizarGrid(dgv);
            return dgv;
        }

        public static void EstilizarGrid(Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Fondo;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Texto;
            dgv.ThemeStyle.HeaderStyle.BackColor = Fondo;
            dgv.ThemeStyle.HeaderStyle.ForeColor = Texto;
            dgv.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9.5F);
            dgv.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgv.ThemeStyle.RowsStyle.ForeColor = Texto;
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgv.ThemeStyle.RowsStyle.SelectionForeColor = Texto;
            dgv.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgv.ThemeStyle.GridColor = Borde;
        }

        // Quita la flechita de ordenamiento de los encabezados
        public static void DesactivarOrden(Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // ---- Mensajes ----
        public static void Info(string mensaje)
        {
            MessageBox.Show(mensaje, "Farmacia Picado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Aviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Farmacia Picado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, "Farmacia Picado", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
