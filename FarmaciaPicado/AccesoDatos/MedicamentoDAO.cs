using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.AccesoDatos
{
    public class MedicamentoDAO
    {
        
        // Obtener lista de medicamentos (con filtro opcional)
       
        public List<Medicamento> Obtener(string filtro = "")
        {
            List<Medicamento> lista = new List<Medicamento>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerMedicamentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Filtro", filtro);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    Medicamento m = new Medicamento
                    {
                        IdMedicamento = lector.GetInt32(lector.GetOrdinal("IdMedicamento")),
                        Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                        Presentacion = lector.GetString(lector.GetOrdinal("Presentacion")),
                        Categoria = lector.GetString(lector.GetOrdinal("Categoria")),
                        PrecioCompra = lector.GetDecimal(lector.GetOrdinal("PrecioCompra")),
                        PrecioVenta = lector.GetDecimal(lector.GetOrdinal("PrecioVenta")),
                        StockActual = lector.GetInt32(lector.GetOrdinal("StockActual")),
                        StockMinimo = lector.GetInt32(lector.GetOrdinal("StockMinimo")),
                        FechaVencimiento = lector.GetDateTime(lector.GetOrdinal("FechaVencimiento")),
                    };
                    lista.Add(m);
                }
            }

            return lista;
        }

        
        // Insertar un nuevo medicamento
      
        public void Insertar(Medicamento m)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarMedicamento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                AgregarParametros(cmd, m);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        // Actualizar un medicamento existente
        
        public void Actualizar(Medicamento m)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarMedicamento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdMedicamento", m.IdMedicamento);
                AgregarParametros(cmd, m);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        // Eliminar un medicamento

        public void Eliminar(int idMedicamento)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarMedicamento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdMedicamento", idMedicamento);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        // Obtener el listado de categorías (para llenar el ComboBox)

        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerCategorias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Categoria
                    {
                        IdCategoria = lector.GetInt32(lector.GetOrdinal("IdCategoria")),
                        NombreCategoria = lector.GetString(lector.GetOrdinal("NombreCategoria")),
                    });
                }
            }

            return lista;
        }


        // Helper privado: agrega los parámetros comunes a Insertar/Actualizar
       
        private void AgregarParametros(SqlCommand cmd, Medicamento m)
        {
            cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
            cmd.Parameters.AddWithValue("@Presentacion", m.Presentacion);
            cmd.Parameters.AddWithValue("@PrecioCompra", m.PrecioCompra);
            cmd.Parameters.AddWithValue("@PrecioVenta", m.PrecioVenta);
            cmd.Parameters.AddWithValue("@StockActual", m.StockActual);
            cmd.Parameters.AddWithValue("@StockMinimo", m.StockMinimo);
            cmd.Parameters.AddWithValue("@FechaVencimiento", m.FechaVencimiento);
            cmd.Parameters.AddWithValue("@IdCategoria", m.IdCategoria);
        }
    }
}
