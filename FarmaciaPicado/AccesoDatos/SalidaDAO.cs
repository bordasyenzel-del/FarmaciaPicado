using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.AccesoDatos
{
    public class SalidaDAO
    {
        public List<Salida> Obtener()
        {
            List<Salida> lista = new List<Salida>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerSalidas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Salida
                    {
                        IdSalida = lector.GetInt32(lector.GetOrdinal("IdSalida")),
                        Medicamento = lector.GetString(lector.GetOrdinal("Medicamento")),
                        Cantidad = lector.GetInt32(lector.GetOrdinal("Cantidad")),
                        FechaSalida = lector.GetDateTime(lector.GetOrdinal("FechaSalida")),
                        Usuario = lector.GetString(lector.GetOrdinal("Usuario")),
                    });
                }
            }

            return lista;
        }

        // Lista de medicamentos con su stock actual (para el ComboBox y la validación)
        public List<Medicamento> ObtenerMedicamentos()
        {
            List<Medicamento> lista = new List<Medicamento>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdMedicamento, Nombre, StockActual FROM Medicamentos ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(sql, conexion);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Medicamento
                    {
                        IdMedicamento = lector.GetInt32(lector.GetOrdinal("IdMedicamento")),
                        Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                        StockActual = lector.GetInt32(lector.GetOrdinal("StockActual")),
                    });
                }
            }

            return lista;
        }

        // La validación "fuerte" de stock (con bloqueo UPDLOCK/ROWLOCK) sigue
        // viviendo en el procedimiento almacenado, como red de seguridad final.
        public void Registrar(int idMedicamento, int cantidad, int idUsuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarSalida", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdMedicamento", idMedicamento);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}