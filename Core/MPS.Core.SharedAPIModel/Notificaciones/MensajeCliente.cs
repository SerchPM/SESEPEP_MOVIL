using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Notificaciones
{
    public class MensajeCliente
    {
        public MensajeCliente(Dictionary<string, object> valores)
        {
            if (valores.ContainsKey("MensajePrincipal"))
                mensajePrincipal = valores["MensajePrincipal"].ToString();

            if (valores.ContainsKey("ACTUAL_LAT"))
                actualLat = valores["ACTUAL_LAT"].ToString();

            if (valores.ContainsKey("CALIFICACION_SOCIO"))
                calificacionSocio = valores["CALIFICACION_SOCIO"].ToString();

            if (valores.ContainsKey("CLAVE_TIPO_SERVICIO"))
                int.TryParse(valores["CLAVE_TIPO_SERVICIO"].ToString(), out claveTipoServicio);

            if (valores.ContainsKey("FECHA_SOLICITUD"))
                fechaSolicitud = valores["FECHA_SOLICITUD"].ToString();

            if (valores.ContainsKey("FOLIO_SOLICITUD"))
                folioSolicitud = valores["FOLIO_SOLICITUD"].ToString();

            if (valores.ContainsKey("GUID_SOCIO"))
                Guid.TryParse(valores["GUID_SOCIO"].ToString(), out idSocio);

            if (valores.ContainsKey("GUID_SOLICITUD"))
                Guid.TryParse(valores["GUID_SOLICITUD"].ToString(), out idSolicitud);

            if (valores.ContainsKey("GUID_TIPO_SOLICITUD"))
                Guid.TryParse(valores["GUID_TIPO_SOLICITUD"].ToString(), out idTipoSolicitud);

            if (valores.ContainsKey("NOMBRE_SERVICIO"))
                nombreServicio = valores["NOMBRE_SERVICIO"].ToString();

            if (valores.ContainsKey("NOMBRE_SOCIO"))
                nombreSocio = valores["NOMBRE_SOCIO"].ToString();            

            if (valores.ContainsKey("TIPO_SERVICIO"))
                tipoServicio = valores["TIPO_SERVICIO"].ToString();

            if (valores.ContainsKey("TIPO_NOTIFICACION"))
                int.TryParse(valores["TIPO_NOTIFICACION"].ToString(), out tipoNotificacion);
        }

        string mensajePrincipal;
        public string MensajePrincipal { get => mensajePrincipal; set => mensajePrincipal = value; }

        private string actualLat;
        public string ActualLat { get => actualLat; set => actualLat = value; }

        private string calificacionSocio;
        public string CalificacionSocio { get => calificacionSocio; set => calificacionSocio = value; }

        private int claveTipoServicio;
        public int ClaveTipoServicio { get => claveTipoServicio; set => claveTipoServicio = value; }

        private string fechaSolicitud;
        public string FechaSolicitud { get => fechaSolicitud; set => fechaSolicitud = value; }

        private string folioSolicitud;
        public string FolioSolicitud { get => folioSolicitud; set => folioSolicitud = value; }

        private Guid idSocio;
        public Guid IdSocio { get => idSocio; set => idSocio = value; }

        private Guid idSolicitud;
        public Guid IdSolicitud { get => idSolicitud; set => idSolicitud = value; }

        private Guid idTipoSolicitud;
        public Guid IdTipoSolicitud { get => idTipoSolicitud; set => idTipoSolicitud = value; }

        private string nombreSocio;
        public string NombreSocio { get => nombreSocio; set => nombreSocio = value; }

        private string nombreServicio;
        public string NombreServicio { get => nombreServicio; set => nombreServicio = value; }

        private string tipoServicio;
        public string TipoServicio { get => tipoServicio; set => tipoServicio = value; }

        private int tipoNotificacion;
        public int TipoNotificacion { get => tipoNotificacion; set => tipoNotificacion = value; }
    }

    public class A
    {
        public string MensajePrincipal { get; set; }
        public string ActualLat { get; set; }
        public string CalificacionSocio { get; set; }
        public int ClaveTipoServicio { get; set; }
        public string FechaSolicitud { get; set; }
        public string FolioSolicitud { get; set; }
        public Guid IdSocio { get; set; }
        public Guid IdSolicitud { get; set; }
        public Guid IdTipoSolicitud { get; set; }
        public string NombreSocio { get; set; }
        public string NombreServicio { get; set; }
        public string TipoServicio { get; set; }
        public int TipoNotificacion { get; set; }
    }

    public class Custom
    {
        public string i { get; set; }
        public A a { get; set; }
    }

    public class Root
    {
        public Custom custom { get; set; }
    }
}
