using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class SolicitudPendiente
    {
        public Guid GUID_SOLICITUD { get; set; }
        public string NO_SOLICITUD { get; set; }
        public Guid GUID_CLIENTE { get; set; }
        public string NO_CLIENTE { get; set; }
        public Guid GUID_TIPO_SOLICITUD { get; set; }
        public string TIPO_SOLICITUD { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public decimal? COSTO_SOCIO { get; set; }
        public int HORAS_PACTADAS { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string ESTATUS_SOLICITUD { get; set; }
        public string UBICACION_SERVICIO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public double RANKING { get; set; }
    }
}
