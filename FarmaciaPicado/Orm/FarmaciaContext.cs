using Microsoft.EntityFrameworkCore;
using FarmaciaPicado.Datos;

namespace FarmaciaPicado.Orm
{
    // DbContext: punto de entrada de Entity Framework Core.
    // Reutiliza la MISMA cadena de conexión que ya lee ConexionBD (appsettings.json),
    // así no hay que configurar nada nuevo.
    public class FarmaciaContext : DbContext
    {
        private static string cadenaConexion;

        public DbSet<CategoriaEf> Categorias { get; set; }
        public DbSet<MedicamentoEf> Medicamentos { get; set; }
        public DbSet<EntradaEf> Entradas { get; set; }
        public DbSet<SalidaEf> Salidas { get; set; }
        public DbSet<UsuarioEf> Usuarios { get; set; }
        public DbSet<RolEf> Roles { get; set; }

        private static string ObtenerCadena()
        {
            if (cadenaConexion == null)
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    cadenaConexion = conexion.ConnectionString;
                }
            }
            return cadenaConexion;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opciones)
        {
            if (!opciones.IsConfigured)
                opciones.UseSqlServer(ObtenerCadena());
        }

        // Mapeo (Fluent API) de las clases a las tablas y columnas existentes
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<CategoriaEf>(e =>
            {
                e.ToTable("Categorias");
                e.HasKey(x => x.IdCategoria);
            });

            mb.Entity<MedicamentoEf>(e =>
            {
                e.ToTable("Medicamentos");
                e.HasKey(x => x.IdMedicamento);
                e.Property(x => x.PrecioCompra).HasColumnType("decimal(18,2)");
                e.Property(x => x.PrecioVenta).HasColumnType("decimal(18,2)");
                e.HasOne(x => x.Categoria)
                 .WithMany(c => c.Medicamentos)
                 .HasForeignKey(x => x.IdCategoria);
            });

            mb.Entity<EntradaEf>(e =>
            {
                e.ToTable("Entradas");
                e.HasKey(x => x.IdEntrada);
                e.HasOne(x => x.Medicamento)
                 .WithMany(m => m.Entradas)
                 .HasForeignKey(x => x.IdMedicamento);
                e.HasOne(x => x.Usuario)
                 .WithMany()
                 .HasForeignKey(x => x.IdUsuario);
            });

            mb.Entity<SalidaEf>(e =>
            {
                e.ToTable("Salidas");
                e.HasKey(x => x.IdSalida);
                e.HasOne(x => x.Medicamento)
                 .WithMany(m => m.Salidas)
                 .HasForeignKey(x => x.IdMedicamento);
                e.HasOne(x => x.Usuario)
                 .WithMany()
                 .HasForeignKey(x => x.IdUsuario);
            });

            mb.Entity<UsuarioEf>(e =>
            {
                e.ToTable("Usuarios");
                e.HasKey(x => x.IdUsuario);
                e.HasOne(x => x.Rol)
                 .WithMany()
                 .HasForeignKey(x => x.IdRol);
            });

            mb.Entity<RolEf>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(x => x.IdRol);
            });
        }
    }
}
