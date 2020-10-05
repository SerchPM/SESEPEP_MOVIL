using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Solicitud
{
    public class AsignarSolicitudResponse
    {
        public string ESTATUS { get; set; }
        public string DESCRIPCION { get; set; }
        public Guid GUID_REGISTRO { get; set; }
        public string FOLIO { get; set; }
        public DateTime FECHA { get; set; }
    }

    public class SocioAsignado
    {
        public Guid IdSolicitud { get; set; }
        public Guid IdSocio { get; set; }
        public int Estatus { get; set; }
    }
}
