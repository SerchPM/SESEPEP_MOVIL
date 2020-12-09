using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Devuelve la fecha y hora como cadena en formato 24 hras.
        /// </summary>
        /// <param name="fechaHora">Fecha y hora que se convertirá.</param>
        /// <returns>Fecha convertida en el formato 24 hras.</returns>
        public static string ToDateTimeFormat24H(this DateTime fechaHora)
        {
            return fechaHora.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Devuelve la fecha y hora como cadena en formato 24 hras.
        /// </summary>
        /// <param name="fechaHora">Fecha y hora que se convertirá.</param>
        /// <returns>Fecha convertida en el formato 24 hras.</returns>
        public static string ToDateTimeFormat24H(this DateTime? fechaHora)
        {
            return fechaHora?.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
