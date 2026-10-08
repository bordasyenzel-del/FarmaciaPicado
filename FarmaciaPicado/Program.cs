using FarmaciaPicado.Entidades;

namespace FarmaciaPicado
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Flujo: login -> menú -> (si cierra sesión) login otra vez.
            // Si el usuario cierra la ventana del login o la del menú con la X,
            // la aplicación termina por completo.
            while (true)
            {
                Usuario u;
                using (var login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK) return;
                    u = login.UsuarioAutenticado;
                }

                var menu = new FrmMenuPrincipal(u.IdUsuario, u.NombreUsuario, u.Rol);
                Application.Run(menu);

                if (!menu.CerroSesion) return;
            }
        }
    }
}
