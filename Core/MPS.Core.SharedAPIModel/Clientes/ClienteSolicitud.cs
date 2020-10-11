using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Clientes
{
    public class ClienteSolicitud
    {
        public Guid GUID_SOLICITUD { get; set; }
        public string FOLIO_SOLICITUD { get; set; }
        public decimal? VALORACION_CLIENTE { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string NO_SOCIO { get; set; }
        public string SOCIO { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public DateTime INICIO_SOLICITUD { get; set; }
        public int TIEMPO_PACTADO { get; set; }
        public int? TIEMPO_REAL { get; set; }
        public string UBICACION_1 { get; set; }
        public string UBICACION_2 { get; set; }
        public decimal? TOTAL_PAGADO { get; set; }
    }
}
