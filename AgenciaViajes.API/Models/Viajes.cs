using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaViajes.API.Models
{
    public class Viajes
    {
        public int IdViaje { get; set; }
        public string Codigo { get; set; }
        public int Plazas { get; set; }
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public decimal Precio { get; set; }
        public int IdViajero { get; set; }
        public DateTime FechaSalida { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLLegada { get; set; }
        public Viajero Viajero { get; set; }

    }
}