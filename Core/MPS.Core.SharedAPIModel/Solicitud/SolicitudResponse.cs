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
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud2 { get; set; }
        public decimal Longitud2 { get; set; }
        public int? NoElementos { get; set; }
    }
}
