using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Orm
{
    // Convierte los errores genéricos de EF en el mensaje real de SQL Server
    // (por ejemplo, "The DELETE statement conflicted with the REFERENCE constraint...").
    internal static class ErroresEf
    {
        public static void Guardar(FarmaciaContext db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                string mensaje = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception(mensaje, ex);
            }
        }
    }

    // =====================================================================
    //  CRUD de MEDICAMENTOS con EF Core + LINQ
    //  Mantiene los mismos nombres de métodos que MedicamentoDAO para que
    //  el formulario solo cambie la línea donde crea el objeto.
    // =====================================================================
    public class MedicamentoServicioEf
    {
        // READ: consulta LINQ con filtro opcional y proyección a la entidad de la app
        public List<Medicamento> Obtener(string filtro = "")
        {
            using (var db = new FarmaciaContext())
            {
                IQueryable<MedicamentoEf> consulta = db.Medicamentos.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(filtro))
                    consulta = consulta.Where(m => m.Nombre.Contains(filtro));

                return consulta
                    .OrderBy(m => m.Nombre)
                    .Select(m => new Medicamento
                    {
                        IdMedicamento = m.IdMedicamento,
                        Nombre = m.Nombre,
                        Presentacion = m.Presentacion,
                        Categoria = m.Categoria.NombreCategoria,
                        IdCategoria = m.IdCategoria,
                        PrecioCompra = m.PrecioCompra,
                        PrecioVenta = m.PrecioVenta,
                        StockActual = m.StockActual,
                        StockMinimo = m.StockMinimo,
                        FechaVencimiento = m.FechaVencimiento
                    })
                    .ToList();
            }
        }

        // CREATE
        public void Insertar(Medicamento m)
        {
            using (var db = new FarmaciaContext())
            {
                var nuevo = new MedicamentoEf();
                Copiar(m, nuevo);
                db.Medicamentos.Add(nuevo);
                ErroresEf.Guardar(db);
            }
        }

        // UPDATE: EF detecta qué campos cambiaron y genera solo ese UPDATE
        public void Actualizar(Medicamento m)
        {
            using (var db = new FarmaciaContext())
            {
                var existente = db.Medicamentos.Find(m.IdMedicamento);
                if (existente == null)
                    throw new InvalidOperationException("El medicamento ya no existe.");

                Copiar(m, existente);
                ErroresEf.Guardar(db);
            }
        }

        // DELETE
        public void Eliminar(int idMedicamento)
        {
            using (var db = new FarmaciaContext())
            {
                var existente = db.Medicamentos.Find(idMedicamento);
                if (existente == null) return;

                db.Medicamentos.Remove(existente);
                ErroresEf.Guardar(db);
            }
        }

        // Para llenar el ComboBox de categorías
        public List<Categoria> ObtenerCategorias()
        {
            return new CategoriaServicioEf().Obtener();
        }

        private static void Copiar(Medicamento origen, MedicamentoEf destino)
        {
            destino.Nombre = origen.Nombre;
            destino.Presentacion = origen.Presentacion;
            destino.PrecioCompra = origen.PrecioCompra;
            destino.PrecioVenta = origen.PrecioVenta;
            destino.StockActual = origen.StockActual;
            destino.StockMinimo = origen.StockMinimo;
            destino.FechaVencimiento = origen.FechaVencimiento;
            destino.IdCategoria = origen.IdCategoria;
        }
    }

    // =====================================================================
    //  CRUD de CATEGORÍAS con EF Core
    // =====================================================================
    public class CategoriaServicioEf
    {
        public List<Categoria> Obtener()
        {
            using (var db = new FarmaciaContext())
            {
                return db.Categorias
                    .AsNoTracking()
                    .OrderBy(c => c.NombreCategoria)
                    .Select(c => new Categoria
                    {
                        IdCategoria = c.IdCategoria,
                        NombreCategoria = c.NombreCategoria
                    })
                    .ToList();
            }
        }

        public void Insertar(Categoria c)
        {
            using (var db = new FarmaciaContext())
            {
                db.Categorias.Add(new CategoriaEf { NombreCategoria = c.NombreCategoria });
                ErroresEf.Guardar(db);
            }
        }

        public void Actualizar(Categoria c)
        {
            using (var db = new FarmaciaContext())
            {
                var existente = db.Categorias.Find(c.IdCategoria);
                if (existente == null)
                    throw new InvalidOperationException("La categoría ya no existe.");

                existente.NombreCategoria = c.NombreCategoria;
                ErroresEf.Guardar(db);
            }
        }

        public void Eliminar(int idCategoria)
        {
            using (var db = new FarmaciaContext())
            {
                var existente = db.Categorias.Find(idCategoria);
                if (existente == null) return;

                db.Categorias.Remove(existente);
                ErroresEf.Guardar(db);
            }
        }
    }
}
