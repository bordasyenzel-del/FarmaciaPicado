using System.Collections.Generic;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Negocio
{
    public class ReporteNegocio
    {
        private readonly ReporteDAO dao = new ReporteDAO();

        public List<Medicamento> ObtenerStockBajo()
        {
            return dao.ObtenerStockBajo();
        }

        public List<Medicamento> ObtenerProximosVencer()
        {
            return dao.ObtenerProximosVencer();
        }

        public List<HistorialMovimiento> ObtenerHistorial()
        {
            return dao.ObtenerHistorial();
        }
    }
}