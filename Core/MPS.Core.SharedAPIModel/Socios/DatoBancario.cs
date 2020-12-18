using System;

namespace MPS.SharedAPIModel.Socios
{
    public class DatoBancario
    {
        public Guid GUID { get; set; }
        public string NO_SOCIO { get; set; }
        public string CUENTA_BANCO { get; set; }
        public Guid GUID_TARJETA { get; set; }
        public string NUMERO_TARJETA { get; set; }
    }

    public class ActualizacionResponse
    {
        public string ESTATUS { get; set; }
        public string DESCRIPCION { get; set; }
        public Guid GUID_REGISTRO { get; set; }
        public string NO_SOCIO { get; set; }
        public DateTime FECHA { get; set; }
    }
}
