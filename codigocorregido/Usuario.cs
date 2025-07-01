using System;
using System.Collections.Generic;
using System.Linq;

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
            //cambio en contraseña
            if (ContieneCaracteresEspeciales(contraseña))
            {
                throw new ArgumentException("La contraseña no debe contener caracteres especiales.");
            }

            this.nombre = nombre;
            this.correo = correo;
            this.contraseña = contraseña;
            coleccion = new List<Juego>();
            sala = new SalaJuego(this);
            historiales = new Dictionary<string, HistorialDeEstados>();
        }

        public string Nombre => nombre;
        public List<Juego> Coleccion => coleccion;

        private bool ContieneCaracteresEspeciales(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsLetterOrDigit(c))
                    return true;
            }
            return false;
        }

        public bool VerificarContrasena(string input)
        {
            return contraseña == input;
        }

        public void Registrarse()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Usuario registrado: " + nombre);
            Console.ResetColor();
        }

        public void IniciarSesion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sesión iniciada para: " + correo);
            Console.ResetColor();
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
            historiales.Remove(nombreJuego);
        }

        public void EliminarJuego(int index)
        {
            if (index >= 0 && index < coleccion.Count)
            {
                string nombreJuego = coleccion[index].Nombre;
                coleccion.RemoveAt(index);
                historiales.Remove(nombreJuego);
            }
        }

        public void BuscarJuego(string nombreJuego)
        {
            var juego = coleccion.FirstOrDefault(j => j.Nombre == nombreJuego);
            if (juego != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Juego encontrado: " + juego.Nombre);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Juego no encontrado.");
            }
            Console.ResetColor();
        }

        public void MostrarColeccion()
        {
            Console.Clear();
            if (coleccion.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════╗");
                Console.WriteLine("║   Tu colección está vacía   ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       TU COLECCIÓN DE JUEGOS ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            for (int i = 0; i < coleccion.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nJuego {i + 1}:");
                Console.ResetColor();
                coleccion[i].mostrarDetalles();
            }
        }

        public void MostrarDatos()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║        DATOS DEL USUARIO     ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Nombre: {nombre}");
            Console.WriteLine($"Correo: {correo}");
            Console.WriteLine($"Juegos en colección: {coleccion.Count}");
            Console.ResetColor();
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
            Console.Clear();
            if (coleccion.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No tienes juegos en tu colección.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║     ESTADO DE TUS JUEGOS     ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            foreach (var juego in coleccion)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{juego.Nombre}: {juego.Estadoactual}");
            }
            Console.ResetColor();
        }

        // Métodos agregado para  usar la sala de juego

        public void IniciarSala()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Iniciando sala de juego...");
            Console.ResetColor();
            sala.Iniciar();
        }

        public void PausarSala()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Pausando sala de juego...");
            Console.ResetColor();
            sala.Pausar();
        }

        public void FinalizarSala()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Finalizando sala de juego...");
            Console.ResetColor();
            sala.Finalizar();
        }
        //se agrego la sala
        public void MostrarInfoSala()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║     INFORMACIÓN DE SALA      ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();
            sala.MostrarInfoSala();
        }

        public void AgregarJuegoASala(string nombreJuego)
        {
            var juego = coleccion.FirstOrDefault(j => j.Nombre == nombreJuego);
            if (juego != null)
            {
                sala.AgregarJuego(juego);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Juego '{nombreJuego}' agregado a la sala.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El juego no está en tu colección.");
            }
            Console.ResetColor();
        }
    }
}
