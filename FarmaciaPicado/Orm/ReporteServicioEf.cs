using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FarmaciaPicado.Orm
{
    // ---------- Resultados de los reportes (DTOs) ----------
    public class StockReporte
    {
        public string Nombre { get; set; } = "";
        public string Presentacion { get; set; } = "";
        public string Categoria { get; set; } = "";
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }

    public class MovimientoReporte
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = "";
        public string Medicamento { get; set; } = "";
        public int Cantidad { get; set; }
        public string Usuario { get; set; } = "";
    }

    public class RotacionReporte
    {
        public string Medicamento { get; set; } = "";
        public string Categoria { get; set; } = "";
        public int Entradas { get; set; }
        public int Salidas { get; set; }
        public int StockActual { get; set; }
    }

    public class InventarioCategoriaReporte
    {
        public string Categoria { get; set; } = "";
        public int Medicamentos { get; set; }
        public int Unidades { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
    }

    // =====================================================================
    //  REPORTES DINÁMICOS con LINQ sobre el DbContext.
    //  Cada método arma la consulta paso a paso según los filtros que
    //  elija el usuario, y EF Core la traduce a un único SELECT en SQL.
    // =====================================================================
    public class ReporteServicioEf
    {
        // Medicamentos con stock igual o menor al mínimo (filtro opcional por categoría)
        public List<StockReporte> StockBajo(int idCategoria = 0)
        {
            using (var db = new FarmaciaContext())
            {
                IQueryable<MedicamentoEf> consulta = db.Medicamentos
                    .AsNoTracking()
                    .Where(m => m.StockActual <= m.StockMinimo);

                if (idCategoria > 0)
                    consulta = consulta.Where(m => m.IdCategoria == idCategoria);

                return consulta
                    .OrderBy(m => m.StockActual)
                    .ThenBy(m => m.Nombre)
                    .Select(m => new StockReporte
                    {
                        Nombre = m.Nombre,
                        Presentacion = m.Presentacion,
                        Categoria = m.Categoria.NombreCategoria,
                        StockActual = m.StockActual,
                        StockMinimo = m.StockMinimo,
                        FechaVencimiento = m.FechaVencimiento
                    })
                    .ToList();
            }
        }

        // Medicamentos vencidos o que vencen dentro de N días
        public List<StockReporte> ProximosVencer(int dias = 30, int idCategoria = 0)
        {
            using (var db = new FarmaciaContext())
            {
                DateTime limite = DateTime.Today.AddDays(dias);

                IQueryable<MedicamentoEf> consulta = db.Medicamentos
                    .AsNoTracking()
                    .Where(m => m.FechaVencimiento <= limite);

                if (idCategoria > 0)
                    consulta = consulta.Where(m => m.IdCategoria == idCategoria);

                return consulta
                    .OrderBy(m => m.FechaVencimiento)
                    .Select(m => new StockReporte
                    {
                        Nombre = m.Nombre,
                        Presentacion = m.Presentacion,
                        Categoria = m.Categoria.NombreCategoria,
                        StockActual = m.StockActual,
                        StockMinimo = m.StockMinimo,
                        FechaVencimiento = m.FechaVencimiento
                    })
                    .ToList();
            }
        }

        // Historial de movimientos (entradas + salidas) filtrado por fechas, tipo y medicamento.
        // tipo: "Todos", "Entrada" o "Salida"
        public List<MovimientoReporte> Movimientos(DateTime desde, DateTime hasta, string tipo = "Todos", string texto = "")
        {
            using (var db = new FarmaciaContext())
            {
                DateTime inicio = desde.Date;
                DateTime fin = hasta.Date.AddDays(1); // el día "hasta" entra completo
                bool hayTexto = !string.IsNullOrWhiteSpace(texto);

                IQueryable<MovimientoReporte> consulta = null;

                if (tipo != "Salida")
                {
                    var entradas = db.Entradas.AsNoTracking()
                        .Where(x => x.FechaEntrada >= inicio && x.FechaEntrada < fin);
                    if (hayTexto)
                        entradas = entradas.Where(x => x.Medicamento.Nombre.Contains(texto));

                    consulta = entradas.Select(x => new MovimientoReporte
                    {
                        Fecha = x.FechaEntrada,
                        Tipo = "Entrada",
                        Medicamento = x.Medicamento.Nombre,
                        Cantidad = x.Cantidad,
                        Usuario = x.Usuario.NombreUsuario
                    });
                }

                if (tipo != "Entrada")
                {
                    var salidas = db.Salidas.AsNoTracking()
                        .Where(x => x.FechaSalida >= inicio && x.FechaSalida < fin);
                    if (hayTexto)
                        salidas = salidas.Where(x => x.Medicamento.Nombre.Contains(texto));

                    var consultaSalidas = salidas.Select(x => new MovimientoReporte
                    {
                        Fecha = x.FechaSalida,
                        Tipo = "Salida",
                        Medicamento = x.Medicamento.Nombre,
                        Cantidad = x.Cantidad,
                        Usuario = x.Usuario.NombreUsuario
                    });

                    consulta = consulta == null ? consultaSalidas : consulta.Concat(consultaSalidas);
                }

                return consulta.OrderByDescending(m => m.Fecha).ToList();
            }
        }

        // Rotación: cuántas unidades entraron y salieron de cada medicamento en un rango de fechas
        public List<RotacionReporte> Rotacion(DateTime desde, DateTime hasta, int idCategoria = 0)
        {
            using (var db = new FarmaciaContext())
            {
                DateTime inicio = desde.Date;
                DateTime fin = hasta.Date.AddDays(1);

                IQueryable<MedicamentoEf> consulta = db.Medicamentos.AsNoTracking();

                if (idCategoria > 0)
                    consulta = consulta.Where(m => m.IdCategoria == idCategoria);

                return consulta
                    .Select(m => new RotacionReporte
                    {
                        Medicamento = m.Nombre,
                        Categoria = m.Categoria.NombreCategoria,
                        Entradas = m.Entradas
                            .Where(e => e.FechaEntrada >= inicio && e.FechaEntrada < fin)
                            .Sum(e => (int?)e.Cantidad) ?? 0,
                        Salidas = m.Salidas
                            .Where(s => s.FechaSalida >= inicio && s.FechaSalida < fin)
                            .Sum(s => (int?)s.Cantidad) ?? 0,
                        StockActual = m.StockActual
                    })
                    .OrderByDescending(r => r.Salidas)
                    .ThenBy(r => r.Medicamento)
                    .ToList();
            }
        }

        // Valor del inventario agrupado por categoría (agregaciones con LINQ)
        public List<InventarioCategoriaReporte> InventarioPorCategoria()
        {
            using (var db = new FarmaciaContext())
            {
                return db.Categorias
                    .AsNoTracking()
                    .Select(c => new InventarioCategoriaReporte
                    {
                        Categoria = c.NombreCategoria,
                        Medicamentos = c.Medicamentos.Count(),
                        Unidades = c.Medicamentos.Sum(m => (int?)m.StockActual) ?? 0,
                        ValorCompra = c.Medicamentos.Sum(m => (decimal?)(m.StockActual * m.PrecioCompra)) ?? 0m,
                        ValorVenta = c.Medicamentos.Sum(m => (decimal?)(m.StockActual * m.PrecioVenta)) ?? 0m
                    })
                    .OrderBy(x => x.Categoria)
                    .ToList();
            }
        }
    }
}
