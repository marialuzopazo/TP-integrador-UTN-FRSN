using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_GRUPAL
{


    public class Usuario
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public List<Juego> Coleccion { get; set; }
        public SalaJuegos Sala { get; set; }

        public Usuario(string nombre, string correo, string contraseña)
        {
            Nombre = nombre;
            Correo = correo;
            Contraseña = contraseña;
            Coleccion = new List<Juego>();
            Sala = new SalaJuegos(this); // Composición 1 a 1
        }

        public void Registrarse()
        {
            Console.WriteLine("Usuario registrado: " + Nombre);
        }

        public void IniciarSesion()
        {
            Console.WriteLine("Sesión iniciada para: " + Correo);
        }

        public void AgregarJuego(Juego juego)
        {
            Coleccion.Add(juego);
        }

        public void EliminarJuego(string nombre)
        {
            Coleccion.RemoveAll(j => j.Nombre == nombre);
        }

        public void BuscarJuego(string nombre)
        {
            foreach (var juego in Coleccion)
            {
                if (juego.Nombre == nombre)
                {
                    Console.WriteLine("Juego encontrado: " + juego.Nombre);
                    return;
                }
            }
            Console.WriteLine("Juego no encontrado.");
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Correo}\nColección: {Coleccion.Count} juegos");
        }
    }

}


