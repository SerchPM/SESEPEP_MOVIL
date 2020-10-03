using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel
{
    public class Geoposicion
    {
        public double? Latitud { get; set; }

        public double? Longitud { get; set; }

        public Geoposicion(double? latitud, double? longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}
