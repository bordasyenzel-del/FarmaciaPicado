using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FarmaciaPicado.Datos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.AccesoDatos
{
    public class UsuarioDAO
    {
        public List<Usuario> Obtener()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Usuario
                    {
                        IdUsuario = lector.GetInt32(lector.GetOrdinal("IdUsuario")),
                        NombreUsuario = lector.GetString(lector.GetOrdinal("NombreUsuario")),
                        Rol = lector.GetString(lector.GetOrdinal("Rol")),
                        Activo = lector.GetBoolean(lector.GetOrdinal("Activo")),
                    });
                }
            }

            return lista;
        }

        public List<Rol> ObtenerRoles()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdRol, NombreRol FROM Roles ORDER BY IdRol";
                SqlCommand cmd = new SqlCommand(sql, conexion);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new Rol
                    {
                        IdRol = lector.GetInt32(lector.GetOrdinal("IdRol")),
                        NombreRol = lector.GetString(lector.GetOrdinal("NombreRol")),
                    });
                }
            }

            return lista;
        }

        // Validar login: usado por FrmLogin a través de UsuarioNegocio
        public Usuario ValidarLogin(string nombreUsuario, string contrasena)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarLogin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                conexion.Open();
                SqlDataReader lector = cmd.ExecuteReader();

                if (lector.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = lector.GetInt32(lector.GetOrdinal("IdUsuario")),
                        NombreUsuario = lector.GetString(lector.GetOrdinal("NombreUsuario")),
                        Rol = lector.GetString(lector.GetOrdinal("NombreRol")),
                    };
                }
            }

            return null; // no se encontró: usuario/contraseña incorrectos
        }

        public void Insertar(Usuario u)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
                cmd.Parameters.AddWithValue("@Contrasena", u.Contrasena);
                cmd.Parameters.AddWithValue("@IdRol", u.IdRol);
                cmd.Parameters.AddWithValue("@Activo", u.Activo);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarSinContrasena(Usuario u)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarUsuarioSinContrasena", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", u.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
                cmd.Parameters.AddWithValue("@IdRol", u.IdRol);
                cmd.Parameters.AddWithValue("@Activo", u.Activo);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarConContrasena(Usuario u)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarUsuarioConContrasena", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", u.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
                cmd.Parameters.AddWithValue("@Contrasena", u.Contrasena);
                cmd.Parameters.AddWithValue("@IdRol", u.IdRol);
                cmd.Parameters.AddWithValue("@Activo", u.Activo);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idUsuario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}