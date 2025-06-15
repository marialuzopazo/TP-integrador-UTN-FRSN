using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal class Usuario
    {
        private string nombre;
        private string correo;
        private string contraseña;
        private List<Juego> coleccion;
        private SalaJuego sala;
        private Dictionary<string, HistorialDeEstados> historiales;

        public Usuario(string nombre, string correo, string contraseña)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.contraseña = contraseña;
            coleccion = new List<Juego>();
            sala = new SalaJuego(this);
            historiales = new Dictionary<string, HistorialDeEstados>();
        }

        public string Nombre => nombre;

        public List<Juego> Coleccion => coleccion;

        public bool VerificarContrasena(string input)
        {
            return contraseña == input;
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
            if (!historiales.ContainsKey(juego.Nombre))
            {
                historiales[juego.Nombre] = new HistorialDeEstados(juego.Nombre);
            }
        }

        public void EliminarJuego(string nombreJuego)
        {
            coleccion.RemoveAll(j => j.Nombre == nombreJuego);
            if (historiales.ContainsKey(nombreJuego))
            {
                historiales.Remove(nombreJuego);
            }
        }

        public void EliminarJuego(int index)
        {
            if (index >= 0 && index < coleccion.Count)
            {
                string nombreJuego = coleccion[index].Nombre;
                coleccion.RemoveAt(index);
                if (historiales.ContainsKey(nombreJuego))
                {
                    historiales.Remove(nombreJuego);
                }
            }
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

        public void MostrarColeccion()
        {
            if (coleccion.Count == 0)
            {
                Console.WriteLine("Colección vacía.");
            }
            else
            {
                for (int i = 0; i < coleccion.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {coleccion[i].Nombre} - Estado: {coleccion[i].Estadoactual}");
                }
            }
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"Nombre: {nombre}\nCorreo: {correo}\nColección: {coleccion.Count} juegos");
        }

        public void CambiarEstadoJuego(string nombreJuego, EstadoJuego nuevoEstado, string comentario = "")
        {
            var juego = coleccion.FirstOrDefault(j => j.Nombre == nombreJuego);
            if (juego != null)
            {
                juego.Estadoactual = nuevoEstado;

                if (historiales.TryGetValue(nombreJuego, out var historial))
                {
                    switch (nuevoEstado)
                    {
                        case EstadoJuego.Jugando:
                            historial.Iniciar(comentario);
                            break;
                        case EstadoJuego.Pausado:
                            historial.Pausar(comentario);
                            break;
                        case EstadoJuego.Finalizado:
                            historial.Finalizar(comentario);
                            break;
                        case EstadoJuego.NoIniciado:
                            // No hay acción específica para NoIniciado en HistorialDeEstados
                            break;
                    }
                }
            }
        }

        public HistorialDeEstados ObtenerHistorialJuego(string nombreJuego)
        {
            historiales.TryGetValue(nombreJuego, out var historial);
            return historial;
        }

        public void MostrarEstadosJuegos()
        {
            if (coleccion.Count == 0)
            {
                Console.WriteLine("No tienes juegos en tu colección.");
                return;
            }

            Console.WriteLine("\nEstado de tus juegos:");
            foreach (var juego in coleccion)
            {
                Console.WriteLine($"{juego.Nombre}: {juego.Estadoactual}");
            }
        }
    }
}

