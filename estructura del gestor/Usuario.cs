using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_GRUPAL
{

      public class Usuario : IAccionesUsuario
      {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public List<Juego> Coleccion { get; set; }

        public Usuario(string nombre, string correo, string contrasena)
        {
            Nombre = nombre;
            Correo = correo;
            Contrasena = contrasena;
            Coleccion = new List<Juego>();
        }

        public void Registrarse()
        {
            Console.WriteLine("Usuario registrado correctamente.");
        }

        public void IniciarSesion()
        {
            Console.WriteLine("Inicio de sesión exitoso.");
        }

        public void AgregarJuego(Juego juego)
        {
            Coleccion.Add(juego);
        }

        public void EliminarJuego(string nombreJuego)
        {
            Coleccion.RemoveAll(j => j.Nombre == nombreJuego);
        }

        public Juego BuscarJuego(string nombreJuego)
        {
            return Coleccion.Find(j => j.Nombre == nombreJuego);
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"Nombre: {Nombre}, Correo: {Correo}");
        }

        public void GuardarColeccion()
        {
            Console.WriteLine("Colección de juegos guardada.");
        }

        public void Compartir()
        {
            Console.WriteLine("Colección compartida con otros usuarios.");
        }
      }

}

