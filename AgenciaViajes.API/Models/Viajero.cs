using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaViajes.API.Models
{
    public class Viajero
    {
        public int IdViajero { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public int IdDireccion { get; set; }
        public Direccion Direccion { get; set; }
    }
}