using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.AccesoDatos
{
    public class EntradaDAO
    {
        public List<Entrada> Obtener()
        {
            List<Entrada> lista = new List<Entrada>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerEntradas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Entrada
                    {
                        IdEntrada = lector.GetInt32(lector.GetOrdinal("IdEntrada")),
                        Medicamento = lector.GetString(lector.GetOrdinal("Medicamento")),
                        Cantidad = lector.GetInt32(lector.GetOrdinal("Cantidad")),
                        FechaEntrada = lector.GetDateTime(lector.GetOrdinal("FechaEntrada")),
                        Usuario = lector.GetString(lector.GetOrdinal("Usuario")),
                    });
                }
            }

            return lista;
        }

        // Lista simple de medicamentos para llenar el ComboBox
        public List<Medicamento> ObtenerMedicamentos()
        {
            List<Medicamento> lista = new List<Medicamento>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdMedicamento, Nombre FROM Medicamentos ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(sql, conexion);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Medicamento
                    {
                        IdMedicamento = lector.GetInt32(lector.GetOrdinal("IdMedicamento")),
                        Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    });
                }
            }

            return lista;
        }

        // La transacción y la lógica siguen viviendo en el procedimiento almacenado;
        // el DAO solo llama y pasa los parámetros.
        public void Registrar(int idMedicamento, int cantidad, int idUsuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarEntrada", conexion);
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