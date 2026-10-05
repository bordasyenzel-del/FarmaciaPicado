using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FarmaciaPicado.Datos
{
    public class ConexionBD
    {
        private static string ObtenerCadenaConexion()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config.GetConnectionString("FarmaciaPicadoDB");
        }

        public static SqlConnection ObtenerConexion()
        {
            string cadena = ObtenerCadenaConexion();
            return new SqlConnection(cadena);
        }
    }
}
