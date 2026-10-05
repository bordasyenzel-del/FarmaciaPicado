using System;
using System.Collections.Generic;
using FarmaciaPicado.AccesoDatos;
using FarmaciaPicado.Entidades;

namespace FarmaciaPicado.Negocio
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDAO dao = new UsuarioDAO();

        public List<Usuario> Obtener()
        {
            return dao.Obtener();
        }

        public List<Rol> ObtenerRoles()
        {
            return dao.ObtenerRoles();
        }

        // Devuelve el usuario si el login es correcto, o null si no lo es.
        // FrmLogin decide qué hacer en cada caso.
        public Usuario ValidarLogin(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("Debe ingresar el usuario.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Debe ingresar la contraseña.");

            return dao.ValidarLogin(nombreUsuario.Trim(), contrasena.Trim());
        }

        // nuevaContraseña llega vacía cuando se edita sin cambiarla
        public void Guardar(Usuario u, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(u.NombreUsuario))
                throw new Exception("Debe ingresar el nombre de usuario.");

            if (u.IdUsuario == 0 && string.IsNullOrWhiteSpace(nuevaContrasena))
                throw new Exception("Debe ingresar una contraseña.");

            if (u.IdRol == 0)
                throw new Exception("Debe seleccionar un rol.");

            if (u.IdUsuario == 0)
            {
                u.Contrasena = nuevaContrasena.Trim();
                dao.Insertar(u);
            }
            else if (string.IsNullOrWhiteSpace(nuevaContrasena))
            {
                dao.ActualizarSinContrasena(u);
            }
            else
            {
                u.Contrasena = nuevaContrasena.Trim();
                dao.ActualizarConContrasena(u);
            }
        }

        public void Eliminar(int idUsuario)
        {
            dao.Eliminar(idUsuario);
        }
    }
}
