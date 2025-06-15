using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{

    internal class Class1
    {


        public class Rootobject
        {
            
                public Juegos_Por_Consola[] juegos_por_consola { get; set; }
            }

            public class Juegos_Por_Consola
            {
                public string nombre_consola { get; set; }
                public string fabricante { get; set; }
                public int año_creacion { get; set; }
                public string pais_origen { get; set; }
                public Juego[] juegos { get; set; }
                public Juegos_De_Mesa[] juegos_de_mesa { get; set; }
                public Juegosvr[] juegosVR { get; set; }
            }

            public class Juego
            {
                public string nombre { get; set; }
                public string fecha_lanzamiento { get; set; }
                public string creador { get; set; }
                public string[] editores { get; set; }
                public string clasificacion { get; set; }
                public string genero { get; set; }
                public string modo_juego { get; set; }
                public string licencia { get; set; }
                public string imagen { get; set; }
                public string descripcion { get; set; }
                public string nombre_consola { get; set; }
                public string fabricante { get; set; }
                public int año_creacion { get; set; }
                public string pais_origen { get; set; }

                public Juego1[] juegos { get; set; }
            }

            public class Juego1
            {
                public string nombre { get; set; }
                public string fecha_lanzamiento { get; set; }
                public string creador { get; set; }
                public string[] editores { get; set; }
                public string clasificacion { get; set; }
                public string genero { get; set; }
                public string modo_juego { get; set; }
                public string licencia { get; set; }
                public string imagen { get; set; }
                public string descripcion { get; set; }

               
                public string plataforma { get; set; }
            }

            public class Juegos_De_Mesa
            {
                public string nombre { get; set; }
                public string creador { get; set; }
                public int año_lanzamiento { get; set; }
                public string fecha_lanzamiento { get; set; }
                public string genero { get; set; }
                public string estilo_juego { get; set; }
                public string descripcion { get; set; }
            }

            public class Juegosvr
            {
                public string nombre { get; set; }
                public int año { get; set; }
                public string tipo { get; set; }
                public string creador { get; set; }
                public string género { get; set; }
                public string descripcion { get; set; }
            }
        }
    }



          