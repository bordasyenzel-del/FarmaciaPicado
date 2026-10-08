using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FarmaciaPicado.Orm;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    public partial class FrmCategorias : Form
    {
        private readonly CategoriaServicioEf dao = new CategoriaServicioEf();
        private int idSeleccionado = 0;
        private bool cargando = false;

        private Label lblModo;
        private Guna2TextBox txtNombre;
        private Guna2DataGridView dgv;
        private Guna2Button btnGuardar, btnNuevo, btnEliminar;

        public FrmCategorias()
        {
            InitializeComponent();
            ConstruirUI();
            this.Load += (s, e) =>
            {
                try { CargarLista(); Limpiar(); }
                catch (Exception ex) { EstiloUI.Error("Error al cargar las categorías:\n" + ex.Message); }
            };
        }

        private void ConstruirUI()
        {
            var cuerpo = EstiloUI.ConfigurarForm(this, "Categorías", "Clasificación de los medicamentos", 900, 560);
            EstiloUI.DosColumnas(cuerpo, 340, out Guna2Panel izq, out Guna2Panel der);

            // ---------- Formulario ----------
            lblModo = EstiloUI.TituloTarjeta("Nueva categoría");
            izq.Controls.Add(lblModo);

            txtNombre = EstiloUI.Caja("Nombre de la categoría");
            EstiloUI.Campo(izq, "Nombre", txtNombre, 20, 56, 300);

            btnGuardar = EstiloUI.Boton("Guardar", EstiloUI.Azul, 300);
            btnGuardar.Location = new Point(20, 140);
            btnNuevo = EstiloUI.Boton("Nueva", EstiloUI.TextoSuave, 145);
            btnNuevo.Location = new Point(20, 190);
            btnEliminar = EstiloUI.Boton("Eliminar", EstiloUI.Rojo, 145);
            btnEliminar.Location = new Point(175, 190);

            btnGuardar.Click += BtnGuardar_Click;
            btnNuevo.Click += (s, e) => Limpiar();
            btnEliminar.Click += BtnEliminar_Click;

            izq.Controls.Add(btnGuardar);
            izq.Controls.Add(btnNuevo);
            izq.Controls.Add(btnEliminar);

            // ---------- Listado ----------
            der.Padding = new Padding(20, 56, 20, 20);
            dgv = EstiloUI.CrearGrid();
            dgv.Columns.Add("Id", "N.º");
            dgv.Columns.Add("Nombre", "Categoría");
            dgv.Columns["Id"].FillWeight = 20;
            dgv.Columns["Nombre"].FillWeight = 80;
            EstiloUI.DesactivarOrden(dgv);
            dgv.SelectionChanged += Dgv_SelectionChanged;

            der.Controls.Add(dgv);
            der.Controls.Add(EstiloUI.TituloTarjeta("Categorías registradas"));
        }

        private void CargarLista()
        {
            cargando = true;
            dgv.Rows.Clear();
            foreach (Categoria c in dao.Obtener())
            {
                int i = dgv.Rows.Add(c.IdCategoria, c.NombreCategoria);
                dgv.Rows[i].Tag = c;
            }
            dgv.ClearSelection();
            dgv.CurrentCell = null;
            cargando = false;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || dgv.SelectedRows.Count == 0) return;
            if (!(dgv.SelectedRows[0].Tag is Categoria c)) return;

            idSeleccionado = c.IdCategoria;
            txtNombre.Text = c.NombreCategoria;
            lblModo.Text = "Editar categoría";
            btnEliminar.Enabled = true;
        }

        private void Limpiar()
        {
            idSeleccionado = 0;
            txtNombre.Clear();
            lblModo.Text = "Nueva categoría";
            btnEliminar.Enabled = false;
            dgv.ClearSelection();
            txtNombre.Focus();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (nombre == "") { EstiloUI.Aviso("Ingrese el nombre de la categoría."); return; }

            var c = new Categoria { IdCategoria = idSeleccionado, NombreCategoria = nombre };

            try
            {
                if (idSeleccionado == 0) dao.Insertar(c);
                else dao.Actualizar(c);

                CargarLista();
                Limpiar();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo guardar:\n" + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0) { EstiloUI.Aviso("Seleccione una categoría de la lista."); return; }
            if (!EstiloUI.Confirmar("¿Eliminar la categoría \"" + txtNombre.Text + "\"?")) return;

            try
            {
                dao.Eliminar(idSeleccionado);
                CargarLista();
                Limpiar();
            }
            catch (Exception ex)
            {
                EstiloUI.Error("No se pudo eliminar. Puede que tenga medicamentos asociados.\n\n" + ex.Message);
            }
        }
    }
}
