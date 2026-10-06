using System;

namespace FarmaciaPicado.Entidades
{
    public class HistorialMovimiento
    {
        public string Tipo { get; set; }       // "Entrada" o "Salida"
        public string Medicamento { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
    }
}
