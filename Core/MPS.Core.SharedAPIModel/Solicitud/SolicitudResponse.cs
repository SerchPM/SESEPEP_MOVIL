using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Solicitud
{
    public class SolicitudResponse
    {
        public string ESTATUS { get; set; }
        public string DESCRIPCION { get; set; }
        public Guid GUID { get; set; }
        public string FOLIO { get; set; }
        public DateTime FECHA { get; set; }
        public string ATRIBUTO1 { get; set; }
    }
    public class Solicitud
    {
        public Guid IdTipoSolicitud { get; set; }
        public int IdTipoServicio { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int HorasSolicidatas { get; set; }
        public Guid IdCliente { get; set; }
        public Guid? IdSocio { get; set; }
        public decimal TotalPago { get; set; }
        public Guid IdMoneda { get; set; }
        public Guid? IdCuentaOrigen { get; set; }
        public string Origen { get; set; }
        public Guid? IdCuentaDestino { get; set; }
        public string Destino { get; set; }
        public int TiempoGenerarSolicitud { get; set; }
        public string VersionApp { get; set; }
        public string Movil { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public decimal Latitud2 { get; set; }
        public decimal Longitud2 { get; set; }
        public int NoElementos { get; set; }
    }

    public class SolicitudActivaResponse
    {
        public string GUID_SOLICITUD { get; set; }
        public string NOMBRE { get; set; }
        public string SOLICITUD { get; set; }
        public float VALORACION { get; set; }
        public string NO_CLIENTE { get; set; }
        public string NO_SOCIO { get; set; }
        public DateTime FECHA_HORA_SOLICITUD { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
        public DateTime FECHA_HORA_FIN { get; set; }
        public int HORAS_SOLICITADAS { get; set; }
        public int HORAS_REALES { get; set; }
        public string DIFERENCIA { get; set; }
        public string C1RA_UBICACION { get; set; }
        public string C2DA_UBICACION { get; set; }
    }
}
