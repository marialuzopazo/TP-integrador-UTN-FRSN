using System;
using System.Collections.Generic;

namespace TP_GRUPAL
{
    public class Usuario
    {
        private string nombre;
        private string correo;
        private string contraseña;
        private List<Juego> coleccion;
        private SalaJuegos sala;

        public Usuario(string nombre, string correo, string contraseña)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.contraseña = contraseña;
            coleccion = new List<Juego>();
            sala = new SalaJuegos(this); // Composición 1 a 1
        }

        public void Registrarse()
        {
            Console.WriteLine("Usuario registrado: " + nombre);
        }

        public void IniciarSesion()
        {
            Console.WriteLine("Sesión iniciada para: " + correo);
        }

        public void AgregarJuego(Juego juego)
        {
            coleccion.Add(juego);
        }

        public void EliminarJuego(string nombreJuego)
        {
            coleccion.RemoveAll(j => j.Nombre == nombreJuego);
        }

        public void BuscarJuego(string nombreJuego)
        {
            foreach (var juego in coleccion)
            {
                if (juego.Nombre == nombreJuego)
                {
                    Console.WriteLine("Juego encontrado: " + juego.Nombre);
                    return;
                }
            }
            Console.WriteLine("Juego no encontrado.");
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"Nombre: {nombre}\nCorreo: {correo}\nColección: {coleccion.Count} juegos");
        }

        
    }
}



