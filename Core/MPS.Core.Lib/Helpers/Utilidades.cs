using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.Helpers
{
    public class Utilidades
    {
        public static (double latitud, double longitud) LimpiarCadenaUbicacion(string ubicacion)
        {
            double latitud = 0;
            double longitud = 0;
            var cadenaLimpia = ubicacion;
            try
            {
                cadenaLimpia = cadenaLimpia.Replace("POINT (", "");
                cadenaLimpia = cadenaLimpia.Replace(")", "");
                var datos = cadenaLimpia.Split(' ');
                latitud = double.Parse(datos[1]);
                longitud = (double.Parse(datos[0]));
            }
            catch
            {
                return (latitud, longitud);
            }
            return (latitud, longitud);
        }
    }
}
