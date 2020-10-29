﻿using System;
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
                claveTipoServicio = valores["CLAVE_TIPO_SERVICIO"].ToString();

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
        }

        string mensajePrincipal;
        public string MensajePrincipal { get => mensajePrincipal; set => mensajePrincipal = value; }

        private string actualLat;
        public string ActualLat { get => actualLat; set => actualLat = value; }

        private string calificacionSocio;
        public string CalificacionSocio { get => calificacionSocio; set => calificacionSocio = value; }

        private string claveTipoServicio;
        public string ClaveTipoServicio { get => claveTipoServicio; set => claveTipoServicio = value; }

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
    }
}
