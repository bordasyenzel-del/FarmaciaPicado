using System;
using System.Collections.Generic;

namespace FarmaciaPicado.Orm
{
    // Entidades de Entity Framework Core. Se mapean a las MISMAS tablas que ya
    // usa el ADO.NET existente (no se crea ni se modifica ninguna tabla).
    // Llevan el sufijo "Ef" para no confundirse con las clases de FarmaciaPicado.Entidades.

    public class CategoriaEf
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = "";

        public ICollection<MedicamentoEf> Medicamentos { get; set; } = new List<MedicamentoEf>();
    }

    public class MedicamentoEf
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; } = "";
        public string Presentacion { get; set; } = "";
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdCategoria { get; set; }

        public CategoriaEf Categoria { get; set; }
        public ICollection<EntradaEf> Entradas { get; set; } = new List<EntradaEf>();
        public ICollection<SalidaEf> Salidas { get; set; } = new List<SalidaEf>();
    }

    public class EntradaEf
    {
        public int IdEntrada { get; set; }
        public int IdMedicamento { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int IdUsuario { get; set; }

        public MedicamentoEf Medicamento { get; set; }
        public UsuarioEf Usuario { get; set; }
    }

    public class SalidaEf
    {
        public int IdSalida { get; set; }
        public int IdMedicamento { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdUsuario { get; set; }

        public MedicamentoEf Medicamento { get; set; }
        public UsuarioEf Usuario { get; set; }
    }

    // Solo lectura (para reportes). La contraseña NO se mapea a propósito:
    // el alta/edición de usuarios sigue por ADO.NET + procedimientos almacenados.
    public class UsuarioEf
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = "";
        public int IdRol { get; set; }
        public bool Activo { get; set; }

        public RolEf Rol { get; set; }
    }

    public class RolEf
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = "";
    }
}
