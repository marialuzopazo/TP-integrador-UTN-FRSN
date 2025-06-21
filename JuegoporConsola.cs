using System;


namespace Misjuegos
{



    internal class JuegoporConsola : Juego
    {
        private string plataforma;  // <-- CAMBIO: de enum Plataforma a string

        public JuegoporConsola(
            string nombre,
            int año,
            string creador,
            string genero,
            string descripcion,
            EstadoJuego estadoactual,
            string plataforma)  // <-- CAMBIO aquí también
            : base(nombre, año, creador, genero, descripcion, estadoactual)
        {
            this.plataforma = plataforma;
        }

        public string Plataforma
        {
            get { return plataforma; }
            set { plataforma = value; }
        }

        public override void mostrarDetalles()
        {
            Console.WriteLine($"Juego por Consola");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");

            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Año: {Año}");
            Console.WriteLine($"Creador: {Creador}");
            Console.WriteLine($"Género: {Genero}");

            Console.WriteLine($"Descripcion del juego:");
            Console.WriteLine(Descripcion.Replace(". ", ".\n")); // saltos de línea en descripción

            Console.WriteLine($"Estado: {Estadoactual}");
            Console.WriteLine($"Plataforma: {Plataforma}");

            Console.WriteLine(new string('-', 40));
        }
    }

}  

