using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel
{
    public class Dispositivo
    {
        public Guid Id { get; set; }
        public int TipoUsuario { get; set; }
        public string TimeZona { get; set; }
        public string VercionApp { get; set; }
        public string TipoDispositivo { get; set; }
        public string Modelo { get; set; }
        public string SO { get; set; }
    }
}
