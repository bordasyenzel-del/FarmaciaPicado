using System;
using System.Collections.Generic;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Negocio
{
    public class CategoriaNegocio
    {
        private readonly CategoriaDAO dao = new CategoriaDAO();

        public List<Categoria> Obtener()
        {
            return dao.Obtener();
        }

        public void Guardar(Categoria c)
        {
            if (string.IsNullOrWhiteSpace(c.NombreCategoria))
                throw new Exception("Debe ingresar el nombre de la categoría.");

            if (c.IdCategoria == 0)
                dao.Insertar(c);
            else
                dao.Actualizar(c);
        }

        public void Eliminar(int idCategoria)
        {
            dao.Eliminar(idCategoria);
        }
    }
}
