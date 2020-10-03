using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class Socio
    {
        public Guid GUID_SOCIO { get; set; }
        public string NO_SOCIO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public double? RANKING { get; set; }
        public string IMAGEN { get; set; }
        public string PARTNER_NOMBRE { get; set; }
        public string IMAGEN_PARTNER { get; set; }
        public string SERVICIOS { get; set; }
        public int EDAD { get; set; }
        public string Seleccionado { get; set; }
    }
}
