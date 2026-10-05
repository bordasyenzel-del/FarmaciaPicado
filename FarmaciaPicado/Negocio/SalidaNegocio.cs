using System;
using System.Collections.Generic;
using System.Linq;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Negocio
{
    public class SalidaNegocio
    {
        private readonly SalidaDAO dao = new SalidaDAO();

        public List<Salida> Obtener()
        {
            return dao.Obtener();
        }

        public List<Medicamento> ObtenerMedicamentos()
        {
            return dao.ObtenerMedicamentos();
        }

        public void Registrar(int idMedicamento, int cantidad, int idUsuario, List<Medicamento> medicamentosCargados)
        {
            if (idMedicamento == 0)
                throw new Exception("Debe seleccionar un medicamento.");

            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            
            Medicamento m = medicamentosCargados.FirstOrDefault(x => x.IdMedicamento == idMedicamento);
            if (m != null && cantidad > m.StockActual)
            {
                throw new Exception(
                    $"No hay suficiente stock disponible.\nStock actual: {m.StockActual}\nCantidad solicitada: {cantidad}");
            }

            dao.Registrar(idMedicamento, cantidad, idUsuario);
        }
    }
}