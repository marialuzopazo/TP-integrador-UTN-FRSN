using System;

namespace Misjuegos
{
    internal class JuegoporConsola : Juego
    {
        private string plataforma;

        public JuegoporConsola(
            string nombre,
            int año,
            string creador,
            string genero,
            string descripcion,
            EstadoJuego estadoactual,
            string plataforma)
            : base(nombre, año, creador, genero, descripcion, estadoactual)
        {
            this.plataforma = plataforma;
        }

        public string Plataforma
        {
            get { return plataforma ?? "Sin especificar"; }
            set { plataforma = value; }
        }

        public override void mostrarDetalles()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║       JUEGO POR CONSOLA      ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Año: {Año}");
            Console.WriteLine($"Creador: {Creador}");
            Console.WriteLine($"Género: {Genero}");
            Console.WriteLine("Descripción:");
            Console.WriteLine(Descripcion.Replace(". ", ".\n"));
            Console.WriteLine($"Estado: {Estadoactual}");
            Console.WriteLine($"Plataforma: {Plataforma}");  // ← 🔥 MOSTRÁ DIRECTO EL STRING
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('═', 40));
            Console.ResetColor();
        }
    }

}