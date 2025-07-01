using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal class JuegoderealidadVirtual: Juego
    {
        private string plataformaVR;
        private bool requiereControladores;

        public JuegoderealidadVirtual(
            string nombre,
            int año,
            string creador,
            string genero,
            string descripcion,
            EstadoJuego estadoactual,
            string plataformaVR,
            bool requiereControladores
        ) : base(nombre, año, creador, genero, descripcion,estadoactual)
        {
            this.plataformaVR = plataformaVR;
            this.requiereControladores = requiereControladores;
        }

        public string PlataformaVR
        {
            get { return plataformaVR; }
            set { plataformaVR = value; }
        }

        public bool RequiereControladores
        {
            get { return requiereControladores; }
            set { requiereControladores = value; }
        }

        public override void mostrarDetalles()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║     JUEGO DE REALIDAD VIRTUAL      ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Año de lanzamiento: {Año}");
            Console.WriteLine($"Creador: {Creador}");
            Console.WriteLine($"Género: {Genero}");
            Console.WriteLine("Descripción:");
            Console.WriteLine(Descripcion.Replace(". ", ".\n"));
            Console.WriteLine($"Estado actual: {Estadoactual}");
            Console.WriteLine($"Plataforma VR: {plataformaVR}");
            Console.WriteLine($"Requiere controladores: {(requiereControladores ? "Sí" : "No")}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('═', 40));
            Console.ResetColor();
        }

    }

}

