using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

    public class MovimientoDiaReporte
    {
        public DateTime Dia { get; set; }
        public int Entradas { get; set; }
        public int Salidas { get; set; }
    }

    // =====================================================================
    //  REPORTES DINÁMICOS con LINQ sobre el DbContext.
    //  Los métodos terminados en "Async" aceptan un CancellationToken: si el
    //  usuario pulsa «Cancelar» en la ventana de carga, EF Core cancela la
    //  consulta en SQL Server.
    // =====================================================================
    public class ReporteServicioEf
    {
        // Medicamentos con stock igual o menor al mínimo (filtro opcional por categoría)
        public async Task<List<StockReporte>> StockBajoAsync(int idCategoria, CancellationToken ct)
        {
            using (var db = new FarmaciaContext())
            {
                IQueryable<MedicamentoEf> consulta = db.Medicamentos
                    .AsNoTracking()
                    .Where(m => m.StockActual <= m.StockMinimo);

                if (idCategoria > 0)
                    consulta = consulta.Where(m => m.IdCategoria == idCategoria);

                return await consulta
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
                    .ToListAsync(ct);
            }
        }

        // Medicamentos vencidos o que vencen dentro de N días
        public async Task<List<StockReporte>> ProximosVencerAsync(int dias, int idCategoria, CancellationToken ct)
        {
            using (var db = new FarmaciaContext())
            {
                DateTime limite = DateTime.Today.AddDays(dias);

                IQueryable<MedicamentoEf> consulta = db.Medicamentos
                    .AsNoTracking()
                    .Where(m => m.FechaVencimiento <= limite);

                if (idCategoria > 0)
                    consulta = consulta.Where(m => m.IdCategoria == idCategoria);

                return await consulta
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
                    .ToListAsync(ct);
            }
        }

        // Historial de movimientos (entradas + salidas) filtrado por fechas, tipo y medicamento.
        // tipo: "Todos", "Entrada" o "Salida"
        public async Task<List<MovimientoReporte>> MovimientosAsync(DateTime desde, DateTime hasta, string tipo, string texto, CancellationToken ct)
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

                return await consulta.OrderByDescending(m => m.Fecha).ToListAsync(ct);
            }
        }

        // Rotación: unidades que entraron y salieron de cada medicamento en un rango de fechas
        public async Task<List<RotacionReporte>> RotacionAsync(DateTime desde, DateTime hasta, int idCategoria, CancellationToken ct)
        {
            using (var db = new FarmaciaContext())
            {
                DateTime inicio = desde.Date;
                DateTime fin = hasta.Date.AddDays(1);

                IQueryable<MedicamentoEf> consulta = db.Medicamentos.AsNoTracking();

                if (idCategoria > 0)
                    consulta = consulta.Where(m => m.IdCategoria == idCategoria);

                return await consulta
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
                    .ToListAsync(ct);
            }
        }

        // Valor del inventario agrupado por categoría (agregaciones con LINQ)
        public async Task<List<InventarioCategoriaReporte>> InventarioPorCategoriaAsync(CancellationToken ct)
        {
            using (var db = new FarmaciaContext())
            {
                return await db.Categorias
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
                    .ToListAsync(ct);
            }
        }

        // Para la gráfica del menú principal: entradas y salidas de los últimos N días
        // (incluye los días sin movimientos, con valor 0).
        public List<MovimientoDiaReporte> MovimientosPorDia(int dias)
        {
            using (var db = new FarmaciaContext())
            {
                DateTime inicio = DateTime.Today.AddDays(-(dias - 1));
                DateTime fin = DateTime.Today.AddDays(1);

                var entradas = db.Entradas.AsNoTracking()
                    .Where(e => e.FechaEntrada >= inicio && e.FechaEntrada < fin)
                    .GroupBy(e => e.FechaEntrada.Date)
                    .Select(g => new { Dia = g.Key, Total = g.Sum(x => x.Cantidad) })
                    .ToList();

                var salidas = db.Salidas.AsNoTracking()
                    .Where(s => s.FechaSalida >= inicio && s.FechaSalida < fin)
                    .GroupBy(s => s.FechaSalida.Date)
                    .Select(g => new { Dia = g.Key, Total = g.Sum(x => x.Cantidad) })
                    .ToList();

                var resultado = new List<MovimientoDiaReporte>();
                for (int i = 0; i < dias; i++)
                {
                    DateTime dia = inicio.AddDays(i);
                    resultado.Add(new MovimientoDiaReporte
                    {
                        Dia = dia,
                        Entradas = entradas.Where(x => x.Dia == dia).Sum(x => x.Total),
                        Salidas = salidas.Where(x => x.Dia == dia).Sum(x => x.Total)
                    });
                }
                return resultado;
            }
        }
    }
}