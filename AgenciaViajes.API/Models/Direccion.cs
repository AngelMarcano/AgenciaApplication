using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgenciaViajes.API.Models
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public long TelefonoMovil { get; set; }
        public long TelefonoFijo { get; set; }
    }
}