using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.AccesoDatos
{
    public class ReporteDAO
    {
        public List<Medicamento> ObtenerStockBajo()
        {
            List<Medicamento> lista = new List<Medicamento>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = @"SELECT Nombre, Presentacion, StockActual, StockMinimo, FechaVencimiento
                               FROM Medicamentos
                               WHERE StockActual <= StockMinimo
                               ORDER BY StockActual ASC";
                SqlCommand cmd = new SqlCommand(sql, conexion);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Medicamento
                    {
                        Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                        Presentacion = lector.GetString(lector.GetOrdinal("Presentacion")),
                        StockActual = lector.GetInt32(lector.GetOrdinal("StockActual")),
                        StockMinimo = lector.GetInt32(lector.GetOrdinal("StockMinimo")),
                        FechaVencimiento = lector.GetDateTime(lector.GetOrdinal("FechaVencimiento")),
                    });
                }
            }

            return lista;
        }

        public List<Medicamento> ObtenerProximosVencer()
        {
            List<Medicamento> lista = new List<Medicamento>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = @"SELECT Nombre, Presentacion, StockActual, FechaVencimiento
                               FROM Medicamentos
                               WHERE FechaVencimiento <= DATEADD(DAY, 30, GETDATE())
                               ORDER BY FechaVencimiento ASC";
                SqlCommand cmd = new SqlCommand(sql, conexion);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Medicamento
                    {
                        Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                        Presentacion = lector.GetString(lector.GetOrdinal("Presentacion")),
                        StockActual = lector.GetInt32(lector.GetOrdinal("StockActual")),
                        FechaVencimiento = lector.GetDateTime(lector.GetOrdinal("FechaVencimiento")),
                    });
                }
            }

            return lista;
        }

        public List<HistorialMovimiento> ObtenerHistorial()
        {
            List<HistorialMovimiento> lista = new List<HistorialMovimiento>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = @"
                    SELECT 'Entrada' AS Tipo, M.Nombre AS Medicamento, E.Cantidad, 
                           E.FechaEntrada AS Fecha, U.NombreUsuario AS Usuario
                    FROM Entradas E
                    INNER JOIN Medicamentos M ON E.IdMedicamento = M.IdMedicamento
                    INNER JOIN Usuarios U ON E.IdUsuario = U.IdUsuario
 
                    UNION ALL
 
                    SELECT 'Salida' AS Tipo, M.Nombre AS Medicamento, S.Cantidad, 
                           S.FechaSalida AS Fecha, U.NombreUsuario AS Usuario
                    FROM Salidas S
                    INNER JOIN Medicamentos M ON S.IdMedicamento = M.IdMedicamento
                    INNER JOIN Usuarios U ON S.IdUsuario = U.IdUsuario
 
                    ORDER BY Fecha DESC";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new HistorialMovimiento
                    {
                        Tipo = lector.GetString(lector.GetOrdinal("Tipo")),
                        Medicamento = lector.GetString(lector.GetOrdinal("Medicamento")),
                        Cantidad = lector.GetInt32(lector.GetOrdinal("Cantidad")),
                        Fecha = lector.GetDateTime(lector.GetOrdinal("Fecha")),
                        Usuario = lector.GetString(lector.GetOrdinal("Usuario")),
                    });
                }
            }

            return lista;
        }
    }
}