using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_De_Gestion
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public string Rol { get; set; }

        public Usuario(int id, string nombre, string contrasenia, string rol)
        {
            Id = id;
            Nombre = nombre;
            Contrasenia = contrasenia;
            Rol = rol;
        }

         
        public Usuario(string nombre, string contrasenia, string rol)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
            Rol = rol;
        }
    }
}
