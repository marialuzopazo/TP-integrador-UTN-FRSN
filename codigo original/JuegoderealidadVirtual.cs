﻿using System;
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
            Console.WriteLine("Juego de Realidad Virtual");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            Console.WriteLine($"Nombre: {Nombre}");

            Console.WriteLine($"Año de lanzamiento: {Año}");

            Console.WriteLine($"Creador: {Creador}");

            Console.WriteLine($"Género: {Genero}");

            Console.WriteLine($"Descripcion del juego:{Descripcion}");

            Console.WriteLine(Descripcion.Replace(". ", ".\n")); // Para que cada oración vaya en una línea nueva

            Console.WriteLine($"Estado actual: {Estadoactual}");

            Console.WriteLine($"Plataforma VR: {plataformaVR}");

            Console.WriteLine($"Requiere controladores: {(requiereControladores ? "Sí" : "No")}");

            Console.WriteLine(new string('-', 40));
        }
    }

}

