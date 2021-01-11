using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Seguridad
{
    public class Respuesta
    {
        public string ESTATUS { get; set; }
        public string DESCRIPCION { get; set; }
        public string GUID { get; set; }
        public Guid GUID_SOCIO { get; set; }
        public string FOLIO { get; set; }
        public DateTime FECHA { get; set; }
    }

    public class ResponseCalificacion
    {
        public string ESTATUS { get; set; }
        public string DESCRIPCION { get; set; }
        public Guid GUID_REGISTRO { get; set; }
        public double? FOLIO { get; set; }
        public DateTime FECHA { get; set; }
    }
}
