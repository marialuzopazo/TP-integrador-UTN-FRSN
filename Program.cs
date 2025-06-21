using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using static Misjuegos.Class1;

namespace Misjuegos
{
    internal class Program
    {
        private static Usuario usuarioActual;
        private static List<Usuario> usuarios = new List<Usuario>();
        private static List<Juego> juegosDisponibles = new List<Juego>();

        static async Task Main(string[] args)
        {
            string jsonconfig1;
            string path = "jsconfig1.json";

            if (File.Exists(path))
            {
                Console.WriteLine("Cargando datos desde archivo local...");
                jsonconfig1 = File.ReadAllText(path);
            }
            else
            {
                try
                {
                    Console.WriteLine("Archivo local no encontrado. Intentando descargar desde internet...");
                    var client = new HttpClient(); 
                    string url = "https://run.mocky.io/v3/6312fe8c-c54f-4db0-b5ea-c95b4aed2560";
                    jsonconfig1 = await client.GetStringAsync(url);

                    File.WriteAllText(path, jsonconfig1);
                    Console.WriteLine("Archivo descargado y guardado localmente en: " + path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al descargar el archivo: {ex.Message}");
                    Console.WriteLine("No hay archivo local disponible. Abortando ejecución.");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(jsonconfig1))
            {
                Console.WriteLine("El contenido JSON está vacío. Abortando.");
                return;
            }

            Rootobject root;

            try
            {
                root = JsonSerializer.Deserialize<Rootobject>(jsonconfig1);
                Console.WriteLine("Deserialización exitosa.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al deserializar el JSON: " + ex.Message);
                return;
            }




            if (root.juegos_por_consola != null)
            {
                foreach (var juegoConsolaJson in root.juegos_por_consola)
                {
                    int año = ParseYear(juegoConsolaJson.fecha_lanzamiento);

                    var juego = new JuegoporConsola(
                        juegoConsolaJson.nombre,
                        año,
                        juegoConsolaJson.creador,
                        juegoConsolaJson.genero,
                        juegoConsolaJson.descripcion,
                        EstadoJuego.NoIniciado,
                        "Consola"
                    );

                    juegosDisponibles.Add(juego);
                    Console.WriteLine($"Juego agregado: {juegoConsolaJson.nombre} (Consola)");
                }
            }

            if (root.juegos_de_mesa != null)
            {
                foreach (var mesa in root.juegos_de_mesa)
                {
                    var juegoMesa = new JuegodeMesa(
                        mesa.nombre,
                        mesa.año_lanzamiento,
                        mesa.creador,
                        mesa.genero,
                        mesa.descripcion,
                        EstadoJuego.NoIniciado,
                        2,
                        30
                    );
                    juegosDisponibles.Add(juegoMesa);
                    Console.WriteLine($"Juego de mesa agregado: {mesa.nombre}");
                }
            }

            if (root.juegosVR != null)
            {
                foreach (var vr in root.juegosVR)
                {
                    var juegoVR = new JuegoderealidadVirtual(
                        vr.nombre,
                        vr.año,
                        vr.creador,
                        vr.género,
                        vr.descripcion,
                        EstadoJuego.NoIniciado,
                        "Género VR",
                        true
                    );
                    juegosDisponibles.Add(juegoVR);
                    Console.WriteLine($"Juego VR agregado: {vr.nombre}");
                }
            }


            InicializarDatos();
            MostrarBienvenida();

            while (true)
            {
                if (usuarioActual == null)
                {
                    MostrarMenuInicio();
                }
                else
                {
                    MostrarMenuPrincipal();
                }
            }

            string misobjetos = JsonSerializer.Serialize(root);

            using (FileStream archivo = new FileStream("jsconfig1.json", FileMode.Create))
            using (StreamWriter conte = new StreamWriter(archivo))
            {
                conte.Write(misobjetos);
            }


        }

        static int ParseYear(string fecha)
        {
            if (DateTime.TryParse(fecha, out var fechaDate))
                return fechaDate.Year;

            return 2000; 
        }

        static void InicializarDatos()
        {
            usuarios.Add(new Usuario("admin", "admin@juegos.com", "admin123"));
        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("      GESTOR DE JUEGOS v1.0");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        static void MostrarMenuInicio()
        {
            Console.WriteLine("\nMenú de Inicio:");

            Console.WriteLine("1. Iniciar sesión");

            Console.WriteLine("2. Registrar nuevo usuario");

            Console.WriteLine("3. Salir");

            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    IniciarSesion();
                    break;
                case "2":
                    RegistrarUsuario();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        static void IniciarSesion()
        {
            Console.Write("\nNombre de usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();

            usuarioActual = usuarios.Find(u => u.Nombre == nombre && u.VerificarContrasena(contrasena));

            if (usuarioActual != null)
            {
                Console.WriteLine($"\n¡Bienvenido {usuarioActual.Nombre}!");
            }
            else
            {
                Console.WriteLine("\nUsuario o contraseña incorrectos.");
            }
        }

        static void RegistrarUsuario()
        {
            Console.Write("\nNuevo nombre de usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("Correo: ");
            string correo = Console.ReadLine();
            Console.Write("Nueva contraseña: ");
            string contrasena = Console.ReadLine();

            if (usuarios.Exists(u => u.Nombre == nombre))
            {
                Console.WriteLine("\nEl usuario ya existe.");
                return;
            }

            usuarios.Add(new Usuario(nombre, correo, contrasena));
            Console.WriteLine("\nUsuario registrado con éxito.");
        }

        static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine($"\nBienvenido, {usuarioActual.Nombre}!");

            Console.WriteLine("*Menú Principal:");

            Console.WriteLine("1. Mi Colección");

            Console.WriteLine("2. Gestionar Historial de Juegos");

            Console.WriteLine("3. Cerrar sesión");

            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MenuColeccion();
                    break;
                case "2":
                    MenuHistorial();
                    break;
                case "3":
                    usuarioActual = null;
                    MostrarBienvenida();
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        static void MenuColeccion()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nMI COLECCIÓN");
                Console.WriteLine("~~~~~~~~~~~~~~~~");
                Console.WriteLine("1. Ver mis juegos");

                Console.WriteLine("2. Agregar juego a mi colección.");

                Console.WriteLine("3. Eliminar juego de mi colección.");

                Console.WriteLine("4. Volver al menú principal.");

                Console.Write("Seleccione el número de la opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        usuarioActual.MostrarColeccion();
                        break;
                    case "2":
                        AgregarJuegoAColeccion();
                        break;
                    case "3":
                        EliminarJuegoDeColeccion();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }


        static void AgregarJuegoAColeccion()
        {
            Console.WriteLine("\nJuegos disponibles:");

            for (int i = 0; i < juegosDisponibles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {juegosDisponibles[i].Nombre}");
            }

            Console.Write("\nSeleccione un juego para agregar: ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= juegosDisponibles.Count)
            {
                var juegoSeleccionado = juegosDisponibles[seleccion - 1];

                if (!usuarioActual.Coleccion.Any(j => j.Nombre == juegoSeleccionado.Nombre))
                {
                    usuarioActual.AgregarJuego(juegoSeleccionado);
                    Console.WriteLine("Juego agregado a tu colección.");
                }
                else
                {
                    Console.WriteLine("El juego ya está en tu colección.");
                }
            }
            else
            {
                Console.WriteLine("Selección no válida.");
            }
        }




        static void EliminarJuegoDeColeccion()
        {
            if (usuarioActual.Coleccion.Count == 0)
            {
                Console.WriteLine("No tienes juegos en tu colección.");
                return;
            }

            Console.WriteLine("\nTus juegos:");
            usuarioActual.MostrarColeccion();

            Console.Write("\nSeleccione un juego para eliminar (número): ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= usuarioActual.Coleccion.Count)
            {
                usuarioActual.EliminarJuego(seleccion - 1);
                Console.WriteLine("Juego eliminado de tu colección.");
            }
            else
            {
                Console.WriteLine("Selección no válida.");
            }
        }

        static void MenuHistorial()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nGESTIÓN DE HISTORIAL");
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("1. Ver historial de un juego");

                Console.WriteLine("2. Cambiar estado de un juego");

                Console.WriteLine("3. Ver estados de todos los juegos");

                Console.WriteLine("4. Volver al menú principal");

                Console.Write("Seleccione el número de la opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        VerHistorialJuego();
                        break;
                    case "2":
                        CambiarEstadoJuego();
                        break;
                    case "3":
                        usuarioActual.MostrarEstadosJuegos();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void VerHistorialJuego()
        {
            if (usuarioActual.Coleccion.Count == 0)
            {
                Console.WriteLine("No tienes juegos en tu colección.");
                return;
            }

            Console.WriteLine("\nTus juegos:");
            usuarioActual.MostrarColeccion();

            Console.Write("\nSeleccione un juego para ver su historial (número): ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= usuarioActual.Coleccion.Count)
            {
                Juego juegoSeleccionado = usuarioActual.Coleccion[seleccion - 1];
                var historial = usuarioActual.ObtenerHistorialJuego(juegoSeleccionado.Nombre);
                Console.WriteLine(historial?.ObtenerHistorialCompleto() ?? "No hay historial disponible para este juego.");
            }
            else
            {
                Console.WriteLine("Selección no válida.");
            }
        }

        static void CambiarEstadoJuego()
        {
            if (usuarioActual.Coleccion.Count == 0)
            {
                Console.WriteLine("No tienes juegos en tu colección.");
                return;
            }

            Console.WriteLine("\nTus juegos:");
            usuarioActual.MostrarColeccion();

            Console.Write("\nSeleccione un juego para cambiar su estado (número): ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= usuarioActual.Coleccion.Count)
            {
                Juego juegoSeleccionado = usuarioActual.Coleccion[seleccion - 1];

                Console.WriteLine("\nEstados disponibles:");
                foreach (EstadoJuego estado in Enum.GetValues(typeof(EstadoJuego)))
                {
                    Console.WriteLine($"{(int)estado}. {estado}");
                }

                Console.Write("Seleccione el nuevo estado (número): ");
                if (int.TryParse(Console.ReadLine(), out int estadoSeleccionado) && Enum.IsDefined(typeof(EstadoJuego), estadoSeleccionado))
                {
                    string comentario = "";
                    Console.Write("Ingrese un comentario (opcional): ");
                    comentario = Console.ReadLine();

                    usuarioActual.CambiarEstadoJuego(juegoSeleccionado.Nombre, (EstadoJuego)estadoSeleccionado, comentario);
                    Console.WriteLine($"Estado del juego '{juegoSeleccionado.Nombre}' cambiado a {(EstadoJuego)estadoSeleccionado}");
                }
                else
                {
                    Console.WriteLine("Estado no válido.");
                }
            }
            else
            {
                Console.WriteLine("Selección no válida.");
            }
        }
    }
}