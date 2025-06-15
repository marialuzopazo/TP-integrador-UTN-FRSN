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
    private EstadoJuego estadoactual;

        public Juego(string nombre, int año, string creador, string genero, EstadoJuego estadoactual)
        {
            this.nombre = nombre;
            this.año = año;
            this.creador = creador;
            this.genero = genero;
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
        public EstadoJuego Estadoactual 
        { get { return estadoactual; } 
            set { estadoactual = value; }
        
        }
           

        public abstract void mostrarDetalles();
        public void MostrarEstado()
        {
            Console.WriteLine($"Estado actual del juego:{estadoactual}");
        }
            
        







    }
}
