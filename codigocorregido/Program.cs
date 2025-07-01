using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using static Misjuegos.Class1;
using System.Threading.Tasks;
using System.Threading;

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
                    string url = "https://mocki.io/v1/7582900b-a1d8-4651-b9b5-7d8d641a2b45";
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
                        juegoConsolaJson.plataforma 
                    );

                    juegosDisponibles.Add(juego);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Juego agregado: {juego.Nombre} ({juego.Plataforma})");
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Juego de mesa agregado: {mesa.nombre}");
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Juego VR agregado: {vr.nombre}");
                    Console.ResetColor();
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
        }

       
        private static int ParseYear(string fecha)
        {
            if (DateTime.TryParse(fecha, out DateTime dt))
            {
                return dt.Year;
            }
            return 0;
        }

        static void InicializarDatos()
        {
            usuarios.Add(new Usuario("admin", "admin@juegos.com", "admin123"));
        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║                                            ║");
            Console.WriteLine("║         GESTOR DE JUEGOS v1.0              ║");
            Console.WriteLine("║                                            ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        static void MostrarMenuInicio()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════╗");
                Console.WriteLine("║     MENÚ DE INICIO   ║");
                Console.WriteLine("╚══════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Iniciar sesión");
                Console.WriteLine("2. Registrar nuevo usuario");
                Console.WriteLine("3. Salir");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        IniciarSesion();
                        if (usuarioActual != null) return;
                        break;
                    case "2":
                        RegistrarUsuario();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        static void IniciarSesion()
        {
            Console.Clear();
            Console.Write("\nNombre de usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();

            usuarioActual = usuarios.Find(u => u.Nombre == nombre && u.VerificarContrasena(contrasena));

            if (usuarioActual != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n¡Bienvenido {usuarioActual.Nombre}!");
                Console.ResetColor();
                Thread.Sleep(1000);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nUsuario o contraseña incorrectos.");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
        }

        static void RegistrarUsuario()
        {
            Console.Clear();
            Console.Write("\nNuevo nombre de usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("Correo: ");
            string correo = Console.ReadLine();
            Console.Write("Nueva contraseña: ");
            string contrasena = Console.ReadLine();

            if (usuarios.Exists(u => u.Nombre == nombre))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEl usuario ya existe.");
                Console.ResetColor();
                Thread.Sleep(1500);
                return;
            }

            usuarios.Add(new Usuario(nombre, correo, contrasena));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nUsuario registrado con éxito.");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        static void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n╔══════════════════════════════╗");
                Console.WriteLine($"║  BIENVENIDO, {usuarioActual.Nombre.ToUpper(),-20} ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Mi Colección");
                Console.WriteLine("2. Gestionar Historial de Juegos");
                Console.WriteLine("3. Cerrar sesión");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción: ");

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
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                        break;
                }
            }
        }
        static void MenuColeccion()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════╗");
                Console.WriteLine("║     MI COLECCIÓN     ║");
                Console.WriteLine("╚══════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Ver mis juegos");
                Console.WriteLine("2. Agregar juego a mi colección");
                Console.WriteLine("3. Eliminar juego de mi colección");
                Console.WriteLine("4. Volver al menú principal");
                Console.ResetColor();

                Console.Write("\nSeleccione el número de la opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
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
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                        break;
                }

                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void AgregarJuegoAColeccion()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║     JUEGOS DISPONIBLES       ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.ResetColor();

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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Juego agregado a tu colección.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("El juego ya está en tu colección.");
                        Console.ResetColor();
                    }
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Selección no válida.");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                }
            }
        }

        static void EliminarJuegoDeColeccion()
        {
            while (true)
            {
                Console.Clear();
                if (usuarioActual.Coleccion.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No tienes juegos en tu colección.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║     ELIMINAR UN JUEGO        ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.ResetColor();

                usuarioActual.MostrarColeccion();

                Console.Write("\nSeleccione un juego para eliminar (número): ");
                if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= usuarioActual.Coleccion.Count)
                {
                    usuarioActual.EliminarJuego(seleccion - 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Juego eliminado de tu colección.");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Selección no válida.");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                }
            }
        }

        static void MenuHistorial()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║   GESTIÓN DE HISTORIAL       ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Ver historial de un juego");
                Console.WriteLine("2. Cambiar estado de un juego");
                Console.WriteLine("3. Ver estados de todos los juegos");
                Console.WriteLine("4. Volver al menú principal");
                Console.ResetColor();

                Console.Write("\nSeleccione el número de la opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        VerHistorialJuego();
                        break;
                    case "2":
                        CambiarEstadoJuego();
                        break;
                    case "3":
                        Console.Clear();
                        usuarioActual.MostrarEstadosJuegos();
                        break;
                    case "4":
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                        break;
                }

                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void VerHistorialJuego()
        {
            Console.Clear();
            if (usuarioActual.Coleccion.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No tienes juegos en tu colección.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔══════════════════════════════╗");
            Console.WriteLine("║     HISTORIAL DE JUEGO       ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();

            usuarioActual.MostrarColeccion();

            Console.Write("\nSeleccione un juego para ver su historial (número): ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= usuarioActual.Coleccion.Count)
            {
                var juegoSeleccionado = usuarioActual.Coleccion[seleccion - 1];

                // Mostrar detalles del juego
                juegoSeleccionado.mostrarDetalles();

                // Mostrar historial
                var historial = usuarioActual.ObtenerHistorialJuego(juegoSeleccionado.Nombre);
                Console.WriteLine(historial?.ObtenerHistorialCompleto() ?? "No hay historial disponible para este juego.");

            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Selección no válida.");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
        }

        static void CambiarEstadoJuego()
        {
            while (true)
            {
                Console.Clear();
                if (usuarioActual.Coleccion.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No tienes juegos en tu colección.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║     CAMBIAR ESTADO DE JUEGO  ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.ResetColor();

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
                        Console.Write("Ingrese un comentario (opcional): ");
                        string comentario = Console.ReadLine();

                        usuarioActual.CambiarEstadoJuego(juegoSeleccionado.Nombre, (EstadoJuego)estadoSeleccionado, comentario);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Estado del juego '{juegoSeleccionado.Nombre}' cambiado a {(EstadoJuego)estadoSeleccionado}");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Estado no válido.");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Selección no válida.");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                }
            }
        }
    }
}
