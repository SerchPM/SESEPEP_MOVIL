using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Clientes
{
    public class Historial
    {
        public DateTime Solicitud { get; set; }
        public DateTime Inicio { get; set; }
        public decimal Costo { get; set; }
        public int Tiempo { get; set; }
    }
}
