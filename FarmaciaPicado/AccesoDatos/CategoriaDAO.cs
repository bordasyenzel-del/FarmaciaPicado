using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.AccesoDatos
{
    public class CategoriaDAO
    {
        public List<Categoria> Obtener()
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

        public void Insertar(Categoria c)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreCategoria", c.NombreCategoria);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Categoria c)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarCategoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCategoria", c.IdCategoria);
                cmd.Parameters.AddWithValue("@NombreCategoria", c.NombreCategoria);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idCategoria)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}