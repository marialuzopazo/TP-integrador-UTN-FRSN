using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal class SalaJuego : IChequeoEstado
    {
        private Usuario propietario;
        private DateTime fechaCreacion;
        private List<Juego> juegosActivos;

        public SalaJuego(Usuario propietario)
        {
            this.propietario = propietario;
            this.fechaCreacion = DateTime.Now;
            this.juegosActivos = new List<Juego>();
        }

        public void AgregarJuego(Juego juego)
        {
            juegosActivos.Add(juego);
        }

        public void EliminarJuego(string nombre)
        {
            juegosActivos.RemoveAll(j => j.Nombre == nombre);
        }

        public void BuscarJuego(string nombre)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║     BÚSQUEDA EN LA SALA      ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            var juego = juegosActivos.FirstOrDefault(j => j.Nombre == nombre);
            if (juego != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Juego encontrado en sala: " + juego.Nombre);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Juego no encontrado en sala.");
            }
            Console.ResetColor();
        }

        public void CambiarEstado(Juego juego, EstadoJuego estado)
        {
            if (juegosActivos.Contains(juego))
            {
                juego.Estadoactual = estado;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Estado del juego '{juego.Nombre}' cambiado a {estado}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El juego no se encuentra en la sala.");
            }
            Console.ResetColor();
        }

        public void Iniciar()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se han iniciado los juegos en la sala.");
            Console.ResetColor();
        }

        public void Pausar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Los juegos en la sala están pausados.");
            Console.ResetColor();
        }

        public void Finalizar()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■ Se ha finalizado la sesión de juegos.");
            Console.ResetColor();
        }

        //metodo creado de sala

        public void MostrarInfoSala()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║     INFORMACIÓN DE LA SALA   ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Sala creada por: {propietario.Nombre}");
            Console.WriteLine($"Fecha de creación: {fechaCreacion}");
            Console.WriteLine($"Cantidad de juegos activos: {juegosActivos.Count}");
            Console.ResetColor();
        }
    }
}

