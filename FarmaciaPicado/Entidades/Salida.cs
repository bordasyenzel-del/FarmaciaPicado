using System;

namespace FarmaciaPicado.Entidades
{
    public class Salida
    {
        public int IdSalida { get; set; }
        public int IdMedicamento { get; set; }
        public string Medicamento { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
    }
}