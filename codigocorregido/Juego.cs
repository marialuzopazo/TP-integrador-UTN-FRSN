using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
     internal abstract class Juego
    {
       private  string nombre;
       private int año;
       private string creador;
       private string genero;
        private string descripcion;
        private EstadoJuego estadoactual;

        public Juego(string nombre, int año, string creador, string genero,string descripcion ,EstadoJuego estadoactual)
        {
            this.nombre = nombre;
            this.año = año;
            this.creador = creador;
            this.genero = genero;
            this.descripcion = descripcion;
            this.estadoactual = estadoactual;
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Año
        {
            get { return año; }
            set { año= value; }

        }
        public string Creador
        {
            get { return creador; }
            set { creador = value; }


        }
        public string Genero
        {
            get { return genero; }
            set { genero = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
       
        public EstadoJuego Estadoactual 
        { get { return estadoactual; } 
            set { estadoactual = value; }
        
        }
           

        public abstract void mostrarDetalles();
        public void MostrarEstado()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║     ESTADO DEL JUEGO         ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Estado actual: {estadoactual}");
            Console.ResetColor();
        }


     }
}
