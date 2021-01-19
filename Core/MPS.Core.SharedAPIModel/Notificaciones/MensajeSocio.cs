using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Notificaciones
{
    public class MensajeSocio
    {
        public MensajeSocio(Dictionary<string, object> valores)
        {
            if (valores.ContainsKey("MensajePrincipal"))
                mensajePrincipal = valores["MensajePrincipal"].ToString();

            if (valores.ContainsKey("CLAVE_TIPO_SERVICIO"))
                int.TryParse(valores["CLAVE_TIPO_SERVICIO"].ToString(), out claveTipoServicio);

            if (valores.ContainsKey("FECHA_SOLICITUD"))
                fechaSolicitud = valores["FECHA_SOLICITUD"].ToString();

            if (valores.ContainsKey("FOLIO_SOLICITUD"))
                folioSolicitud = valores["FOLIO_SOLICITUD"].ToString();

            if (valores.ContainsKey("GUID_CLIENTE"))
                Guid.TryParse(valores["GUID_CLIENTE"].ToString(), out idCliente);

            if (valores.ContainsKey("GUID_SOLICITUD"))
                Guid.TryParse(valores["GUID_SOLICITUD"].ToString(), out idSolicitud);

            if (valores.ContainsKey("GUID_TIPO_SOLICITUD"))
                Guid.TryParse(valores["GUID_TIPO_SOLICITUD"].ToString(), out idTipoSolicitud);

            if (valores.ContainsKey("NOMBRE_CLIENTE"))
                nombreCliente = valores["NOMBRE_CLIENTE"].ToString();

            if (valores.ContainsKey("NOMBRE_SERVICIO"))
                nombreServicio = valores["NOMBRE_SERVICIO"].ToString();

            if (valores.ContainsKey("TIPO_SERVICIO"))
                tipoServicio = valores["TIPO_SERVICIO"].ToString();

            if (valores.ContainsKey("UBICACION_1"))
                ubicacion = valores["UBICACION_1"].ToString();

            if (valores.ContainsKey("TIPO_NOTIFICACION"))
                int.TryParse(valores["TIPO_NOTIFICACION"].ToString(), out tipoNotificacion);
        }
        string mensajePrincipal;
        public string MensajePrincipal { get => mensajePrincipal; set => mensajePrincipal = value; }

        private int claveTipoServicio;
        public int ClaveTipoServicio { get => claveTipoServicio; set => claveTipoServicio = value; }

        private string fechaSolicitud;
        public string FechaSolicitud { get => fechaSolicitud; set => fechaSolicitud = value; }

        private string folioSolicitud;
        public string FolioSolicitud { get => folioSolicitud; set => folioSolicitud = value; }

        private Guid idCliente;
        public Guid IdCliente { get => idCliente; set => idCliente = value; }

        private Guid idSolicitud;
        public Guid IdSolicitud { get => idSolicitud; set => idSolicitud = value; }

        private Guid idTipoSolicitud;
        public Guid IdTipoSolicitud { get => idTipoSolicitud; set => idTipoSolicitud = value; }

        private string nombreCliente;
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }

        private string nombreServicio;
        public string NombreServicio { get => nombreServicio; set => nombreServicio = value; }

        private string tipoServicio;
        public string TipoServicio { get => tipoServicio; set => tipoServicio = value; }

        private string ubicacion;
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }

        private int tipoNotificacion;
        public int TipoNotificacion { get => tipoNotificacion; set => tipoNotificacion = value; }
    }

    public class AC
    {
        public string MensajePrincipal { get; set; }
        public int ClaveTipoServicio { get; set; }
        public string FechaSolicitud { get; set; }
        public string FolioSolicitud { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdSolicitud { get; set; }
        public Guid IdTipoSolicitud { get; set; }
        public string NombreCliente { get; set; }
        public string NombreServicio { get; set; }
        public string TipoServicio { get; set; }
        public string Ubicacion { get; set; }
        public int TipoNotificacion { get; set; }
    }

    public class CustomC
    {
        public string i { get; set; }
        public AC a { get; set; }
    }

    public class RootC
    {
        public CustomC custom { get; set; }
    }
}
