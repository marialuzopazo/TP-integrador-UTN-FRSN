using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal class HistorialDeEstados
    {
        private readonly string _nombreJuego;
        private readonly List<Dictionary<string, object>> _cambios;

        public HistorialDeEstados(string nombreJuego)
        {
            _nombreJuego = nombreJuego;
            _cambios = new List<Dictionary<string, object>>();
        }

       
        public void Iniciar(string comentario = "")
        {
            RegistrarCambio(EstadoJuego.Jugando, comentario);
        }

        public void Pausar(string comentario = "")
        {
            if (ObtenerEstadoActual() != EstadoJuego.Jugando)
                throw new InvalidOperationException("Solo se puede pausar un juego en estado Jugando");

            RegistrarCambio(EstadoJuego.Pausado, comentario);
        }

        public void Finalizar(string comentario = "")
        {
            RegistrarCambio(EstadoJuego.Finalizado, comentario);
        }

        public EstadoJuego ObtenerEstadoActual()
        {
            if (!_cambios.Any())
                return EstadoJuego.NoIniciado;

            return (EstadoJuego)_cambios.Last()["estado"];
        }

        public bool EstaFinalizado()
        {
            return ObtenerEstadoActual() == EstadoJuego.Finalizado;
        }

        
        private void RegistrarCambio(EstadoJuego estado, string comentario)
        {
            var registro = new Dictionary<string, object>
        {
            { "fecha", DateTime.Now },
            { "estado", estado },
            { "comentario", comentario }
        };

            _cambios.Add(registro);
        }

        public int ObtenerDiasEnEstadoActual()
        {
            if (!_cambios.Any())
                return 0;

            var ultimoCambio = (DateTime)_cambios.Last()["fecha"];
            return (DateTime.Now - ultimoCambio).Days;
        }

        public string ObtenerHistorialCompleto()
        {
            var reporte = $"Historial de estados para {_nombreJuego}:\n";
            reporte += "----------------------------------------\n";

            foreach (var cambio in _cambios)
            {
                reporte += $"[{(DateTime)cambio["fecha"]:dd/MM/yyyy HH:mm}]\n";
                reporte += $"Estado: {cambio["estado"]}\n";
                reporte += $"Comentario: {cambio["comentario"]}\n";
                reporte += "----------------------------------------\n";
            }

            if (_cambios.Any())
            {
                reporte += $"Estado actual: {ObtenerEstadoActual()} (desde hace {ObtenerDiasEnEstadoActual()} días)\n";
            }

            return reporte;
        }
    }
}
