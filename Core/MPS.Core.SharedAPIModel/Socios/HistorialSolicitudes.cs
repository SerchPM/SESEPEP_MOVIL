using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class HistorialSolicitudes
    {
        public string GUID_SOLICITUD { get; set; }
        public string FOLIO_SOLICITUD { get; set; }
        public double? VALORACION_CLIENTE { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string CLIENTE { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public DateTime? INICIO_SOLICITUD { get; set; }
        public int TIEMPO_PACTADO { get; set; }
        public string TIEMPO_REAL { get; set; }
        public string UBICACION_1 { get; set; }
        public string UBICACION_2 { get; set; }
        public decimal? TOTAL_PAG_SOCIO { get; set; }
    }
}
