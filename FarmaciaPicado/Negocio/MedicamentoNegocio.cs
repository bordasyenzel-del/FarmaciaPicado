using System;
using System.Collections.Generic;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Negocio

    public class MedicamentoNegocio
    {
        private readonly MedicamentoDAO dao = new MedicamentoDAO();

     
        // Obtener medicamentos (no necesita reglas de negocio,
        // simplemente delega al DAO)
  
        public List<Medicamento> Obtener(string filtro = "")
        {
            return dao.Obtener(filtro);
        }

        public List<Categoria> ObtenerCategorias()
        {
            return dao.ObtenerCategorias();
        }

  
        // Guardar (Insertar o Actualizar), validando antes
        
        public void Guardar(Medicamento m)
        {
            Validar(m);

            if (m.IdMedicamento == 0)
                dao.Insertar(m);
            else
                dao.Actualizar(m);
        }

       
        public void Eliminar(int idMedicamento)
        {
            dao.Eliminar(idMedicamento);
        }

      
        // Reglas de negocio / validaciones

        private void Validar(Medicamento m)
        {
            if (string.IsNullOrWhiteSpace(m.Nombre))
                throw new Exception("Debe ingresar el nombre del medicamento.");

            if (string.IsNullOrWhiteSpace(m.Presentacion))
                throw new Exception("Debe ingresar la presentación.");

            if (m.PrecioCompra < 0)
                throw new Exception("El precio de compra no puede ser negativo.");

            if (m.PrecioVenta < 0)
                throw new Exception("El precio de venta no puede ser negativo.");

            if (m.PrecioVenta < m.PrecioCompra)
                throw new Exception("El precio de venta no puede ser menor al precio de compra.");

            if (m.StockActual < 0)
                throw new Exception("El stock actual no puede ser negativo.");

            if (m.StockMinimo < 0)
                throw new Exception("El stock mínimo no puede ser negativo.");

            if (m.IdCategoria == 0)
                throw new Exception("Debe seleccionar una categoría.");
        }
    }
}