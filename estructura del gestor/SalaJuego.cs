using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_GRUPAL
{
   

    public class SalaJuegos : IChequeoEstado
    {
        private Usuario propietario;
        private DateTime fechaCreacion;
        private List<Juego> juegosActivos;

        public SalaJuegos(Usuario propietario)
        {
            this.propietario = propietario;
            this.fechaCreacion = DateTime.Now;
            this.juegosActivos = new List<Juego>();
        }

        public void AgregarJuego(Juego juego)
        {
            JuegosActivos.Add(juego);
        }

        public void EliminarJuego(string nombre)
        {
            JuegosActivos.RemoveAll(j => j.Nombre == nombre);
        }

        public void BuscarJuego(string nombre)
        {
            foreach (var juego in JuegosActivos)
            {
                if (juego.Nombre == nombre)
                {
                    Console.WriteLine("Juego encontrado en sala: " + juego.Nombre);
                    return;
                }
            }
            Console.WriteLine("Juego no encontrado en sala.");
        }
        public void CambiarEstado(Juego juego, EstadoJuego estado)
        {
            if (JuegosActivos.Contains(juego))
            {
                juego.EstadoActual = estado;
                Console.WriteLine($"Estado del juego '{juego.Nombre}' cambiado a {estado}");
            }
            else
            {
                Console.WriteLine("El juego no se encuentra en la sala.");
            }
        }
        // Implementación de IChequeoEstado
        public void Iniciar()
        {
            Console.WriteLine("Se han iniciado los juegos en la sala.");
        }

        public void Pausar()
        {
            Console.WriteLine("Los juegos en la sala están pausados.");
        }

        public void Finalizar()
        {
            Console.WriteLine("Se ha finalizado la sesión de juegos.");
        }

    }

}

