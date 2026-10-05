using System;

namespace FarmaciaPicado.Entidades
{
    public class Entrada
    {
        public int IdEntrada { get; set; }
        public int IdMedicamento { get; set; }
        public string Medicamento { get; set; }   // nombre del medicamento (para mostrar en el grid)
        public int Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }       // nombre del usuario que registró (para mostrar)
    }
}