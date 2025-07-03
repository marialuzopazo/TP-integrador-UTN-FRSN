--------------------------------------------
               TRABAJO INTEGRADOR
                 PROGRAMACIÓN 2 / utn frsn
       Aplicación de consola con conexión a API
---------------------------
## Nombres de integrantes de este trabajo : 👩 👩 👩

*  Pilar
* Candela
* Maria luz
----------------------------------------
# El Gestor de Juegos 
es una aplicación de consola desarrollada en C# que permite a los usuarios organizar su colección personal de juegos (de mesa, consola y realidad virtual) una sala de juego y un historial que pueda guardar no solo los estados de los juegos si no un comentario sobre el mismo 

![Gestor de colección](https://github.com/user-attachments/assets/c976e079-c353-4d75-908e-fe292c162cf5)

# Objetivos de la aplicación: 
- Gestionar una colección personalizada de juegos con opciones para agregar, eliminar, modificar y visualizar detalles.
- gestionar una sala de juego en donde se pueda modificar los estados de los juegos ( ejemplo: juego finalizado ) 
- revisar el historial de juegos en donde no solo se podra ver los estados de los juegos si no que tambièn se pueda realizar un comentario sobre la experiencia con el mismo
- Implementar persistencia de datos mediante serialización en JSON para guardar y cargar la información del usuario.
- Consumir una API (en nuestro caso, simulada y personalizada) para obtener información adicional sobre los juegos.
- Aplicar conceptos de POO: herencia, encapsulamiento, interfaces, enumeraciones y relaciones de agregación/composición.

--------------------------------------
## DIARIO DE AVANCES DEL EQUIPO: 📰
VIERNES 6 / JUNIO: Definimos la idea del proyecto - la presentamos al docente a cargo de evaluar este proyecto (visto bueno para la propuesta)

SABADO 7 / JUNIO: realizamos videollamada grupal, con las partes que tendrá el menu del usuario, las posibilidades de evaluar cada api encontrada y sus caracteristicas

DOMINGO 8 / JUNIO: nueva videollamada grupal, por inconvenientes de la api seleccionada, y los plazos de entrega, se propuso "simular la api"

en consecuencia, se buscaron los datos necesarios para el tp + se realizaron los primeros bocetos del UML

-- el json se subió a este repositorio 

-- el borrador del UML se subió en el repositorio ( se acordó presentar el borrador durante la próxima clase, para poder avanzar ) 

-- para la parte del museo se acordó generar patrones ASCII / para mejorar la experiencia del usuario y considerar sumarlo en caso de contar con tiempo en el menu. 

MARTES 10 / JUNIO : revision del UML por el docente, tareas: mejorar el UML: colección unir con usuario - definir la relación de museo - modificar el nombre juego en lista de colección ( debe ser videojuego ) - chequear videojuego retro / proxima revisión VIERNES 13/06

VIERNES 13 / JUNIO: docente revisó UML - observaciones: interfaz / clases que cumplan la herencia como por ejemplo clase juego y sub clase juego de mesa / de realidad virtual / de consola

SABADO 14 / JUNIO: videollamada , inicio de los primeros codigos, definicion de una interfaz valida util para el gestor + armado de documento en pdf de presentación del gestor + UML finalizado 

DOMINGO 15 / JUNIO: videollamada para prueba de cada parte del codigo y el menu por consola, limpieza del repositorio, definimos armar un archivo pdf con la presentación del trabajo para la entrega y pactamos nueva reunion en meet el LUNES 16 / JUNIO para definir detalles y entrega del TP 

sitio web utilizado para realizar el UML:
https://app.diagrams.net/  

link de la Api simulada: 
api https://run.mocky.io/v3/9b58d6ab-5447-449d-9f75-601dd6334f34
--------------------------------------------
## Observaciones  DEL DOCENTE:

UML: Herencia señalada en sentido inverso. Lo nombres de las clases no deben tener espacios. La clase JuegoPorConsola parece estar demás. La relación de agregación de Juego con Usuario y SalaJuegos está al revés. Existe una relación entre SalaJuego y HistoriaDeEstados pero no se ve ninguna proiedad de un tipo u otro. Lo mismo pasa entre HistorialDeEstado y Juegos. Las relaciones de IChequeoEstado con las 3 clases no tienen sentido (mal graficada). Ademas no tiene sentido. CODIGO: en la clase Juego, la propiedad año no deberia tener caracteres especiales. La propiedad genero es de distinto tipo que en el diagrama UML. En la clase JuegoPorConsola tiene una propiedad que no está en el diagrama UML. En la clase Usuario, la propiedad contraseña no debe tener caracteres especiales. Ademas la propiedad sala (que no se usa) e historiales no están en el diagrama. Los métodos VerificarContrasena, MostrarColeccion, CambiarEstadoJuego, ObtenerHistorialJuego y MostrarEstadoJuegos no estan en el diagrama UML. En la clase SalaJuego, las propiedades propietario y fechaCreacion no se utilizan. En la clase HistoriaDeEstados la propiedad FechaEstado que esta en el diagrama UML no está en el código. Los métodos Iniciar, Pausar, Finalizar, RegistrarCambio, ObtenerDiasEnEstadoActual y ObtenerHistorialCompleto no estan en el diagrama UML. Las clases dentro de Class1 no estan en el diagrama UML. En el programa principal hay una mala practica al utilizar while (true). EJECUCION: Al ingresar datos incorrectos en inicio de sesion, luego de mostrar el cartel de datos incorrectos se deberia limpiar la pantalla y volver a redibujar el menu.

## CORRECCIONES realizadas:

- se realizó y actualizó el UML
- se mejoraron partes del codigo
- se expuso el tp grupal
- el codigo nuevo / esta disponible para descargar como TP.CORREGIDO.RAR 
  
 ## 🎉 EL TRABAJO ESTÁ APROBADO ! ! 🎉

