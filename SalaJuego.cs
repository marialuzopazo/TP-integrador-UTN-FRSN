using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal class SalaJuego: IChequeoEstado
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
                foreach (var juego in juegosActivos)
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
                if (juegosActivos.Contains(juego))
                {
                    juego.Estadoactual = estado;
                    Console.WriteLine($"Estado del juego '{juego.Nombre}' cambiado a {estado}");
                }
                else
                {
                    Console.WriteLine("El juego no se encuentra en la sala.");
                }
            }
            
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
