namespace FarmaciaPicado.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }   // solo se usa al insertar/actualizar, nunca se muestra
        public string Rol { get; set; }          // nombre del rol (para mostrar)
        public int IdRol { get; set; }           // id del rol (para guardar/actualizar)
        public bool Activo { get; set; }
    }
}
