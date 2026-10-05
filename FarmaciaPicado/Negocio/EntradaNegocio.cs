using System;
using System.Collections.Generic;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Negocio
{
    public class EntradaNegocio
    {
        private readonly EntradaDAO dao = new EntradaDAO();

        public List<Entrada> Obtener()
        {
            return dao.Obtener();
        }

        public List<Medicamento> ObtenerMedicamentos()
        {
            return dao.ObtenerMedicamentos();
        }

        public void Registrar(int idMedicamento, int cantidad, int idUsuario)
        {
            if (idMedicamento == 0)
                throw new Exception("Debe seleccionar un medicamento.");

            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            dao.Registrar(idMedicamento, cantidad, idUsuario);
        }
    }
}