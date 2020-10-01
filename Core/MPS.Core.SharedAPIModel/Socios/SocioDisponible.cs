using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    class SocioDisponible
    {
        public string GUID_SOCIO { get; set; }
        public string NO_SOCIO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public object RANKING { get; set; }
        public string IMAGEN { get; set; }
        public object PARTNER_NOMBRE { get; set; }
        public object IMAGEN_PARTNER { get; set; }
        public string SERVICIOS { get; set; }
    }
}
