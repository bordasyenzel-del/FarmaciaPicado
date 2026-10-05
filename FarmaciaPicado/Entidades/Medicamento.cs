using System;

namespace FarmaciaPicado.Entidades
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public string Categoria { get; set; }      // nombre de la categoría (para mostrar en el grid)
        public int IdCategoria { get; set; }        // id de la categoría (para guardar/actualizar)
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
