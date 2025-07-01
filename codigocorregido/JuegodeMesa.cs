using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal class JuegodeMesa : Juego
    {

        private int cantidadJugadores;
        private int duracionMinutos;

        public JuegodeMesa(
            string nombre,
            int año,
            string creador,
            string genero,
            string descripcion,
            EstadoJuego estadoactual,
            int cantidadJugadores,
            int duracionMinutos
        ) : base(nombre, año, creador, genero, descripcion, estadoactual)
        {
            this.cantidadJugadores = cantidadJugadores;
            this.duracionMinutos = duracionMinutos;
        }

        public int CantidadJugadores
        {
            get { return cantidadJugadores; }
            set { cantidadJugadores = value; }
        }

        public int DuracionMinutos
        {
            get { return duracionMinutos; }
            set { duracionMinutos = value; }
        }

        public override void mostrarDetalles()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║        JUEGO DE MESA         ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Año: {Año}");
            Console.WriteLine($"Creador: {Creador}");
            Console.WriteLine($"Género: {Genero}");
            Console.WriteLine($"Descripción:");
            Console.WriteLine(Descripcion.Replace(". ", ".\n"));
            Console.WriteLine($"Estado actual: {Estadoactual}");
            Console.WriteLine($"Cantidad de jugadores: {CantidadJugadores}");
            Console.WriteLine($"Duración aproximada: {DuracionMinutos} minutos");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('═', 40));
            Console.ResetColor();
        }

    }
}


   